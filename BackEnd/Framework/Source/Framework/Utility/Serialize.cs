using System.Web.Script.Serialization;

namespace Framework.Utility
{
    public class SerializeDesrialize
    {
        public static string Serialize(object input)
        {
            return new JavaScriptSerializer().Serialize(input);
        }

        public static T Deserialize<T>(string des)
        {
            return new JavaScriptSerializer().Deserialize<T>(des);
        }

        public static object Deserialize (string des)
        {
            return new JavaScriptSerializer().DeserializeObject(des);
        }
    }
}
