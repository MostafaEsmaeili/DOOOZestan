using System.Data;
using Framework.Mapper;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Framework.DataAccess.Entlib
{
    public class CustomRowMapperForEntlib<T> : IRowMapper<T> where T : new()
    {
        private readonly PopulateMethodDelegate<T> _populateMethod;

        public CustomRowMapperForEntlib(string mapName)
        {
            _populateMethod = RowMapper.Instance.GetPopulateMethod<T>(mapName);
        }

        public CustomRowMapperForEntlib(PopulateMethodDelegate<T> mapDelegate)
        {
            _populateMethod = mapDelegate;
        }

        public T MapRow(IDataRecord reader)
        {
            //BRule.Assert(reader is IDataReader, "reader should be of type IDataReader", (int)BaseErrorCodes.UnknownError);
            return _populateMethod((IDataReader)reader);
        }
    }

    public static class ExtensionMapperForEntlib
    {
        public static IRowMapper<T> GetMapper<T>(this Database database, string mapName) where T : new()
        {
            return new CustomRowMapperForEntlib<T>(mapName);
        }

        public static IRowMapper<T> GetMapper<T>(this Database database, PopulateMethodDelegate<T> mapDelegate) where T : new()
        {
            return new CustomRowMapperForEntlib<T>(mapDelegate);
        }
    }
}
