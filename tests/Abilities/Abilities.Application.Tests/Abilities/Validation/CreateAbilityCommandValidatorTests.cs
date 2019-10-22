using Abilities.Application.Abilities.Commands.CreateAbility;
using Abilities.Application.Abilities.Enums;
using Abilities.Domain.Enums;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Abilities.Application.Tests.Abilities.Validation
{
    public class CreateAbilityCommandValidatorTests
    {
        [Fact]
        public void Validate_CorrectlyValidatesCommonParameters()
        {
            var request = new CreateAbilityCommand
            {
                Type = (AbilityType)8,
                Name = "",
                Description = "",
            };
            var validator = new CreateAbilityCommandValidator();

            var response = validator.Validate(request);

            using var asc = new AssertionScope();
            response.IsValid.Should().BeFalse();
            response.Errors.Should().Contain(e => e.PropertyName == nameof(CreateAbilityCommand.Type));
            response.Errors.Should().Contain(e => e.PropertyName == nameof(CreateAbilityCommand.Name));
            response.Errors.Should().Contain(e => e.PropertyName == nameof(CreateAbilityCommand.Description));
        }

        [Fact]
        public void Validate_CorrctlyValidatesAbilityRequest()
        {
            var request = new CreateAbilityCommand
            {
                Type = AbilityType.Ability,
                Name = "Test ability",
                Description = "Test description",
                NoviceType = (ActionType)9,
                NoviceDescription = "",
                AdeptType = (ActionType)9,
                AdeptDescription = "",
                MasterType = (ActionType)9,
                MasterDescription = ""
            };

            var validator = new CreateAbilityCommandValidator();

            var response = validator.Validate(request);

            using var asc = new AssertionScope();
            response.IsValid.Should().BeFalse();
            response.Errors.Should().Contain(e => e.PropertyName == nameof(CreateAbilityCommand.NoviceType));
            response.Errors.Should().Contain(e => e.PropertyName == nameof(CreateAbilityCommand.NoviceDescription));
            response.Errors.Should().Contain(e => e.PropertyName == nameof(CreateAbilityCommand.AdeptType));
            response.Errors.Should().Contain(e => e.PropertyName == nameof(CreateAbilityCommand.AdeptDescription));
            response.Errors.Should().Contain(e => e.PropertyName == nameof(CreateAbilityCommand.MasterType));
            response.Errors.Should().Contain(e => e.PropertyName == nameof(CreateAbilityCommand.MasterDescription));
        }

        [Fact]
        public void Validate_CorrctlyValidatesMysticalPowerRequest()
        {
            var request = new CreateAbilityCommand
            {
                Type = AbilityType.MysticalPower,
                Name = "Test mystical power",
                Description = "Test description",
                NoviceType = (ActionType)9,
                NoviceDescription = "",
                AdeptType = (ActionType)9,
                AdeptDescription = "",
                MasterType = (ActionType)9,
                MasterDescription = "",
                Material = ""
            };

            var validator = new CreateAbilityCommandValidator();

            var response = validator.Validate(request);

            using var asc = new AssertionScope();
            response.IsValid.Should().BeFalse();
            response.Errors.Should().Contain(e => e.PropertyName == nameof(CreateAbilityCommand.NoviceType));
            response.Errors.Should().Contain(e => e.PropertyName == nameof(CreateAbilityCommand.NoviceDescription));
            response.Errors.Should().Contain(e => e.PropertyName == nameof(CreateAbilityCommand.AdeptType));
            response.Errors.Should().Contain(e => e.PropertyName == nameof(CreateAbilityCommand.AdeptDescription));
            response.Errors.Should().Contain(e => e.PropertyName == nameof(CreateAbilityCommand.MasterType));
            response.Errors.Should().Contain(e => e.PropertyName == nameof(CreateAbilityCommand.MasterDescription));
            response.Errors.Should().Contain(e => e.PropertyName == nameof(CreateAbilityCommand.Material));
        }

        [Fact]
        public void Validate_CorrctlyValidatesRiatualRequest()
        {
            var request = new CreateAbilityCommand
            {
                Type = AbilityType.Ritual,
                Name = "Test mystical power",
                Description = "Test description",
                Tradition = (Tradition)7
            };

            var validator = new CreateAbilityCommandValidator();

            var response = validator.Validate(request);

            using var asc = new AssertionScope();
            response.IsValid.Should().BeFalse();
            response.Errors.Should().Contain(e => e.PropertyName == nameof(CreateAbilityCommand.Tradition));
        }
    }
}
