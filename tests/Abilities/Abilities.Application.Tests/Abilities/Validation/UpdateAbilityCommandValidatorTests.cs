using Abilities.Application.Abilities.Commands.UpdateAbility;
using Abilities.Domain.Enums;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Abilities.Application.Tests.Abilities.Validation
{
    public class UpdateAbilityCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Validate_CorrectlyValidatesAbilityId(int abilityId)
        {
            var request = new UpdateAbilityCommand
            {
                AbilityId = abilityId,
                Body = new UpdateAbilityCommandBody()
            };
            var validator = new UpdateAbilityCommandValidator();

            var response = validator.Validate(request);

            using var asc = new AssertionScope();
            response.IsValid.Should().BeFalse();
            response.Errors.Should().Contain(e => e.PropertyName == nameof(UpdateAbilityCommand.AbilityId));
        }

        [Fact]
        public void Validate_CorrectlyValidatesBody()
        {
            var request = new UpdateAbilityCommand()
            {
                AbilityId = 1,
                Body = new UpdateAbilityCommandBody
                {
                    Tradition = (Tradition)9,
                    NoviceType = (ActionType)9,
                    AdeptType = (ActionType)9,
                    MasterType = (ActionType)9
                }
            };
            var validator = new UpdateAbilityCommandValidator();

            var response = validator.Validate(request);
            
            using var asc = new AssertionScope();
            response.IsValid.Should().BeFalse();

            var bodyPropertyName = nameof(UpdateAbilityCommand.Body);
            var traditionPropertyName = $"{bodyPropertyName}.{nameof(UpdateAbilityCommandBody.Tradition)}";
            response.Errors.Should().Contain(e => e.PropertyName == traditionPropertyName);

            var noviceTypePropertyName = $"{bodyPropertyName}.{nameof(UpdateAbilityCommandBody.NoviceType)}";
            response.Errors.Should().Contain(e => e.PropertyName == noviceTypePropertyName);

            var adeptTypePropertyName = $"{bodyPropertyName}.{nameof(UpdateAbilityCommandBody.AdeptType)}";
            response.Errors.Should().Contain(e => e.PropertyName == adeptTypePropertyName);

            var masterTypePropertyName = $"{bodyPropertyName}.{nameof(UpdateAbilityCommandBody.MasterType)}";
            response.Errors.Should().Contain(e => e.PropertyName == masterTypePropertyName);
        }
    }
}
