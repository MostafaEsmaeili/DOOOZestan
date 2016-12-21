using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Doozestan.UserManagement;
using Microsoft.AspNet.Identity;

namespace Doozestan.WebApi.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
       [System.Web.Mvc.HttpPost]
        [System.Web.Http.Route("CheckEmail")]

        public JsonResult CheckEmail(string email)
        {

            var user = AuthenticationManager.AuthenticationProvider.UserManager.FindByEmail(email);


            return Json(user == null);

        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Http.Route("CheckIfUserNameExist")]

        public JsonResult CheckIfUserNameExist(string UserName)
        {

            var user = AuthenticationManager.AuthenticationProvider.UserManager.FindByName(UserName);
            return Json(user == null);
        }
        [System.Web.Mvc.HttpPost]
        [System.Web.Http.Route("CheckPasswordValidation")]

        public JsonResult CheckPasswordValidation(string Password, string CurrentPassword)
        {
            return Json(AuthenticationManager.AuthenticationProvider.UserManager.PasswordValidator.ValidateAsync(Password).Result.Succeeded);
        }

    }
}