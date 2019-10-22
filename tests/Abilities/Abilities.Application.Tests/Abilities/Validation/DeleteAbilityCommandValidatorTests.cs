using Abilities.Application.Abilities.Commands.DeleteAbility;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Abilities.Application.Tests.Abilities.Validation
{
    public class DeleteAbilityCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Validate_CorrecltyValidates(int abilityId)
        {
            var request = new DeleteAbilityCommand()
            {
                AbilityId = abilityId
            };
            var validator = new DeleteAbilityCommandValidator();

            var response = validator.Validate(request);

            using var asc = new AssertionScope();
            response.IsValid.Should().BeFalse();
            response.Errors.Should().Contain(e => e.PropertyName == nameof(DeleteAbilityCommand.AbilityId));
        }
    }
}
