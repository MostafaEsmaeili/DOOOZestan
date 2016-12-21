using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Doozestan.WebApi.Models
{
    // Models used as parameters to AccountController actions.

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        //[Required]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }
        //[ScaffoldColumn(false)]

       // public string Id { get; set; }
        [DisplayName("نام و نام خانوادگی")]
        public string DisplayName { get; set; }
        [ScaffoldColumn(false)]
        public int RoleId { get; set; }
        [DisplayName("تاریخ ثبت")]
        public string CreateDate { get; set; }

        [Required(ErrorMessage = " فیلد مورد نظر نمی تواند خالی باشد")]
        [DisplayName("ایمیل")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "ایمیل به درستی وارد نشده است.")]
        [Remote("CheckEmail", "Common", HttpMethod = "POST", ErrorMessage = " ایمیل مورد نظر قبلا انتخاب شده است.لطفا مجدد سعی نمایید")]

        public string Email { get; set; }
        [Display(Name = "شماره تلفن")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        //   [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { get; set; }

        [DisplayName("نام کاربری")]

        [Required(ErrorMessage = "لطفا نام کاربری را وارد نمایید")]
        [Remote("CheckIfUserNameExist", "Common", HttpMethod = "POST", ErrorMessage = "نام کاربری موجود است  لطفا نام کاربری دیگری انتخاب نمایید ")]
        public string UserName { get; set; }

        [Bindable(false)]
        [Required(ErrorMessage = "لطفا کلمه را وارد نمایید")]
        [DataType(DataType.Password)]
        [DisplayName("رمز عبور")]
        [MinLength(5, ErrorMessage = "رمز عبور باید حداقل شامل هفت کاراکتر باشد")]
        [Remote("CheckPasswordValidation", "Common", HttpMethod = "POST", ErrorMessage = "کلمه عبور باید حداقل شامل هفت کاراکتر ، اعداد حروف بزرگ و کوچک و همچنین کاراکترهای غیر متنی باشد")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [DisplayName("تایید رمز عبور")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "کلمه عبور مطابقت ندارد")]
        public string ConfirmPasswod { get; set; }


    }

    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
