using System.Linq;
using Framework.Localizer.Interfaces;

namespace Framework.Validation.Model
{
    public static class BRuleExtension
    {
        public static string LocalizeMessage(this BusinessRuleException ex, ITranslator translator)
        {
            return ex.Parameters != null && ex.Parameters.Any()
                ? translator.Localize(string.Format(ex.Message, ex.Parameters.ToArray()))
                : translator.Localize(ex.Message);
        }
    }
}
