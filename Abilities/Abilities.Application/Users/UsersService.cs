using Abilities.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Abilities.Application.Users
{
    public class UsersService : IUsersService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UsersService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool IsUserAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public string GetUserId()
        {
            return _contextAccessor.HttpContext.User.FindFirst("sub").Value;
        }
    }
}
