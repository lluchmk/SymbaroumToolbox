using FluentValidation;

namespace Abilities.Application.Abilities.Commands.UpdateAbility
{
    public class UpdateAbilityCommandValidator : AbstractValidator<UpdateAbilityCommand>
    {
        public UpdateAbilityCommandValidator()
        {
            RuleFor(c => c.AbilityId)
                .GreaterThan(0);

            RuleFor(c => c.Body)
                .NotNull();

            RuleFor(c => c.Body.Tradition)
                .IsInEnum()
                .When(c => c.Body.Tradition.HasValue);

            RuleFor(c => c.Body.NoviceType)
                .IsInEnum()
                .When(c => c.Body.NoviceType.HasValue);

            RuleFor(c => c.Body.AdeptType)
                .IsInEnum()
                .When(c => c.Body.AdeptType.HasValue);

            RuleFor(c => c.Body.MasterType)
                .IsInEnum()
                .When(c => c.Body.MasterType.HasValue);
        }
    }
}
