namespace Framework.Validation
{
    public enum RuleExceptionCode
    {
        Unknown,
        SerialNumberNotFound,
        NeginAccountMapCodeForThisRowNumbersNotFound,
        MapperNotFound,
        ProviderNotFound,
        CanNotRegisterRequestMoreThanOnePerDay,
        PleaseEnterEmergencyPhone,
        RemainNotEnough,
        CreateBranchNotFound,
        AmountMustBeGreaterThanZero,
        RequestIsDuplicated,
        PleaseSetPerformDate
    }
}
