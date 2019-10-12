using Abilities.Domain.Entities;

namespace Abilities.Application.Abilities.Dtos
{
    public class MysticalPowerDto : AbilityDto
    {
        public string Material { get; set; }

        public MysticalPowerDto(MysticalPower mysticalPower)
            : base(mysticalPower)
        {
            Material = mysticalPower.Material;
        }
    }
}
