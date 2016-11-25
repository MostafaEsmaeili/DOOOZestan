using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.DataAccess.Query.ExcelQuery;
using Framework.Utility;

namespace Framework.DataAccess.Query
{
    public class SqlGenerator
    {
        private static Dictionary<string, string> _stringEquality;

        public static string GetQuery(Query query)
        {
            
            if (!query.IsForCheckExistanceOnly)
            {
                const string oneOne = "1=1";

                string star = "*";
                if (query.IncludeColumns != null && query.IncludeColumns.Count > 0)
                {
                    star = query.IncludeColumns.Aggregate("", (current, includeColumn) => current + (current != "" ? "," : "") + includeColumn);
                }
                string pattern =
                    " ;WITH list AS(SELECT Row_Number() OVER ( ORDER BY {0} {1}) AS RowNumber," + star + " FROM {2} ec " +
                    /*"{3}"+*/ " WHERE " +
                    oneOne + " ";
                var sb = new StringBuilder();

              
                sb.AppendFormat(pattern, Query.EncloseInBracket(query.SortOrderItem), query.SortOrderType, query.ViewName /*, join*/);
                string condition = GetSearchItemCondition(query);
                sb.Append(condition);
                sb.Append(" ) ");

                if (query.SupportPaging)
                {
                    int lastRecord = GetUpperPage(query.PageIndex, query.PageSize);
                    int fristRecord = GetLowerPage(query.PageIndex, query.PageSize);


                    sb.AppendFormat(" SELECT * FROM list WHERE RowNumber BETWEEN {0} AND {1} ", fristRecord, lastRecord);
                    sb.Append(GetSortItems(query.SortItems));
                    sb.AppendFormat(" SELECT COUNT(*) FROM {1} " + /*"{2}"+*/ " om WHERE " + oneOne + " {0} ",
                        condition,
                        query.ViewName /*,
                        join*/);

                }
                else
                {
                    sb.Append(" SELECT * FROM list");
                    sb.Append(GetSortItems(query.SortItems));
                    sb.Append(";");
                }
           
                if (query.AggregateColumns != null && query.AggregateColumns.Count > 0)
                {
                    const string aggregateTemplate = " {0}({1}) AS {1}, ";
                    string finalAggregateTemplate = " SELECT {0} '' AS nothing FROM {1} WHERE 1=1 {2} ";
                    var tempSb = new StringBuilder();
                    foreach (AggregateColumn aggregateColumn in query.AggregateColumns)
                    {
                        tempSb.AppendFormat(aggregateTemplate,
                            aggregateColumn.AggregateType,
                            aggregateColumn.ColumnName);
                    }
                    finalAggregateTemplate = string.Format(finalAggregateTemplate, tempSb, query.ViewName, condition);

                    if (!query.SupportPaging)
                    {
                        sb.Append(" SELECT 0 ");
                    }

                    sb.Append(finalAggregateTemplate);
                }

                return sb.ToString();
            }
            else
            {
                const string pattern = "SELECT TOP(1)  1 AS returned FROM {0} WHERE 1=1 {1}";
                var sb = new StringBuilder();
                string condition = GetSearchItemCondition(query);
                sb.AppendFormat(pattern, query.ViewName, condition);
                return sb.ToString();
            }
        }

        private static string GetSortItems(List<SortOrder> sortItems)
        {
            if (sortItems == null || !sortItems.Any())
                return string.Empty;
            var sortItem = sortItems.Aggregate(" ORDER BY ",
                (current, sortOrder) =>
                    current + (sortOrder.SortOrderItem + " " + sortOrder.SortOrderType.ToString().ToUpper() + ", "));
            return sortItem.Substring(0, sortItem.Length - 2);
        }

        private static string GetSearchItemCondition(Query query)
        {
            var sb = new StringBuilder();

            if (query.Conditions != null)
            {
                foreach (SearchItem seacrhItem in query.Conditions)
                {
                    string conditiontype = GetCondition(seacrhItem);

                    sb.Append(conditiontype);
                }
            }
            return sb.ToString();
        }

