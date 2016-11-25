using System.Collections.Generic;

namespace Framework.Mapper
{
    public class RowMap
    {
        public string MapName { get; set; }
        public Dictionary<string, ColumnMap> ColumnMapsByPropertyName { get; set; }
    }
}
