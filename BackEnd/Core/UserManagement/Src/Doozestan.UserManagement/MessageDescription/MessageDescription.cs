using System.ComponentModel;

namespace Doozestan.UserManagement.MessageDescription
{
    public enum MessageDescription
    {
        [Description("User Status Is Faulted")]
        UserStatusIsFaulted = 1,
        [Description("User Status Is Ok")]
        UserStatusIsOk = 2,
        [Description("Email Not Confirmed")]
        EmailNotConfirmed = 3,
        [Description("User Not Active")]
        UserNotActive = 4,
        [Description("ApplicationUserRequest Is Null")]
        ApplicationUserRequestIsNull = 5,
        [Description("ApplicationUserDTO Is Null")]
        ApplicationUserDTOIsNull = 6,
        [Description("RoleId Is Null")]
        RoleIdIsNull = 7,
        [Description("User Creation Failed")]
        CreateUserFailed = 8,
        [Description("RoleNotFound")]
        RoleNotFound = 9,
        [Description("Transaction Failed")]
        TransactionFailed = 10,
        [Description("No Claim")]
        NoClaim = 11


    }
}
