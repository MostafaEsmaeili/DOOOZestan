using System.Data;

namespace Framework.Mapper
{
    public delegate T PopulateMethodDelegate<out T>(IDataReader dr);
}
