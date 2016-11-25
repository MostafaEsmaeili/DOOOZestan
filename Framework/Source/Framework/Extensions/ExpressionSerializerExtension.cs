using System.Linq.Expressions;
using System.Xml.Linq;
using ExpressionSerialization;

namespace Framework.Extensions
{
    public static class ExpressionSerializerExtension
    {
        public static string Serialize(this Expression expression)
        {
            return new ExpressionSerializer().Serialize(expression).ToString();
        }

        public static Expression Deserialize(this string expression)
        {
            return new ExpressionSerializer().Deserialize(XElement.Parse(expression));
        }
    }
}
