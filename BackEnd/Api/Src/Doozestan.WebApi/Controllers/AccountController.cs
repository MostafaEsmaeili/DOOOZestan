using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using System.Web.Mvc;
using Doozestan.Domain;
using Doozestan.UserManagement;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Doozestan.WebApi.Models;
using Doozestan.WebApi.Providers;
using Doozestan.WebApi.Results;

namespace Doozestan.WebApi.Controllers
{
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public AccountController()
        {
            
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        [System.Web.Http.Route("GetAllUsers")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult GetAllUsers()
        {
            var users = AuthenticationManager.AuthenticationProvider.GetApplicationUsers();
            return Json(users);
        }
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }


        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [System.Web.Http.Route("UserInfo")]
        [System.Web.Http.HttpPost]

        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            var user = AuthenticationManager.AuthenticationProvider.UserManager.FindByName(User.Identity.Name);
            return new UserInfoViewModel
            {
                Email = user.Email,

                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null

            };
        }


        [System.Web.Http.Route("Logout")]
        [System.Web.Http.HttpPost]

        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        [System.Web.Http.Route("ChangePassword")]
        [System.Web.Http.HttpPost]

        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }


        [System.Web.Http.Route("SetPassword")]
        [System.Web.Http.HttpPost]

        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }


        [System.Web.Http.Route("AddExternalLogin")]
        [System.Web.Http.HttpPost]

        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                && ticket.Properties.ExpiresUtc.HasValue
                && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return BadRequest("External login failure.");
            }

            ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return BadRequest("The external login is already associated with an account.");
            }

            IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }


        [System.Web.Http.OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            var user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                   OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("Register")]
      
        public IHttpActionResult Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AutoMapper.Mapper.CreateMap<RegisterBindingModel, Domain.ApplicationUser>()
               .ForMember(dest => dest.CreateDate,
                   opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.PasswordHash,
                   opt =>
                       opt.Ignore())
               .ForMember(x => x.EmailConfirmed, y => y.UseValue(true))
               .ForMember(x => x.PhoneNumberConfirmed, y => y.UseValue(true))
               .ForMember(x => x.Status, y => y.UseValue(1))
               .ForMember(x => x.LockoutEnabled, y => y.UseValue(false));


            var res = AutoMapper.Mapper.Map<RegisterBindingModel, ApplicationUser>(model);

            res.LockoutEnabled = false;

            AuthenticationManager.AuthenticationProvider.UserManager.Create(res, model.Password);
            _userManager.Create(res, model.Password);
            var addedUser = AuthenticationManager.AuthenticationProvider.UserManager.FindByName(model.UserName);

            //if (model. != null)
            //    AuthenticationManager.AuthenticationProvider.UserManager.AddToRoles(addedUser.Id,
            //        model.RolesCodeList.ToArray());
            addedUser.LockoutEnabled = false;
            AuthenticationManager.AuthenticationProvider.UserManager.Update(addedUser);

            //IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            //if (!result.Succeeded)
            //{
            //    return GetErrorResult(result);
            //}

            return Ok();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("CheckEmail")]

        public JsonResult<bool> CheckEmail(string email)
        {

            var user = AuthenticationManager.AuthenticationProvider.UserManager.FindByEmail(email);


            return Json(user == null);

        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("CheckIfUserNameExist")]

        public JsonResult<bool> CheckIfUserNameExist(string UserName)
        {

            var user = AuthenticationManager.AuthenticationProvider.UserManager.FindByName(UserName);
            return Json(user == null);
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("CheckPasswordValidation")]

        public JsonResult<bool> CheckPasswordValidation(string Password, string CurrentPassword)
        {
            return Json(AuthenticationManager.AuthenticationProvider.UserManager.PasswordValidator.ValidateAsync(Password).Result.Succeeded);
        }

        #endregion
    }
}
