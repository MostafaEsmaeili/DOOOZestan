using System.Linq;
using Framework.Annotations;
using Framework.Validation.Model;

namespace Framework.Validation
{
    /// <summary>
    /// BusinessRule class
    /// </summary>
    public static class BRule
    {
        //private static ILog _logger;
        private const string DefaultErrorMessage = "business logic condition not satisfied";

        //private static ILog Logger
        //{
        //    get
        //    {
        //        if (_logger == null)
        //        {
        //            try
        //            {
        //                _logger = LogManager.GetCurrentClassLogger();
        //            }
        //            catch (Exception e)
        //            {
        //                Debug.Write(e.Message);
        //                _logger = new NoOpLogger();
        //            }
        //        }
        //        return _logger;
        //    }
        //}

        [AssertionMethod]
        public static void Assert([AssertionCondition(AssertionConditionType.IS_TRUE)]bool condition, string message, RuleExceptionCode code = RuleExceptionCode.Unknown, params object[] parameters)
        {
            Assert(condition, string.Empty, message, code, parameters.ToArray());
        }

        [AssertionMethod]
        public static void Assert([AssertionCondition(AssertionConditionType.IS_TRUE)]bool condition, string memberName, string message, RuleExceptionCode code = RuleExceptionCode.Unknown, params object[] parameters)
        {
            var exception = DoAssertion(condition, memberName, message, code, parameters.ToArray());

            if (exception == null)
                return;

            //Logger.Error(message);
            throw exception;
        }

        private static BusinessRuleException DoAssertion([AssertionCondition(AssertionConditionType.IS_TRUE)]bool condition, string memberName, string message, RuleExceptionCode code, params object[] parameters)
        {
            if (condition) return null;

            message = message == string.Empty ? DefaultErrorMessage : message;

            return new BusinessRuleException(memberName, message) { Code = code, Parameters = parameters.ToArray() };
        }
    }
}
