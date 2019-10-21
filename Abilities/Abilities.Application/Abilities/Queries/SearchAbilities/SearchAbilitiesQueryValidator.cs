using FluentValidation;

namespace Abilities.Application.Abilities.Queries.SearchAbilities
{
    public class SearchAbilitiesQueryValidator : AbstractValidator<SearchAbilitiesQuery>
    {
        public SearchAbilitiesQueryValidator()
        {
            RuleForEach(c => c.Types)
                .IsInEnum();
        }
    }
}
