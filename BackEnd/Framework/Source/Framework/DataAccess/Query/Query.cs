using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Framework.DataAccess.Query;

namespace Framework.DataAccess.Query
{
    [KnownType(typeof(NullValue))]
    public class Query
    {
        #region Fields

        public List<SortOrder> SortItems;

        #endregion Fields

        #region Constructors

        public Query()
        {
            SortOrderType = SortDirection.Asc;
            IsForCheckExistanceOnly = false;
            PageSize = 50;
            PageIndex = 0;
            Conditions = new List<SearchItem>();
            AggregateColumns = new List<AggregateColumn>();
            SupportPaging = true;
        }

        #endregion Constructors

        #region Properties

        public List<SearchItem> Conditions { get; set; }

        //public IAuthorizationCondition Authorization { get; set; }

        public List<string> IncludeColumns { get; set; }

        //public List<QueryJoinItem> ExtraJoinItems { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string SortOrderItem { get; set; }

        public SortDirection SortOrderType { get; set; }

        public bool SupportPaging { get; set; }

        public int TopResult { get; set; }

        public int TotalRecord { get; set; }

        public string ViewName { get; set; }

        public bool IsForCheckExistanceOnly { get; set; }

        public bool XmlOutPut { get; set; }

        public List<AggregateColumn> AggregateColumns { get; set; }

        #endregion Properties

        #region Methods

        public static string CleanSearchString(string searchString)
        {
            //return searchString;

            if (string.IsNullOrEmpty(searchString))
                return null;

            // Do wild card replacements
            //searchString = searchString.Replace("*", "%");

            // Strip any markup characters
            // searchString = Transforms.StripHtmlXmlTags(searchString);

            // Remove known bad SQL characters
            searchString = searchString.ToLower();
            searchString = Regex.Replace(searchString, "insert|update|union|waitfor|sleep|delay|benchmark|--|;|'|\"", " ", RegexOptions.Compiled | RegexOptions.Multiline);

            // Finally remove any extra spaces from the string
            //searchString = Regex.Replace(searchString, " {1,}", " ", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);

            return searchString;
        }

        public static SearchItem GetSearchItem(string columnName, object valueInSaearch)
        {
            return GetSearchItem(columnName, valueInSaearch, ConditionEquality.Equality);
        }

        public static SearchItem GetSearchItem(string columnName, object valueInSaearch, ConditionEquality conditionEquality)
        {
            var searchItem = new SearchItem();

            searchItem.ColumnName = columnName;
            searchItem.ValueInSearch = valueInSaearch;
            searchItem.ConditionEquality = conditionEquality;
            searchItem.ConditionType = ConditionType.And;
            searchItem.ConditionStringType = ConditionStringType.LikeBetween;

            return searchItem;
        }

        protected void AddAggregationColumns(AggregateColumn aggregateColumn)
        {
            if (aggregateColumn != null && aggregateColumn.AggregateValue != null)
            {
                if (AggregateColumns == null)
                {
                    AggregateColumns = new List<AggregateColumn>();
                }
                AggregateColumns.Add(aggregateColumn);
            }

        }

        protected internal void AddCondition(SearchItem searchItem)
        {
            if (searchItem != null && searchItem.ValueInSearch != null)
            {
                if (Conditions == null)
                {
                    Conditions = new List<SearchItem>();
                }
                Conditions.Add(searchItem);
            }

        }

        public virtual void PrepareQueryItems()
        {
        }

        public virtual string Parse()
        {

            return SqlGenerator.GetQuery(this);
        }

        #endregion Methods

        public static string RemoveQuotationMarks(string ValueInSearch)
        {
            return Regex.Replace(ValueInSearch, "'|\"", " ", RegexOptions.Compiled | RegexOptions.Multiline);
        }

        public static string FilterInBasedQueries(string ValueInSearch)
        {
            // it should be like x,y,z first we split it with ',' which are not in "" then we sanitize each part and remove problematic parts
            bool inSingleQuote = false;
            bool inDoubleQuote = false;
            List<string> eachPart = new List<string>();
            string part = "";
            for (int i=0;i<ValueInSearch.Length;i++)
            {
                if ( ValueInSearch[i] == '\"')
                    inDoubleQuote = !inDoubleQuote;
                if ( ValueInSearch[i] == '\'')
                    inSingleQuote = !inSingleQuote;
                if (ValueInSearch[i] == ',')
                    if (!inDoubleQuote && !inSingleQuote)
                    {
                        eachPart.Add(part);
                        part = "";
                    }
                    else part += ValueInSearch[i];
                else part += ValueInSearch[i];
            }
            if (part!="")
            {
                if (inSingleQuote)
                    part += "'";
                if (inDoubleQuote)
                    part += "\"";
                eachPart.Add(part);
            }

            List<string> SafeParts = new List<string>();
            foreach ( var tempPart in eachPart)
            {
                var trimmedTempPart = tempPart.Trim();
                if (trimmedTempPart.Length == 0)
                    continue;
                if (trimmedTempPart[0] == '\'' || trimmedTempPart[0] == '\"')
                {
                    if (trimmedTempPart[0] == trimmedTempPart[trimmedTempPart.Length - 1] && Regex.IsMatch(trimmedTempPart, "^[ \t]*[\'\"][^\'\"]*[\'\"][ \t]*$"))
                        SafeParts.Add(trimmedTempPart);
                }
                else
                    if (Regex.IsMatch(trimmedTempPart, @"^[ \t]*[0-9.]+([+*\-\/%][0-9.]+)*[ \t]*$"))
                        SafeParts.Add(trimmedTempPart);
            }
            return string.Join(",", SafeParts);
        }

        public static string EncloseInBracket(string columnName)
        {
            columnName = columnName.Replace("]", "").Replace("[", "");
            foreach (var c in "+-*/%")
                columnName = columnName.Replace(new string(c, 1), "]" + c + "[");
            return string.Format("[{0}]", columnName);
        }

    }

    //public class QueryJoinItem
    //{
    //    public string TableViewName { get; set; }
    //    public SearchItem JoinOnCondition { get; set; }
    //    public JoinType JoinType { get; set; }
    //    public JoinHint JoinHint { get; set; }
    //}

    //public enum JoinType
    //{
    //    Innerjoin,
    //    LeftOuterJoin,
    //    RightOuterJoin,
    //    FullOuterJoin
    //}

    //public enum JoinHint
    //{
    //    Default,
    //    LOOP,
    //    HASH,
    //    MERGE,
    //    REMOTE
    //}
}
