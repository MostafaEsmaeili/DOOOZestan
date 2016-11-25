using System.Runtime.Remoting.Messaging;
using System.Web;

namespace Framework.Utility
{
    public class WebSafeCallContext
    {
        public static object GetData(string name)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx == null)
            {
                return CallContext.GetData(name);
            }
            else
            {
                return ctx.Items[name];
            }
        }

        public static void SetData(string name, object value)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx == null)
            {
                CallContext.SetData(name, value);
            }
            else
            {
                ctx.Items[name] = value;
            }
        }

        public static void FreeNamedDataSlot(string name)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx == null)
            {
                CallContext.FreeNamedDataSlot(name);
            }
            else
            {
                ctx.Items.Remove(name);
            }
        }

        
    }
}
