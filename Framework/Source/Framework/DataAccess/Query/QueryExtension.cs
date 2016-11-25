namespace Framework.DataAccess.Query
{
    public static class QueryExtension
    {
        private static bool IsNullOrEmpty(object value)
        {
            return value == null || string.IsNullOrEmpty(value.ToString());
        }
        private static bool AddCondition(object value, bool excludeNullValue)
        {
            return !IsNullOrEmpty(value) || !excludeNullValue;
        }
        public static Query LikeBetween(this Query query, string columnName, object value, bool doNotGenerateForNullValue)
        {
            //if (!IsNullOrEmpty(value) || !doNotGenerateForNullValue)
            if (AddCondition(value, doNotGenerateForNullValue))
            {
                var item = new SearchItem
                {
                    ColumnName = columnName,
                    ConditionEquality = ConditionEquality.Equality,
                    ConditionStringType = ConditionStringType.LikeBetween,
                    ConditionType = ConditionType.And,
                    ValueInSearch = value
                };
                query.AddCondition(item);
            }
            return query;
        }

        public static Query Eq(this Query query, string columnName, object value, bool doNotGenerateForNullValue = false, ConditionEquality conditionEquality = ConditionEquality.Equality)
        {
            //if (!IsNullOrEmpty(value) || !doNotGenerateForNullValue)
            if (AddCondition(value, doNotGenerateForNullValue))
            {
                var item = new SearchItem
                {
                    ColumnName = columnName,
                    ConditionEquality = conditionEquality,
                    ConditionStringType = ConditionStringType.Equal,
                    ConditionType = ConditionType.And,
                    ValueInSearch = value,

                };
                query.AddCondition(item);
            }
            return query;
        }

    }
}