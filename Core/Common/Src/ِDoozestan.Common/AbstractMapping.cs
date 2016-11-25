using System.Collections.Generic;

namespace Doozestan.Common
{
    public abstract class AbstractMapping<T, TR>
    {
        public abstract TR Map(T t);

        public virtual void Map(TR tr, T t)
        {

        }


        public abstract IEnumerable<TR> MapList(IEnumerable<T> list);

        public virtual void MapList(IEnumerable<TR> trList, IEnumerable<T> list)
        {

        }


    }
}
