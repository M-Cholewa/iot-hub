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
        /// <param name="_context"></param>
        /// <returns></returns>
        public static User? GetUser(this ClaimsPrincipal controllerUser, IoTHubContext _context)
        {
            var userGuid = GetGuid(controllerUser);

            return _context.Users.FirstOrDefault(u => u.Id == userGuid);
        }


        /// <summary>
        /// Safely get a device if the user has access to it
        /// </summary>
        /// <param name="controllerUser"></param>
        /// <param name="deviceId"></param>
        /// <param name="_context"></param>
        /// <returns>The device if user has access to it</returns>
        public static Device? GetDevice(this ClaimsPrincipal controllerUser, Guid deviceId, IoTHubContext _context)
        {
            var user = controllerUser.GetUser(_context);

            if (user == null || user.Devices == null)
            {
                return null;
            }

            return user.Devices.First(d => d.Id == deviceId);
        }
    }
}
