using FluentValidation;

namespace Abilities.Application.Abilities.Commands.DeleteAbility
{
    public class DeleteAbilityCommandValidator : AbstractValidator<DeleteAbilityCommand>
    {
        public DeleteAbilityCommandValidator()
        {
            RuleFor(c => c.AbilityId)
                .GreaterThan(0);
        }
    }
}
