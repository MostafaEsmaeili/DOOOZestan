using Doozestan.Domain.User;

namespace Doozestan.UserManagement.Mapper
{
    public class MapperHelper
    {
        public static ApplicationUser ApplicationUserMapper(ApplicationUserDTO applicationUserDto)
        {
            var applicationUser = new ApplicationUser
            {
                CreateDate = applicationUserDto.CreateDate,
                AccessFailedCount = applicationUserDto.AccessFailedCount,
                DisplayName = applicationUserDto.DisplayName,
                Email = applicationUserDto.Email,
                EmailConfirmed = applicationUserDto.EmailConfirmed,
                IsActive = applicationUserDto.IsActive,
                LockoutEnabled = applicationUserDto.LockoutEnabled,
                LockoutEndDateUtc = applicationUserDto.LockoutEndDateUtc,
                PasswordHash = applicationUserDto.PasswordHash,
                PhoneNumber = applicationUserDto.PhoneNumber,
                PhoneNumberConfirmed = applicationUserDto.PhoneNumberConfirmed,
                SecurityStamp = applicationUserDto.SecurityStamp,
                TwoFactorEnabled = applicationUserDto.TwoFactorEnabled,
                UserName = applicationUserDto.UserName
            };
            return applicationUser;
        }

        public static ApplicationUserDTO ApplicationUserDTOMapper(ApplicationUser applicationUser)
        {
            var applicationUserDto = new ApplicationUserDTO
            {
                Id = applicationUser.Id,
                CreateDate = applicationUser.CreateDate,
                AccessFailedCount = applicationUser.AccessFailedCount,
                DisplayName = applicationUser.DisplayName,
                Email = applicationUser.Email,
                EmailConfirmed = applicationUser.EmailConfirmed,
                IsActive = applicationUser.IsActive,
                LockoutEnabled = applicationUser.LockoutEnabled,
                LockoutEndDateUtc = applicationUser.LockoutEndDateUtc,
                PasswordHash = applicationUser.PasswordHash,
                PhoneNumber = applicationUser.PhoneNumber,
                PhoneNumberConfirmed = applicationUser.PhoneNumberConfirmed,
                SecurityStamp = applicationUser.SecurityStamp,
                TwoFactorEnabled = applicationUser.TwoFactorEnabled,
                UserName = applicationUser.UserName
            };
            return applicationUserDto;
        }

    }
}
