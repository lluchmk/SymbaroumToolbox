using Abilities.Application.Abilities.Commands.CreateAbility;
using Abilities.Application.Abilities.Commands.DeleteAbility;
using Abilities.Application.Abilities.Commands.UpdateAbility;
using Abilities.Application.Abilities.Queries.SearchAbilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Abilities.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbilitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AbilitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> SearchAbilities([FromQuery]SearchAbilitiesQuery request)
        {
            return Ok(await _mediator.Send(request));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAbility([FromBody]CreateAbilityCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [Authorize(Policy = "MustOwnAbility")]
        [HttpPut("{abilityId}")]
        public async Task<IActionResult> UpdateAbility([FromRoute] int abilityId, [FromBody] UpdateAbilityCommandBody requestBody)
        {
            var request = new UpdateAbilityCommand
            {
                AbilityId = abilityId,
                Body = requestBody
            };

            return Ok(await _mediator.Send(request));
        }

        [Authorize(Policy = "MustOwnAbility")]
        [HttpDelete("{abilityId}")]
        public async Task<IActionResult> DeleteAbility([FromRoute] int abilityId)
        {
            var request = new DeleteAbilityCommand
            {
                AbilityId = abilityId
            };
            return Ok(await _mediator.Send(request));
        }
    }
}
