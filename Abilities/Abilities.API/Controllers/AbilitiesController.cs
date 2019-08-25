using Abilities.Application.Abilities.Commands.CreateAbility;
using Abilities.Application.Abilities.Queries.SearchAbilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Abilities.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> CreateAbility([FromBody]CreateSkillCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        // TODO: Update PUT

        // TODO: Delete
    }
}
