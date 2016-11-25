namespace Framework.ErrorHandler.Exceptions
{
    public class PropertyValidatorError
    {
        public PropertyValidatorError(string propertyName, PropertyValidatorReason reason,string errorMessage)
        {
            ProperyName = propertyName;
            Reason = reason;
            ErrorMessage= errorMessage;
        }
        public PropertyValidatorError(string propertyName, PropertyValidatorReason reason):this(propertyName,reason,null)
        {
            
        }
        public string ProperyName { get; set; }
        public PropertyValidatorReason Reason { get; set; }
        public string ErrorMessage { get; set; }
    }
}