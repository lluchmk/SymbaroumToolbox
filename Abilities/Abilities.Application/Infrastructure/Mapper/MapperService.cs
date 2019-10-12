using Abilities.Application.Abilities.Commands.CreateAbility;
using Abilities.Application.Abilities.Commands.UpdateAbility;
using Abilities.Application.Abilities.Dtos;
using Abilities.Application.Abilities.Enums;
using Abilities.Application.Interfaces.Services;
using Abilities.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abilities.Application.Infrastructure.Mapper
{
    public class MapperService : IMapperService
    {
        public AbilitiesListViewModel MapEntitiesToAbilitiesListViewModel(IEnumerable<BaseAbility> allAbilities)
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

        public BaseAbility MapCreateAbilityCommandToEntity(CreateAbilityCommand command)
        {
            BaseAbility ability;
            switch (command.Type)
            {
                case AbilityType.Ability:
                    ability = CreateAbilityCommandToAbility(command);
                    break;
                case AbilityType.MysticalPower:
                    ability = CreateAbilityCommandToMysticalPower(command);
                    break;
                case AbilityType.Ritual:
                    ability = CreateAbilityCommandToRitual(command);
                    break;
                default:
                    throw new NotImplementedException("No handler for unrecognized ability type"); // TODO: Better exception? Should't happen with proper validation
            }

            return ability;
        }

        public BaseAbilityDto MapEntityToDto(BaseAbility baseAbility)
        {
            if (baseAbility.GetType() == typeof(Ability))
            {
                return new AbilityDto(baseAbility as Ability);
            }
            else if (baseAbility.GetType() == typeof(MysticalPower))
            {
                return new MysticalPowerDto(baseAbility as MysticalPower);
            }
            else
            {
                return new RitualDto(baseAbility as Ritual);
            }
        }

        public BaseAbility MapUpdateAbilityCommandToAbility(UpdateAbilityCommandBody requestBody, BaseAbility baseAbility)
        {
            var abilityType = baseAbility.GetType();

            baseAbility.Name = requestBody.Name ?? baseAbility.Name;
            baseAbility.Description = requestBody.Description ?? baseAbility.Description;

            if (abilityType == typeof(Ability) || abilityType == typeof(MysticalPower))
            {
                var ability = baseAbility as Ability;

                ability.Novice.Type = requestBody.NoviceType ?? ability.Novice.Type;
                ability.Novice.Description = requestBody.NoviceDescription ?? ability.Novice.Description;

                ability.Adept.Type = requestBody.AdeptType ?? ability.Adept.Type;
                ability.Adept.Description = requestBody.AdeptDescription ?? ability.Adept.Description;

                ability.Master.Type = requestBody.MasterType ?? ability.Master.Type;
                ability.Master.Description = requestBody.MasterDescription ?? ability.Master.Description;

                if (abilityType == typeof(MysticalPower))
                {
                    var mysticalPower = ability as MysticalPower;
                    mysticalPower.Material = requestBody.Material ?? mysticalPower.Material;
                }

            }
            else
            {
                var ritual = baseAbility as Ritual;
                ritual.Tradition = requestBody.Tradition ?? ritual.Tradition;
            }

            return baseAbility;
        }

        private TAbility CreateAbilityCommandToBaseAbility<TAbility>(CreateAbilityCommand command, TAbility destination)
            where TAbility : BaseAbility
        {
            destination.Name = command.Name;
            destination.Description = command.Description;

            return destination;
        }

        private Ability CreateAbilityCommandToAbility(CreateAbilityCommand command)
        {
            var ability = new Ability();
            CreateAbilityCommandToAbility(command, ability);
            return ability;
        }

        private Ability CreateAbilityCommandToAbility(CreateAbilityCommand command, Ability ability)
        {
            CreateAbilityCommandToBaseAbility(command, ability);

            ability.Novice = new AbilityTier
            {
                Type = command.NoviceType,
                Description = command.NoviceDescription
            };

            ability.Adept = new AbilityTier
            {
                Type = command.AdeptType,
                Description = command.AdeptDescription
            };

            ability.Master = new AbilityTier
            {
                Type = command.MasterType,
                Description = command.MasterDescription
            };

            return ability;
        }

        private MysticalPower CreateAbilityCommandToMysticalPower(CreateAbilityCommand command)
        {
            var mysticalPower = new MysticalPower();
            CreateAbilityCommandToAbility(command, mysticalPower);

            mysticalPower.Material = command.Material;

            return mysticalPower;
        }

        private Ritual CreateAbilityCommandToRitual(CreateAbilityCommand command)
        {
            var ritual = new Ritual();
            CreateAbilityCommandToBaseAbility(command, ritual);

            ritual.Tradition = command.Tradition;

            return ritual;
        }
    }
}