        public static string GetCondition(SearchItem searchItem)
        {
            string res = " ) ";
            if (searchItem.Item != null)
            {
                res = GetCondition(searchItem.Item) + res;
            }
            string conditiontype = string.Empty;

            if (searchItem.ValueInSearch is string)
            {
                switch (searchItem.ConditionStringType)
                {
                    case ConditionStringType.LikeBetween:
                        {
                            searchItem.ValueInSearch = Query.RemoveQuotationMarks(searchItem.ValueInSearch.ToString().SafePersianEncode());
                            conditiontype = "  {0} LIKE N'%{1}%' ";
                            break;
                        }
                    case ConditionStringType.NotLike:
                        {
                            searchItem.ValueInSearch = Query.RemoveQuotationMarks(searchItem.ValueInSearch.ToString().SafePersianEncode());
                            conditiontype = "  {0} NOT LIKE N'%{1}%' ";
                            break;
                        }
                    case ConditionStringType.LikeFrist:
                        {
                            searchItem.ValueInSearch = Query.RemoveQuotationMarks(searchItem.ValueInSearch.ToString().SafePersianEncode());
                            conditiontype = " {0} LIKE N'%{1}' ";
                            break;
                        }
                    case ConditionStringType.LikeLast:
                        {
                            searchItem.ValueInSearch = Query.RemoveQuotationMarks(searchItem.ValueInSearch.ToString().SafePersianEncode());
                            conditiontype = " {0} LIKE N'{1}%' ";
                            break;
                        }
                    case ConditionStringType.Equal:
                        {
                            searchItem.ValueInSearch = Query.RemoveQuotationMarks(searchItem.ValueInSearch.ToString().SafePersianEncode());
                            conditiontype = " {0} = N'{1}' ";
                            break;
                        }
                    case ConditionStringType.NotEqual:
                        {
                            searchItem.ValueInSearch = Query.RemoveQuotationMarks(searchItem.ValueInSearch.ToString().SafePersianEncode());
                            conditiontype = " {0} != N'{1}' ";
                            break;
                        }
                    case ConditionStringType.NotIn:
                        {
                            searchItem.ValueInSearch =
                                Query.FilterInBasedQueries(searchItem.ValueInSearch.ToString().SafePersianEncode());
                            conditiontype = " {0} NOT IN ({1}) ";
                            break;
                        }
                    case ConditionStringType.In:
                        {
                            searchItem.ValueInSearch =
                                Query.FilterInBasedQueries(searchItem.ValueInSearch.ToString().SafePersianEncode());
                            conditiontype = " {0}  IN ({1}) ";
                            break;
                        }
                }
            }

            if (searchItem.ValueInSearch is int || searchItem.ValueInSearch is Int16 ||
                searchItem.ValueInSearch is Int64 || searchItem.ValueInSearch is decimal ||
                searchItem.ValueInSearch is double || searchItem.ValueInSearch is float)
            {
                string conexpression = GetStringEquality(searchItem.ConditionEquality);
                conditiontype = "  {0} " + conexpression + " {1} ";
            }

            if (searchItem.ValueInSearch is DateTime)
            {
                string conexpression = GetStringEquality(searchItem.ConditionEquality);
                conditiontype = "  {0} " + conexpression + " '{1}' ";
            }

            if (searchItem.ValueInSearch is IList<int>)
            {
                var lst = (List<int>)searchItem.ValueInSearch;
                string conexpression = GetStringEquality(searchItem.ConditionEquality);
                string v = lst.Aggregate(string.Empty, (current, i) => current + string.Format(" {0},", i));
                v = v.TrimEnd(',');
                searchItem.ValueInSearch = v;
                conditiontype = "  {0} " + conexpression + " ( {1} ) ";
            }

            if (searchItem.ValueInSearch is IList<string>)
            {
                var lst = (List<string>)searchItem.ValueInSearch;
                string conexpression = GetStringEquality(searchItem.ConditionEquality);
                string v = lst.Aggregate(string.Empty, (current, i) => current + string.Format("'{0}',", i));
                v = v.TrimEnd(',');
                searchItem.ValueInSearch = v;
                conditiontype = "  {0} " + conexpression + " ( {1} ) ";
            }

            if (searchItem.ValueInSearch is BetweenDate)
            {
                //string conexpression = GetStringEquality(seacrhItem.ConditionEquality);
                conditiontype = "  {0} BETWEEN  {1} ";
            }

            object vinSearch = searchItem.ValueInSearch;
            if (searchItem.ValueInSearch is DateTime)
            {
                vinSearch = ((DateTime)searchItem.ValueInSearch).ToSqlDateTime();
            }

            if (searchItem.ValueInSearch is string)
            {
                if (string.IsNullOrEmpty(searchItem.ValueInSearch.ToString()))
                {
                    return string.Empty;
                }

                vinSearch = vinSearch.ToString().SafePersianEncode();
            }

            if (searchItem.ValueInSearch is NullValue)
            {
                if (((NullValue)searchItem.ValueInSearch) == NullValue.IsNull)
                {
                    vinSearch = " IS NULL ";
                }

                if (((NullValue)searchItem.ValueInSearch) == NullValue.IsNotNull)
                {
                    vinSearch = " IS NOT NULL ";
                }

                conditiontype = " {0} {1} ";
            }

            string m1 = string.Format(conditiontype, Query.EncloseInBracket(searchItem.ColumnName), vinSearch);

            return string.Format("  {0} ( {1} {2} ", searchItem.ConditionType, m1, res);
        }


