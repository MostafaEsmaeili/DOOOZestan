namespace Framework.Utility
{
    public class ObjectMapper
    {
        public class BaseConverter
        {
            public static TDest ConvertSourceToDest<TSource, TDest>(TSource source)
            {
                var dest = AutoMapper.Mapper.Map<TSource, TDest>(source, x => x.CreateMissingTypeMaps = true);
                return dest;
            }

        }
    }
}
