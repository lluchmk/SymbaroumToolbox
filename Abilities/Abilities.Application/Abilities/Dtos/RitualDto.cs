using Abilities.Domain.Entities;
using Abilities.Domain.Enums;

namespace Abilities.Application.Abilities.Dtos
{
    public class RitualDto : BaseAbilityDto
    {
        public Tradition Tradition { get; set; }

        public RitualDto()
        { }

        public RitualDto(Ritual ritual)
            : base(ritual)
        {
            Tradition = ritual.Tradition;
        }
    }
}
