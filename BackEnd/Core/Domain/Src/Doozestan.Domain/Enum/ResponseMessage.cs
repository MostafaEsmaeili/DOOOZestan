using System.ComponentModel;

namespace Doozestan.Domain.Enum
{
    public enum ResponseMessage
    {
        Unknwon = 0,
        [Description("Action Done Successfully")]
        ActionDoneSuccessfully = 1,
        [Description("Action Failed")]
        ActionFailed = 2,
    }
}