using Framework.DataAccess.Query;
using Framework.Utility;

namespace Framework.DataAccess.Query
{
    public class SortOrder
    {
        private SortDirection _sortOrderType = SortDirection.Asc;
        private string _sortOrderItem;

        public SortOrder()
        {
        }

        public SortOrder(string item, SortDirection direction)
        {
            SortOrderItem = item;
            _sortOrderType = direction;
        }

        public string SortOrderItem
        {
            get { return Framework.DataAccess.Query.Query.EncloseInBracket(_sortOrderItem); }
            set { _sortOrderItem = value; }
        }

        public SortDirection SortOrderType
        {
            get { return _sortOrderType; }
            set { _sortOrderType = value; }
        }

        public override string ToString()
        {
            return string.Format(" {0} {1}", SortOrderItem, SortOrderType);
        }
    }
}