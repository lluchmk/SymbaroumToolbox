using System.Collections.Generic;

namespace Abilities.Application.Abilities.Dtos
{
    public class AbilitiesListViewModel
    {
        public IEnumerable<AbilityDto> Abilities { get; set; }
        public IEnumerable<MysticalPowerDto> MysticalPowers { get; set; }
        public IEnumerable<RitualDto> Rituals { get; set; }
    }
}
