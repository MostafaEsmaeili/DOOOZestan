namespace Framework.Authorization
{
    public interface IAuthorizationProvider<in T>
    {
        bool CheckAccess(T context);
    }
}
