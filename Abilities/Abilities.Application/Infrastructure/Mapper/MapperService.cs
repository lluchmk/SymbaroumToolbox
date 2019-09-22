using Abilities.Application.Abilities.Queries.Dtos;
using Abilities.Application.Interfaces.Services;
using Abilities.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Abilities.Application.Infrastructure.Mapper
{
    public class MapperService : IMapperService
    {
        public AbilitiesListViewModel MapToAbilitiesListViewModel(IEnumerable<BaseAbility> allAbilities)
        {
            var abilities = allAbilities.Where(a => a.GetType() == typeof(Ability))
                .Cast<Ability>()
                .Select(a => new AbilityDto(a));

            var mysticalPowers = allAbilities.Where(a => a.GetType() == typeof(MysticalPower))
                .Cast<MysticalPower>()
                .Select(m => new MysticalPowerDto(m));

            var rituals = allAbilities.Where(a => a.GetType() == typeof(Ritual))
                .Cast<Ritual>()
                .Select(r => new RitualDto(r));

            var viewModel = new AbilitiesListViewModel
            {
                Abilities = abilities,
                MysticalPowers = mysticalPowers,
                Rituals = rituals
            };

            return viewModel;
        }
    }
}
