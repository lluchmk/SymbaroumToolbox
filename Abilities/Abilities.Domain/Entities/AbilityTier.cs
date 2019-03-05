using Abilities.Domain.Enums;

namespace Abilities.Domain.Entities
{
    public class AbilityTier
    {
        public ActionType Type { get; set; }
        public string Description { get; set; }

        // TODO: Dictionary of data to reference on the description??
    }
}
