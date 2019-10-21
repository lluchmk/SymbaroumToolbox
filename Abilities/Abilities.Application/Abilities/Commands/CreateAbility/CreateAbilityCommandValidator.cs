using Abilities.Application.Abilities.Enums;
using FluentValidation;

namespace Abilities.Application.Abilities.Commands.CreateAbility
{
    public class CreateAbilityCommandValidator : AbstractValidator<CreateAbilityCommand>
    {
        public CreateAbilityCommandValidator()
        {
            RuleFor(c => c.Type)
                .IsInEnum();

            RuleFor(c => c.Name)
                .NotEmpty();

            RuleFor(c => c.Description)
                .NotEmpty();

            RuleFor(c => c.Material)
                .NotEmpty()
                .When(c => c.Type == AbilityType.MysticalPower);

            RuleFor(c => c.Tradition)
                .IsInEnum()
                .When(c => c.Type == AbilityType.Ritual);

            RuleFor(c => c.NoviceType)
                .IsInEnum()
                .When(c => c.Type == AbilityType.MysticalPower || c.Type == AbilityType.Ability);

            RuleFor(c => c.NoviceDescription)
                .NotEmpty()
                .When(c => c.Type == AbilityType.MysticalPower || c.Type == AbilityType.Ability);

            RuleFor(c => c.AdeptType)
                .IsInEnum()
                .When(c => c.Type == AbilityType.MysticalPower || c.Type == AbilityType.Ability);

            RuleFor(c => c.AdeptDescription)
                .NotEmpty()
                .When(c => c.Type == AbilityType.MysticalPower || c.Type == AbilityType.Ability);

            RuleFor(c => c.MasterType)
                .IsInEnum()
                .When(c => c.Type == AbilityType.MysticalPower || c.Type == AbilityType.Ability);

            RuleFor(c => c.MasterDescription)
                .NotEmpty()
                .When(c => c.Type == AbilityType.MysticalPower || c.Type == AbilityType.Ability);
        }
    }
}
