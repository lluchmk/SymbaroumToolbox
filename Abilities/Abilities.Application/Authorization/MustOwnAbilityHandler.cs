using Abilities.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using System.Threading.Tasks;

namespace Abilities.Application.Authorization
{
    public class MustOwnAbilityHandler : AuthorizationHandler<MustOwnAbilityRequirement>
    {
        private readonly IAbilitiesRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MustOwnAbilityHandler(IAbilitiesRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MustOwnAbilityRequirement requirement)
        {
            var routeAbilityId = _httpContextAccessor.HttpContext.GetRouteData().Values["abilityId"];
            var abilityId = int.Parse(routeAbilityId as string);

            var abilityOwnerId = context.User.Claims.First(c => c.Type == "sub").Value;

            if (!(await _repository.IsAbilityOwner(abilityId, abilityOwnerId)))
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }
    }
}
