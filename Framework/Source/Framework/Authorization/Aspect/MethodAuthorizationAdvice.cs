namespace Framework.Authorization.Aspect
{
    //public class MethodAuthorizationAdvice : IMethodInterceptor
    //{
    //    protected IAuthorizationProvider<GeneralContext> AuthorizationProvider { get; set; }

    //    public object Invoke(IMethodInvocation invocation)
    //    {
    //        var authorizeAttributes = invocation.Method.GetCustomAttributes(typeof(AuthorizeAttribute), true).Select(x => x as AuthorizeAttribute);
            
    //        if (!authorizeAttributes.Any())
    //            return invocation.Proceed();

    //        var methodContext = new GeneralContext
    //        {
    //            Principal = Thread.CurrentPrincipal,
    //            OperationIdList = authorizeAttributes.Select(x=> x.OperationId)
    //        };

    //        //return false;
    //        if (AuthorizationProvider.CheckAccess(methodContext))
    //            return invocation.Proceed();

    //        throw new System.Security.SecurityException("Access Denied");
    //    }
    //}
}
