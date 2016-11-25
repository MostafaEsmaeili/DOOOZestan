using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Framework.DataAccess.Query;

namespace Framework.DataAccess.Query.ExcelQuery
{
    [DataContract]
    public class ExcelQuery : Query
    {
        private string _compressFunctionName = "ref.fn_compress";

        public string CompressFunctionName
        {
            get
            {
                return _compressFunctionName;
            }
            set
            {
                _compressFunctionName = value;
            }
        }

        public bool CompressInDataBase { get; set; }
        public List<ExcelQueryColumn> ExcelColumns { get; set; }

        public ExcelQuery()
        {
            ExcelColumns = new List<ExcelQueryColumn>();
        }

        public ExcelQuery(Query query)
        {
            //Authorization = k2Query.Authorization;

            Conditions = query.Conditions;
            SupportPaging = false;
            SortOrderItem = query.SortOrderItem;
            SortOrderType = query.SortOrderType;
            ViewName = query.ViewName;
            SortItems = query.SortItems;
            ExcelColumns = new List<ExcelQueryColumn>();
        }

        public override string Parse()
        {
            return SqlGenerator.GetExcelQuery(this);
        }
    }
}
