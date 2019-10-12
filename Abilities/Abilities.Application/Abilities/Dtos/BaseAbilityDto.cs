using Abilities.Domain.Entities;

namespace Abilities.Application.Abilities.Dtos
{
    public class BaseAbilityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public BaseAbilityDto()
        { }

        public BaseAbilityDto(BaseAbility baseAbility)
        {
            Id = baseAbility.Id;
            Name = baseAbility.Name;
            Description = baseAbility.Description;
        }
    }
}
