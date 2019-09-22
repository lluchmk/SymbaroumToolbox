using Abilities.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace Abilities.Application.Authorization
{
    public class MustOwnAbilityHandler : AuthorizationHandler<MustOwnAbilityRequirement>
    {
        private readonly IAbilitiesRepository _repository;

        public MustOwnAbilityHandler(IAbilitiesRepository repository)
        {
            _repository = repository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MustOwnAbilityRequirement requirement)
        {
            var filterContext = context.Resource as AuthorizationFilterContext;
            if (filterContext is null)
            {
                context.Fail();
                return;
            }

            var abilityId = (int)filterContext.RouteData.Values["abilityId"];
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
