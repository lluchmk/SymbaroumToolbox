using Abilities.Domain.Enums;
using MediatR;

namespace Abilities.Application.Abilities.Commands.UpdateAbility
{
    public class UpdateAbilityCommand : IRequest
    {
        public int AbilityId { get; set; }

        public UpdateAbilityCommandBody Body { get; set; }
    }

    public class UpdateAbilityCommandBody
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Ritual data
        public Tradition? Tradition { get; set; }

        // Ability data
        public ActionType? NoviceType { get; set; }
        public string NoviceDescription { get; set; }

        public ActionType? AdeptType { get; set; }
        public string AdeptDescription { get; set; }

        public ActionType? MasterType { get; set; }
        public string MasterDescription { get; set; }

        // Mystical power data
        public string Material { get; set; }
    }
}
