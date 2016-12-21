using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Doozestan.Common;
using Doozestan.Common.WcfService;
using Doozestan.Domain;
using Doozestan.Domain.ServiceRequest.Login;
using Doozestan.Domain.ServiceRequest.User;
using Doozestan.Domain.ServiceResponse.Identity;
using Doozestan.Domain.ServiceResponse.Login;
using Doozestan.Domain.ServiceResponse.User;
using Doozestan.Domain.User;
using Doozestan.UserManagement.Mapper;
using Framework.Logging;
using Framework.Utility;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace Doozestan.UserManagement
{
    public class AspAuthenticationProvider : IAuthenticationProvider
    {
       private CustomLogger Logger=>new CustomLogger(GetType().FullName);
        public static IDataProtectionProvider DataProtectionProvider { get; set; }
        //public AspAuthenticationProvider()
        //    : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new CustomIdentityDbContext())), new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new CustomIdentityDbContext())))
        //{
        //}

        //public AspAuthenticationProvider()
        //    : base(store)
        //{
        //}

        public AspAuthenticationProvider()

        {
            var userManager = ApplicationUserManager.Create(new IdentityFactoryOptions<ApplicationUserManager>(),
                new OwinContext());
            //IdentityFactoryOptions<UserManager<ApplicationUser>> options = new IdentityFactoryOptions<UserManager<ApplicationUser>>();
            ////StartupOwin.DataProtectionProvider = new DpapiDataProtectionProvider("AssetMgn");
            //userManager.PasswordValidator = new PasswordValidator
            //{
            //    RequiredLength = 7,
            //    RequireNonLetterOrDigit = false,
            //    RequireDigit = true,
            //    RequireLowercase = true,
            //    RequireUppercase = true,
            //};

            ////var dataProtectorProvider = StartupOwin.DataProtectionProvider;
            //var dataProtectorProvider = options.DataProtectionProvider;

            ////var dataProtector = dataProtectorProvider.Create("AssetMgn User Identity");
            //userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, string>(dataProtectorProvider.Create())
            //{
            //    TokenLifespan = TimeSpan.FromHours(24)
            //};

            //userManager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(45);
            //userManager.UserLockoutEnabledByDefault = true;
            //userManager.UserValidator = new UserValidator<ApplicationUser>(new UserManager<ApplicationUser, string>(new UserStore<ApplicationUser>()))
            //{
            //    RequireUniqueEmail = true
            //};

            //userManager.EmailService = service;
            UserManager = userManager;
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new DoozestanDbContext()));
        }

        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public UserManager<ApplicationUser> UserManager { get; private set; }

        public UserLoginResponse Authenticate(UserLoginRequest request)
        {
            var x = new UserLoginResponse();

            //var r = RoleManager.Roles;
            var user = UserManager.Find(request.UserName, request.Password);
            if (user == null)
            {

                x.ResponseStatus=ResponseStatus.ExpectationFailed;
                x.ResponseMessage =
                    MessageDescription.MessageDescription.ApplicationUserRequestIsNull.GetEnumDescription();
                x.ApplicationUser = null;
                return x;
            }
            x.ApplicationUser = MapperHelper.ApplicationUserDTOMapper(user);
            if (user.EmailConfirmed && user.Status != null && user.Status == 1 && !user.LockoutEnabled)
            {
                x.ResponseStatus = ResponseStatus.Ok;

                x.ResponseMessage = MessageDescription.MessageDescription.UserStatusIsOk.GetDescription();

            }
            else
            {
                if (!user.EmailConfirmed)
                {
                    x.ResponseStatus = ResponseStatus.Ok;

                    x.ResponseMessage = MessageDescription.MessageDescription.EmailNotConfirmed.GetDescription();

                }
                
            }
            if (user.Status != null && user.Status == 1)
            {
                x.ResponseStatus = ResponseStatus.BadRequest;
                x.ResponseMessage = MessageDescription.MessageDescription.UserNotActive.GetDescription();


            }


            else
            {
                x.ResponseStatus = ResponseStatus.ExpectationFailed;
                x.ResponseMessage = MessageDescription.MessageDescription.UserNotActive.GetDescription();
            }
            return x;
        }
        public ApplicationUserResponse CreateUser(ApplicationUserRequest user)
        {
            var response = new ApplicationUserResponse();
            try
            {
                if (!UserCreationValidation(user, response))
                {
                    return response;
                }
                var userApp = MapperHelper.ApplicationUserMapper(user.ApplicationUser);
                var res = UserManager.Create(userApp, user.ApplicationUser.Password);
                if (res.Errors.Count() > 0)
                {

                    response.ResponseStatus = ResponseStatus.BadRequest;
                    response.ResponseMessage = MessageDescription.MessageDescription.CreateUserFailed.GetDescription();

                }
                else
                {
                    if (user.ApplicationUser.RoleId > 0)
                    {
                        //TODO:
                        var role = user.ApplicationUser.RoleId;
                        var r = RoleManager.FindById(role.ToString());
                        if (r == null)
                        {
                            response.ResponseStatus = ResponseStatus.BadRequest;
                            response.ResponseMessage = MessageDescription.MessageDescription.RoleNotFound.GetDescription();
                            UserManager.Delete(userApp);
                        }
                        else
                        {
                            UserManager.AddToRole(userApp.Id, r.Name);
                            response.UserId = userApp.Id;
                            response.ResponseStatus = ResponseStatus.Ok;
                            response.ResponseMessage = MessageDescription.MessageDescription.UserStatusIsOk.GetDescription();
                       
                        }
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatus.ExpectationFailed;
                response.ResponseMessage = MessageDescription.MessageDescription.TransactionFailed.GetDescription();
                Logger.ErrorException(ex.Message,ex);
                return response;
            }
        }

        private static bool UserCreationValidation(ApplicationUserRequest user, ApplicationUserResponse response)
        {
            var isOk = true;
            if (user == null)
            {
                isOk = false;

                response.ResponseStatus = ResponseStatus.BadRequest;
                response.ResponseMessage = MessageDescription.MessageDescription.CreateUserFailed.GetDescription();
           
            }
            else if (user.ApplicationUser == null)
            {
                isOk = false;
                response.ResponseStatus = ResponseStatus.BadRequest;
                response.ResponseMessage = MessageDescription.MessageDescription.ApplicationUserDTOIsNull.GetDescription();
       
            }
            else if (user.ApplicationUser.RoleId <= 0)
            {
                isOk = false;
                response.ResponseStatus = ResponseStatus.BadRequest;
                response.ResponseMessage = MessageDescription.MessageDescription.CreateUserFailed.GetDescription();
       
            }
            return isOk;
        }

        public UserResponse GetUserById(UserRequest request)
        {
            var response = new UserResponse();
            try
            {
                var user = UserManager.FindById(request.UserId);
                response.ApplicationUserDto = MapperHelper.ApplicationUserDTOMapper(user);
                response.ResponseStatus = ResponseStatus.Ok;
            
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseMessage = MessageDescription.MessageDescription.TransactionFailed.GetDescription();
                response.ResponseStatus = ResponseStatus.ExpectationFailed;
                Logger.ErrorException(ex.Message, ex);

                return response;
            }
        }

        public ApplicationUser GetUserById(string userId)
        {
            try
            {
                var user = UserManager.FindById(userId);
                return user;
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);

                throw ;
            }
        }

        public ApplicationUser GetUserByName(string userName)
        {
            try
            {
                var user = UserManager.FindByName(userName);
                return user;
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);

                throw ;
            }
        }

        public IdentityRole GetUserRolesById(string userId)
        {
            try
            {
                var role = UserManager.FindById(userId).Roles.First();
                return RoleManager.FindById(role.RoleId);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);

                throw ;
            }
        }
        public ChangePasswordResponse ChangePassword(ChangePasswordRequest userChangePassword)
        {
            var response = new ChangePasswordResponse();
            try
            {
                var res = UserManager.ChangePasswordAsync(userChangePassword.UserId, userChangePassword.OldPassword, userChangePassword.NewPassword);

                if (res.Result.Errors.Any())
                {
                    response.ResponseStatus = ResponseStatus.ExpectationFailed;
                    response.ResponseMessage = MessageDescription.MessageDescription.TransactionFailed.GetDescription();
      
                }
                else
                {
                    response.ResponseStatus = ResponseStatus.Ok;
                    response.ResponseMessage = MessageDescription.MessageDescription.UserStatusIsOk.GetDescription();
                }
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatus.ExpectationFailed;
                response.ResponseMessage = MessageDescription.MessageDescription.TransactionFailed.GetDescription();
                Logger.ErrorException(ex.Message, ex);

                return response;
            }
        }
        public UserRoleResponse AddUserToRole(UserRoleRequest userRoleDto)
        {
            var response = new UserRoleResponse();
            try
            {
                var res = UserManager.AddToRoleAsync(userRoleDto.UserId, userRoleDto.Role.ToString());
                if (res.Result.Errors.Count() > 0)
                {
                    response.ResponseStatus = ResponseStatus.BadRequest;
                    response.ResponseMessage = MessageDescription.MessageDescription.TransactionFailed.GetDescription();
                 
                   
                }
                else
                {
                    response.UserId = userRoleDto.UserId;
                    response.ResponseStatus = ResponseStatus.Ok;
                
                }
            }
            catch (Exception ex)
            {
                    response.ResponseMessage = MessageDescription.MessageDescription.TransactionFailed.GetDescription();
                response.ResponseStatus =ResponseStatus.ExpectationFailed;
                Logger.ErrorException(ex.Message, ex);

                return response;
            }
            return response;
        }

        public BaseServiceResponse DeleteUser(UserRequest userRequest)
        {
            var response = new BaseServiceResponse();
            try
            {
                var user = UserManager.FindByIdAsync(userRequest.UserId);
                var res = UserManager.DeleteAsync(user.Result);
                if (res.Result.Errors.Count() > 0)
                {
                    response.ResponseStatus = ResponseStatus.BadRequest;

                }
                else
                {
                    response.ResponseStatus = ResponseStatus.Ok;

                }
                return response;
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);

                response.ResponseStatus = ResponseStatus.BadRequest;

            }
           
            return response;
        }



        public ClaimsIdentityResponse CreateIdentity(UserRequest request)
        {
            var response = new ClaimsIdentityResponse();
            try
            {
                var userApp = UserManager.FindById(request.UserId);
                var res = UserManager.CreateIdentityAsync(userApp, DefaultAuthenticationTypes.ApplicationCookie);
                if (res.Result == null)
                {

                    response.ResponseStatus = ResponseStatus.BadRequest;
                    response.ResponseMessage = MessageDescription.MessageDescription.NoClaim.GetDescription();
            
                    return response;
                }
                else
                {
                    var id = res.Result.Claims.Where(x => x.Type.Contains("nameidentifier"));
                    response.UserId = id.FirstOrDefault()?.Value;
                    response.ResponseStatus = ResponseStatus.Ok;
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatus.ExpectationFailed;
                response.ResponseMessage = MessageDescription.MessageDescription.TransactionFailed.GetDescription();
                Logger.ErrorException(ex.Message, ex);

                return response;
            }
        }

        public List<ApplicationUserDTO> GetApplicationUsers()
        {
            var lst = UserManager.Users.AsNoTracking();
            return lst.ToList().Select(MapperHelper.ApplicationUserDTOMapper).ToList();
        }
    }
}
