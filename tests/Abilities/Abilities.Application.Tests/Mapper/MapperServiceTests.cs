﻿using Abilities.Application.Abilities.Commands.CreateAbility;
using Abilities.Application.Abilities.Commands.UpdateAbility;
using Abilities.Application.Abilities.Dtos;
using Abilities.Application.Abilities.Enums;
using Abilities.Application.Infrastructure.Mapper;
using Abilities.Domain.Entities;
using Abilities.Domain.Enums;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Abilities.Application.Tests.Mapper
{
    public class MapperServiceTests
    {
        private readonly MapperService _sut;

        private readonly IFixture _fixture;

        public MapperServiceTests()
        {
            _fixture = new Fixture();

            _sut = new MapperService();
        }

        [Fact]
        public void MapEntitiesToAbilitiesListViewModel_CorrecltyMapsAbilitiesToAbilityDtos()
        {
            var abilities = _fixture.CreateMany<Ability>();
            var expectedDtos = abilities.Select(a => new AbilityDto(a));

            var response = _sut.MapEntitiesToAbilitiesListViewModel(abilities);

            var abilityDtos = response.Abilities;
            abilityDtos.Should().BeEquivalentTo(expectedDtos);
        }

        [Fact]
        public void MapEntitiesToAbilitiesListViewModel_CorrecltyMapsMysticalPowersToMysticalPowerDtos()
        {
            var mysticalPowers = _fixture.CreateMany<MysticalPower>();
            var expectedDtos = mysticalPowers.Select(a => new MysticalPowerDto(a));

            var response = _sut.MapEntitiesToAbilitiesListViewModel(mysticalPowers);

            var mysticalPowerDtos = response.MysticalPowers;
            mysticalPowerDtos.Should().BeEquivalentTo(expectedDtos);
        }

        [Fact]
        public void MapEntitiesToAbilitiesListViewModel_CorrecltyMapsRittualsToRitualDtos()
        {
            var rituals = _fixture.CreateMany<Ritual>();
            var expectedDtos = rituals.Select(a => new RitualDto(a));

            var response = _sut.MapEntitiesToAbilitiesListViewModel(rituals);

            var ritualDtos = response.Rituals;
            ritualDtos.Should().BeEquivalentTo(expectedDtos);
        }

        [Fact]
        public void MapCreateAbilityCommandToEntity_CorrectlyMapsToAbility()
        {
            var command = new CreateAbilityCommand
            {
                Type = AbilityType.Ability,
                Name = "TestAbility",
                Description = "Testing the ability mapper",
                NoviceType = ActionType.Free,
                NoviceDescription = "A novice free action",
                AdeptType = ActionType.Active,
                AdeptDescription = "An improved active ability",
                MasterType = ActionType.Reaction,
                MasterDescription = "Now it's a rection!"
            };

            var response = _sut.MapCreateAbilityCommandToEntity(command);

            response.Should().BeEquivalentTo(new Ability { 
                Name = command.Name,
                Description = command.Description,
                Novice = new AbilityTier
                {
                    Type = command.NoviceType,
                    Description = command.NoviceDescription
                },
                Adept = new AbilityTier
                {
                    Type = command.AdeptType,
                    Description = command.AdeptDescription
                },
                Master = new AbilityTier
                {
                    Type = command.MasterType,
                    Description = command.MasterDescription
                }
            });
        }

        [Fact]
        public void MapCreateAbilityCommandToEntity_CorrectlyMapsToMysticalPower()
        {
            var command = new CreateAbilityCommand
            {
                Type = AbilityType.MysticalPower,
                Name = "TestMysticalPower",
                Description = "Testing the mystical power mapper",
                NoviceType = ActionType.Active,
                NoviceDescription = "A novice active action",
                AdeptType = ActionType.OncePerAdventure,
                AdeptDescription = "An awesome but limited action",
                MasterType = ActionType.Reaction,
                MasterDescription = "Sweet reactions!",
                Material = "Sticks and stones"
            };

            var response = _sut.MapCreateAbilityCommandToEntity(command);

            response.Should().BeEquivalentTo(new MysticalPower
            {
                Name = command.Name,
                Description = command.Description,
                Novice = new AbilityTier
                {
                    Type = command.NoviceType,
                    Description = command.NoviceDescription
                },
                Adept = new AbilityTier
                {
                    Type = command.AdeptType,
                    Description = command.AdeptDescription
                },
                Master = new AbilityTier
                {
                    Type = command.MasterType,
                    Description = command.MasterDescription
                },
                Material = command.Material
            });
        }

        [Fact]
        public void MapCreateAbilityCommandToEntity_CorrectlyMapsToRitual()
        {
            var command = new CreateAbilityCommand
            {
                Type = AbilityType.Ritual,
                Name = "TestRitual",
                Description = "Testing the ritual mapper",
                Tradition = Tradition.Theurgy
            };

            var response = _sut.MapCreateAbilityCommandToEntity(command);

            response.Should().BeEquivalentTo(new Ritual
            {
                Name = command.Name,
                Description = command.Description,
                Tradition = command.Tradition
            });
        }

        [Fact]
        public void MapEntityToDto_CorrectlyMapsAbilityToAbilityDto()
        {
            var ability = new Ability
            {
                Name = "TestAbility",
                Description = "Testing the ability entity to DTO mapper",
                Novice = new AbilityTier
                {
                    Type = ActionType.Active,
                    Description = "A novice active action",

                },
                Adept = new AbilityTier
                {
                    Type = ActionType.OncePerAdventure,
                    Description = "An awesome but limited action",
                },
                Master = new AbilityTier
                {
                    Type = ActionType.Reaction,
                    Description = "Sweet reactions!",
                }
            };

            var response = _sut.MapEntityToDto(ability);

            response.Should().BeEquivalentTo(new AbilityDto(ability));
        }

        [Fact]
        public void MapEntityToDto_CorrectlyMapsMysticalPowerToMysticalPowerDto()
        {
            var mysticalPower = new MysticalPower
            {
                Name = "TestMysticalPower",
                Description = "Testing the mystical power entity to DTO mapper",
                Novice = new AbilityTier
                {
                    Description = "A novice active action",
                    Type = ActionType.Active,
                },
                Adept = new AbilityTier
                {
                    Type = ActionType.OncePerAdventure,
                    Description = "An awesome but limited action",
                },
                Master = new AbilityTier
                {
                    Type = ActionType.Reaction,
                    Description = "Sweet reactions!",
                },
                Material = "Sticks and stones"
            };

            var response = _sut.MapEntityToDto(mysticalPower);

            response.Should().BeEquivalentTo(new MysticalPowerDto(mysticalPower));
        }

        [Fact]
        public void MapEntityToDto_CorrectlyMapsRitualToRitualDto()
        {
            var ritual = new Ritual
            {
                Name = "TestRitual",
                Description = "Testing the ritual mapper",
                Tradition = Tradition.Theurgy
            };

            var response = _sut.MapEntityToDto(ritual);

            response.Should().BeEquivalentTo(new RitualDto(ritual));
        }

        [Fact]
        public void MapUpdateAbilityCommandToAbility_WhenTypeAbility_CorrecltyMapsToAbility()
        {
            var originalAbility = _fixture.Create<Ability>();

            var command = new UpdateAbilityCommandBody
            {
                Name = "TestAbility",
                Description = "Testing the ability update mapper",
                NoviceType = ActionType.Free,
                NoviceDescription = "A novice free action",
                AdeptType = ActionType.Active,
                AdeptDescription = "An improved active ability",
                MasterType = ActionType.Reaction,
                MasterDescription = "Now it's a rection!"
            };

            var response = _sut.MapUpdateAbilityCommandToAbility(command, originalAbility);

            response.Should().BeEquivalentTo(new Ability
            {
                Id = originalAbility.Id,
                UserId = originalAbility.UserId,
                Name = command.Name,
                Description = command.Description,
                Novice = new AbilityTier
                {
                    Type = command.NoviceType.Value,
                    Description = command.NoviceDescription
                },
                Adept = new AbilityTier
                {
                    Type = command.AdeptType.Value,
                    Description = command.AdeptDescription
                },
                Master = new AbilityTier
                {
                    Type = command.MasterType.Value,
                    Description = command.MasterDescription
                }
            });
        }

        [Fact]
        public void MapUpdateAbilityCommandToAbility_WhenTypeMysticalPower_CorrecltyMapsToMysticalPower()
        {
            var originalMysticalPower = _fixture.Create<MysticalPower>();

            var command = new UpdateAbilityCommandBody
            {
                Name = "TestMysticalPower",
                Description = "Testing the mystical power mapper",
                NoviceType = ActionType.Active,
                NoviceDescription = "A novice active action",
                AdeptType = ActionType.OncePerAdventure,
                AdeptDescription = "An awesome but limited action",
                MasterType = ActionType.Reaction,
                MasterDescription = "Sweet reactions!",
                Material = "Sticks and stones"
            };

            var response = _sut.MapUpdateAbilityCommandToAbility(command, originalMysticalPower);

            response.Should().BeEquivalentTo(new MysticalPower
            {
                Id = originalMysticalPower.Id,
                UserId = originalMysticalPower.UserId,
                Name = command.Name,
                Description = command.Description,
                Novice = new AbilityTier
                {
                    Type = command.NoviceType.Value,
                    Description = command.NoviceDescription
                },
                Adept = new AbilityTier
                {
                    Type = command.AdeptType.Value,
                    Description = command.AdeptDescription
                },
                Master = new AbilityTier
                {
                    Type = command.MasterType.Value,
                    Description = command.MasterDescription
                },
                Material = command.Material
            });
        }

        [Fact]
        public void MapUpdateAbilityCommandToAbility_WhenTypeRitual_CorrectlyMapsToRitual()
        {
            var originalRitual = _fixture.Create<Ritual>();

            var command = new UpdateAbilityCommandBody
            {
                Name = "TestRitual",
                Description = "Testing the ritual mapper",
                Tradition = Tradition.Theurgy
            };

            var response = _sut.MapUpdateAbilityCommandToAbility(command, originalRitual);

            response.Should().BeEquivalentTo(new Ritual
            {
                Id = originalRitual.Id,
                UserId = originalRitual.UserId,
                Name = command.Name,
                Description = command.Description,
                Tradition = command.Tradition.Value
            });
        }
    }
}
