using System.Collections.Generic;

namespace Framework.DataAccess.Query
{
    public class AggregateColumn
    {
        public string ColumnName { get; set; }
        public AggregateType AggregateType { get; set; }
        public object AggregateValue { get; set; }
        public List<GroupByRow> GroupByRows
        {
            get;
            set;
        }
    }

    public class GroupByRow
    {
        public string ColumnName { get; set; }
        public object Value { get; set; }        
    }
}