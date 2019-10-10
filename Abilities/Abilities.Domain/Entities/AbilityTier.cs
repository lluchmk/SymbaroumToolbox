using Abilities.Domain.Enums;
using System;

namespace Abilities.Domain.Entities
{
    public class AbilityTier
    {
        public ActionType Type { get; set; }
        public string Description { get; set; }

        public AbilityTier()
        { }

        public AbilityTier(ActionType type, string description)
        {
            Type = type;
            Description = description;
        }

        public override bool Equals(object obj)
        {
            return obj is AbilityTier tier &&
                   Type == tier.Type &&
                   Description == tier.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Description);
        }

        // TODO: Dictionary of data to reference on the description??
    }
}
