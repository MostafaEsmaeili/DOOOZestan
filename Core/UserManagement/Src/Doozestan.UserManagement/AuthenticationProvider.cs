using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Doozestan.Common.WcfService;
using Doozestan.Domain;
using Doozestan.Domain.ServiceRequest.User;
using Doozestan.Domain.ServiceResponse.Identity;
using Doozestan.Domain.ServiceResponse.User;
using Doozestan.Domain.User;
using Doozestan.UserManagement.Login;
using Doozestan.UserManagement.Mapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.DataProtection;
using ApplicationUserRequest = Doozestan.UserManagement.User.ApplicationUserRequest;
using ApplicationUserResponse = Doozestan.UserManagement.User.ApplicationUserResponse;

namespace Doozestan.UserManagement
{
    public class AuthenticationProvider
    {
        public static IDataProtectionProvider DataProtectionProvider { get; set; }
        //public AuthenticationProvider()
        //    : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())), new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new CustomIdentityDbContext())), new CustomIdentityEmailService())
        //{
        //}

        public AuthenticationProvider()
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            
            UserManager = userManager;

            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public UserLoginResponse Login(UserLoginRequest request)
        {
            var x = new UserLoginResponse();

            //var r = RoleManager.Roles;
            var user = UserManager.Find(request.UserName, request.Password);
            if (user==null)
            {
                x.LoginStatusEnum = LoginStatusEnum.ExpectationFailed;
                x.ApplicationUser = null;
                return x;
            }
            if (user != null)
            {
                x.ApplicationUser = MapperHelper.ApplicationUserDTOMapper(user);
                if (user.EmailConfirmed && user.IsActive != null && (bool)user.IsActive && !user.LockoutEnabled)
                {
                    x.LoginStatusEnum = LoginStatusEnum.Ok;
                }
                else
                {
                    if (!user.EmailConfirmed)
                    {
                        x.LoginStatusEnum = LoginStatusEnum.BadRequest;
                     
                        x.ErrorList.Add("Email not Confirmed");

                    }
                    if (user.IsActive != null && !(bool)user.IsActive)
                    {
                        x.LoginStatusEnum = LoginStatusEnum.BadRequest;
                        x.ErrorList.Add("user not active");
                    }
                }
            }
            else
            {
                x.LoginStatusEnum = LoginStatusEnum.ExpectationFailed;
            }
            return x;
        }
        public ApplicationUserResponse CreateUser(Doozestan.Domain.ServiceRequest.User.ApplicationUserRequest user)
        {
            var response = new ApplicationUserResponse();
            try
            {
                if (user == null)
                {
                    response.ResponseMessage = "ApplicationUserRequest is null";
                    response.ResponseStatus = ResponseStatus.BadRequest;
                    return response;
                }
                if (user.ApplicationUser == null)
                {
                    response.ResponseMessage = "ApplicationUserDTO is null";
                    response.ResponseStatus = ResponseStatus.BadRequest;
                    return response;
                }
                //if (user.ApplicationUser.RoleId <= 0)//TODO Role Identity Is not impeliment right Now
                //{
                //    response.ResponseMessage = "RoleId is null";
                //    response.ResponseStatus = ResponseStatus.BadRequest;
                //    return response;
                //}
                var userApp = MapperHelper.ApplicationUserMapper(user.ApplicationUser);
                var res = UserManager.Create(userApp, user.ApplicationUser.Password);
                if (res.Errors.Count() > 0)
                {
                    response.ErrorList = res.Errors.ToList();
                    response.ResponseStatus = ResponseStatus.ExpectationFailed;
                }
                else
                {
                    //if (user.ApplicationUser.RoleId > 0)
                    {
                        var role = (Role)user.ApplicationUser.RoleId;
                        var r = RoleManager.FindByName(role.ToString());
                        if (r == null)
                        {
                            response.ResponseStatus = ResponseStatus.ExpectationFailed;
                            UserManager.Delete(userApp);
                        }
                        else
                        {
                            UserManager.AddToRole(userApp.Id, r.Name);
                            response.UserId = userApp.Id;
                            response.ResponseStatus = ResponseStatus.Ok;
                        }
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.ResponseStatus = ResponseStatus.ExpectationFailed;
                return response;
            }
        }
        public UserResponse GetUserById(UserRequest request)
        {
            var response = new UserResponse();
            try
            {

                var user = UserManager.FindById(request.UserId);
                response.ApplicationUserDto = MapperHelper.ApplicationUserDTOMapper(user);
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.ResponseStatus = ResponseStatus.ExpectationFailed;
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
                throw ex;
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
                throw ex;
            }
        }
        public ChangePasswordResponse ChangePassword(ChangePasswordRequest userChangePassword)
        {
            var response = new ChangePasswordResponse();
            try
            {
                var res = UserManager.ChangePasswordAsync(userChangePassword.UserId, userChangePassword.OldPassword, userChangePassword.NewPassword);

                if (res.Result.Errors.Count() > 0)
                {
                    response.ErrorList = res.Result.Errors.ToList();
                    response.ResponseStatus = ResponseStatus.ExpectationFailed;
                }
                else
                {
                    response.ResponseStatus = ResponseStatus.Ok;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.ResponseStatus = ResponseStatus.ExpectationFailed;
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
                    response.ErrorList = res.Result.Errors.ToList();
                    response.ResponseStatus = ResponseStatus.ExpectationFailed;
                }
                else
                {
                    response.UserId = userRoleDto.UserId;
                    response.ResponseStatus = ResponseStatus.Ok;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    response.ResponseMessage = (ex.InnerException).Message;
                }
                else
                {
                    response.ResponseMessage = ex.Message;

                }
                response.ResponseStatus = ResponseStatus.ExpectationFailed;
                return response;
            }
            return response;
        }
        public BaseResponse DeleteUser(UserRequest userRequest)
        {
            var response = new BaseResponse();
            try
            {
                var user = UserManager.FindByIdAsync(userRequest.UserId);
                var res = UserManager.DeleteAsync(user.Result);
                if (res.Result.Errors.Count() > 0)
                {
                    response.ErrorList = res.Result.Errors.ToList();
                    response.ResponseStatus = ResponseStatus.ExpectationFailed;
                }
                else
                {
                    response.ResponseStatus = ResponseStatus.Ok;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = ResponseStatus.ExpectationFailed;
                response.ResponseMessage = ex.Message;
                return response;
            }
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
                    response.ResponseMessage = "No Claim";
                    response.ResponseStatus = ResponseStatus.ExpectationFailed;
                    return response;
                }
                else
                {
                    var id = res.Result.Claims.Where(x => x.Type.Contains("nameidentifier"));
                    response.UserId = id.FirstOrDefault().Value;
                    response.ResponseStatus = ResponseStatus.Ok;
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                response.ResponseStatus = ResponseStatus.ExpectationFailed;
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
