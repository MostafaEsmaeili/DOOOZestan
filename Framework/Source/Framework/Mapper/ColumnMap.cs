namespace Framework.Mapper
{
    public enum InnerMapType
    {
        None=0,
        External=1,
        Internal=2
    }
    public class ColumnMap
    {
        public string ColumnName { get; set; }
        public InnerMapType InnerType { get; set; }
        public string PropertyName { get; set; }
        public string InnerRowMapName { get; set; }
        public RowMap InternalRowMap { get; set; }
        public ColumnMap Clone()
        {
            return MemberwiseClone() as ColumnMap;
           
        }
    }
}