        public static string GetStringEquality(ConditionEquality conditionEquality)
        {
            if (_stringEquality == null)
            {
                _stringEquality = new Dictionary<string, string>
                {
                    {ConditionEquality.Equality.ToString(), " = "},
                    {ConditionEquality.Greater.ToString(), " >  "},
                    {ConditionEquality.GreaterOrEqual.ToString(), " >= "},
                    {ConditionEquality.Less.ToString(), " < "},
                    {ConditionEquality.LessOrEqual.ToString(), " <= "},
                    {ConditionEquality.NotEqual.ToString(), " != "},
                    {ConditionEquality.In.ToString(), " IN "}
                };
            }
            return _stringEquality[conditionEquality.ToString()];
        }

        public static int GetUpperPage(int pageIndex, int pageSize)
        {
            return GetLowerPage(pageIndex, pageSize) + pageSize - 1;
        }

        public static int GetLowerPage(int pageSize, int pageIndex)
        {
            return (pageSize * pageIndex) + 1;
        }

        #region ExcelQuery

        public static string GetExcelQuery(ExcelQuery.ExcelQuery query)
        {
            string baseQuery = CreateQuery(query);
            return GetExcelQuery(query, baseQuery);
        }

        public static string GetExcelQuery(ExcelQuery.ExcelQuery query, string baseQuery)
        {

            string outPutHeader = string.Empty;
            string outPutBody = string.Empty;

            string withBody = "WITH cte AS (" + baseQuery + ")";

            foreach (ExcelQueryColumn col in query.ExcelColumns)
            {
                outPutHeader += "'" + col.ExcelColumn + "' AS 'th',";
                outPutBody += string.Format(" {0} AS 'td',", col.DataBaseColumn);
            }

            outPutHeader = outPutHeader.TrimEnd(',');
            outPutBody = outPutBody.TrimEnd(',');

            outPutHeader = "SELECT" + outPutHeader + " FOR XML RAW('tr'),ELEMENTS,TYPE";
            outPutHeader = "SELECT (" + outPutHeader + ") FOR XML RAW('thead'),ELEMENTS,TYPE";

            outPutBody = "SELECT " + outPutBody + " FROM cte FOR XML RAW('tr'),ELEMENTS,TYPE ";
            outPutBody = "SELECT (" + outPutBody + ") FOR XML RAW('tbody'),ELEMENTS,TYPE";
            string outPutQuery;
            if (query.CompressInDataBase && !string.IsNullOrEmpty(query.CompressFunctionName))
            {
                outPutQuery = withBody + " SELECT ref.fn_compress(CONVERT (varbinary(max),(SELECT '1' border,(" +
                              outPutHeader + "), (" + outPutBody + ") FOR XML RAW ('table'),TYPE)))";
            }
            else
            {
                outPutQuery = withBody + " SELECT (" + outPutHeader + "), (" + outPutBody +
                              ") FOR XML PATH ('table'),TYPE";
            }

            return outPutQuery;
        }

        private static string CreateQuery(ExcelQuery.ExcelQuery query)
        {
            const string pattern = @"SELECT ROW_NUMBER() OVER(ORDER BY {0} {1}) AS RowNumber,* FROM {3} WHERE 1=1 {2}  ";
            var sb = new StringBuilder();
            string cond = GetSearchItemCondition(query);
            sb.AppendFormat(pattern, Query.EncloseInBracket(query.SortOrderItem), query.SortOrderType, cond, query.ViewName);

            return sb.ToString();
        }

        #endregion

    }
}