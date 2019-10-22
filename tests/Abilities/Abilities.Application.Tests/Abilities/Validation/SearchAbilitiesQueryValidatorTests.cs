using Abilities.Application.Abilities.Enums;
using Abilities.Application.Abilities.Queries.SearchAbilities;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Abilities.Application.Tests.Abilities.Validation
{
    public class SearchAbilitiesQueryValidatorTests
    {
        [Fact]
        public void Validate_CorreclyValidatesRequest()
        {
            var request = new SearchAbilitiesQuery()
            {
                Types = new AbilityType[] {
                    (AbilityType)9,
                    (AbilityType)10
                }
            };
            var validator = new SearchAbilitiesQueryValidator();

            var response = validator.Validate(request);

            using var asc = new AssertionScope();
            response.IsValid.Should().BeFalse();
            var expectedPropertyName1 = $"{nameof(SearchAbilitiesQuery.Types)}[0]";
            var expectedPropertyName2 = $"{nameof(SearchAbilitiesQuery.Types)}[1]";
            response.Errors.Should().Contain(e => e.PropertyName == expectedPropertyName1);
            response.Errors.Should().Contain(e => e.PropertyName == expectedPropertyName2);
        }
    }
}
