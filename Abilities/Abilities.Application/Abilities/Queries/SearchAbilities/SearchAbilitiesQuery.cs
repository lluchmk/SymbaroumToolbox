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
            Types = new List<SkillType>();
        }

        [FromQuery]
        public string Name { get; set; }
        [FromQuery]
        public IEnumerable<SkillType> Types { get; set; }

        public bool SearchAbilities() => !Types.Any() || Types.Any(t => t == SkillType.Ability);
        public bool SearchMysticalPowers() => !Types.Any() || Types.Any(t => t == SkillType.MysticalPower);
        public bool SearchRituals() => !Types.Any() || Types.Any(t => t == SkillType.Ritual);
    }
}
