using Abilities.Application.Abilities.Enums;
using Abilities.Application.Abilities.Queries.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Abilities.Application.Abilities.Queries.SearchAbilities
{
    public class SearchAbilitiesQuery : IRequest<AbilitiesListViewModel>
    {
        public SearchAbilitiesQuery()
        {
            Types = new List<AbilityType>();
        }

        [FromQuery]
        public string Name { get; set; }
        [FromQuery]
        public IEnumerable<AbilityType> Types { get; set; }

        public bool SearchAbilities() => !Types.Any() || Types.Any(t => t == AbilityType.Ability);
        public bool SearchMysticalPowers() => !Types.Any() || Types.Any(t => t == AbilityType.MysticalPower);
        public bool SearchRituals() => !Types.Any() || Types.Any(t => t == AbilityType.Ritual);
    }
}
