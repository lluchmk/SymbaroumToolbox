using Abilities.Application.Abilities.Dtos;
using Abilities.Application.Abilities.Enums;
using Abilities.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public IEnumerable<Type> GetRequestedTypes()
        {
            List<Type> requestedTypes = new List<Type>();
            if (SearchAbilities())
            {
                requestedTypes.Add(typeof(Ability));
            }

            if (SearchMysticalPowers())
            {
                requestedTypes.Add(typeof(MysticalPower));
            }

            if (SearchRituals())
            {
                requestedTypes.Add(typeof(Ritual));
            }

            return requestedTypes;
        }

        private bool SearchAbilities() => !Types.Any() || Types.Any(t => t == AbilityType.Ability);
        private bool SearchMysticalPowers() => !Types.Any() || Types.Any(t => t == AbilityType.MysticalPower);
        private bool SearchRituals() => !Types.Any() || Types.Any(t => t == AbilityType.Ritual);
    }
}
