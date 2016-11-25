namespace Doozestan.Common
{
    public abstract class BaseMapper<TInternal,TExternal>
    {
        public abstract TExternal Mapping(TInternal t);
        
        //TODO
        public abstract TInternal Mapping(TExternal t);

    }
}
