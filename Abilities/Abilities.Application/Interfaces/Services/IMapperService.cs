﻿using Abilities.Application.Abilities.Commands.CreateAbility;
using Abilities.Application.Abilities.Commands.UpdateAbility;
using Abilities.Application.Abilities.Dtos;
using Abilities.Domain.Entities;
using System.Collections.Generic;

namespace Abilities.Application.Interfaces.Services
{
    public interface IMapperService
    {
        AbilitiesListViewModel MapEntitiesToAbilitiesListViewModel(IEnumerable<BaseAbility> abilities);

        BaseAbility MapCreateAbilityCommandToEntity(CreateAbilityCommand command);

        BaseAbilityDto MapEntityToDto(BaseAbility baseAbility);

        BaseAbility MapUpdateAbilityCommandToAbility(UpdateAbilityCommandBody requestBody, BaseAbility baseAbility);
    }
}
