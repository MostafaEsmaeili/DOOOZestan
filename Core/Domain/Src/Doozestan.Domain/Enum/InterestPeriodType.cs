using System.ComponentModel;

namespace Doozestan.Domain.Enum
{
    public enum InterestPeriodType
    {
        [Description("ماهانه")]
        Everymonth = 1,
        [Description("هر سه ماه")]
        EveryThreemonths = 3,
        [Description("هر شش ماه")]
        EverySixmonths = 6,
        [Description("بدون کوپن")]
        NoCoupons = -1
    }
}