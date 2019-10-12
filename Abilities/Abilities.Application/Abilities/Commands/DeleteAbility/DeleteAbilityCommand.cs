using Abilities.Application.Abilities.Dtos;
using MediatR;

namespace Abilities.Application.Abilities.Commands.DeleteAbility
{
    public class DeleteAbilityCommand : IRequest<BaseAbilityDto>
    {
        public int AbilityId { get; set; }
    }
}
