using Abilities.Application.Abilities.Queries.Dtos;
using Abilities.Domain.Entities;
using System.Collections.Generic;

namespace Abilities.Application.Interfaces.Services
{
    public interface IMapperService
    {
        AbilitiesListViewModel MapToAbilitiesListViewModel(IEnumerable<BaseAbility> abilities);
    }
}
