using Abilities.Domain.Enums;

namespace Abilities.Application.Abilities.Queries.Dtos
{
    public class RitualDto : BaseAbilityDto
    {
        public Tradition Tradition { get; set; }
    }
}
