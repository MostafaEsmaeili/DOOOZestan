namespace Framework.Audit
{
    public interface IAuditProvider<in T>
    {
        void Audit(T context, params object[] parameters);
        void Clear();
    }
}
