using Abilities.Application.Abilities.Dtos;
using Abilities.Domain.Entities;
using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Abilities.Application.Tests.Abilities.Dtos
{
    public class DtosConstructorTests
    {
        private readonly IFixture _fixture;

        public DtosConstructorTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void BaseAbilityDtoConstructor_CorrectlyInitializesValues()
        {
            var baseAbility = _fixture.Create<BaseAbility>();

            var dto = new BaseAbilityDto(baseAbility);

            dto.Should().BeEquivalentTo(new BaseAbilityDto
            {
                Id = baseAbility.Id,
                Name = baseAbility.Name,
                Description = baseAbility.Description
            });
        }

        [Fact]
        public void AbilityDtoConstructor_CorrectlyInitializesValues()
        {
            var ability = _fixture.Create<Ability>();

            var dto = new AbilityDto(ability);

            dto.Should().BeEquivalentTo(new AbilityDto
            {
                Id = ability.Id,
                Name = ability.Name,
                Description = ability.Description,
                NoviceType = ability.Novice.Type,
                NoviceDescription = ability.Novice.Description,
                AdeptType = ability.Adept.Type,
                AdeptDescription = ability.Adept.Description,
                MasterType = ability.Master.Type,
                MasterDescription = ability.Master.Description
            });
        }

        [Fact]
        public void MysticalPowerDtoConstructor_CorrectlyInitializesValues()
        {
            var mysticalPower = _fixture.Create<MysticalPower>();

            var dto = new MysticalPowerDto(mysticalPower);

            dto.Should().BeEquivalentTo(new MysticalPowerDto
            {
                Id = mysticalPower.Id,
                Name = mysticalPower.Name,
                Description = mysticalPower.Description,
                NoviceType = mysticalPower.Novice.Type,
                NoviceDescription = mysticalPower.Novice.Description,
                AdeptType = mysticalPower.Adept.Type,
                AdeptDescription = mysticalPower.Adept.Description,
                MasterType = mysticalPower.Master.Type,
                MasterDescription = mysticalPower.Master.Description,
                Material = mysticalPower.Material
            });
        }

        [Fact]
        public void RitualDtoConstructor_CorrectlyInitializesValues()
        {
            var ritual = _fixture.Create<Ritual>();

            var dto = new RitualDto(ritual);

            dto.Should().BeEquivalentTo(new RitualDto
            {
                Id = ritual.Id,
                Name = ritual.Name,
                Description = ritual.Description,
                Tradition = ritual.Tradition
            });
        }
    }
}
