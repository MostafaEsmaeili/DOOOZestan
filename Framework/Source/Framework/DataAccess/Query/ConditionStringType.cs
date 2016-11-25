namespace Framework.DataAccess.Query
{
    public enum ConditionStringType
    {
        None = 0,
        Equal = 1,
        LikeBetween = 2,
        LikeFrist = 3,
        LikeLast = 4,
        DateBetween = 5,
        In = 6,
        NotIn = 7,
        NotEqual = 8,
        NotLike = 9
    }
}