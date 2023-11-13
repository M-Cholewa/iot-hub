using Business.Repository;
using Domain.Core;
using Domain.Data;
using iot_hub_backend.Infrastructure.Security;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace iot_hub_backend.Infrastructure.Extensions
{
    public static class UserExtensions
    {
        public static Guid GetGuid(this ClaimsPrincipal controllerUser)
        {
            try
            {
                return Guid.Parse(controllerUser.Claims.First(c => c.Type == ClaimNames.UserId).Value);
            }
            catch
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Get the user from the claims principal
        /// </summary>
        /// <param name="controllerUser"></param>
        /// <param name="_userRepository"></param>
        /// <returns></returns>
        public async static Task<User?> GetUser(this ClaimsPrincipal controllerUser, UserRepository _userRepository)
        {
            var userGuid = GetGuid(controllerUser);

            return await _userRepository.GetByIdAsync(userGuid);
        }


        /// <summary>
        /// Safely get a device if the user has access to it
        /// </summary>
        /// <param name="controllerUser"></param>
        /// <param name="deviceId"></param>
        /// <param name="_context"></param>
        /// <returns>The device if user has access to it</returns>
        public async static Task<Device?> GetDevice(this ClaimsPrincipal controllerUser, Guid deviceId, UserRepository _userRepository)
        {
            var user = await controllerUser.GetUser(_userRepository);

            if (user == null || user.Devices == null)
            {
                return null;
            }

            return user.Devices.First(d => d.Id == deviceId);
        }
    }
}
