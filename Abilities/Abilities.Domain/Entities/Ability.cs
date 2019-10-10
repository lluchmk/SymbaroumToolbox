using System;

namespace Abilities.Domain.Entities
{
    public class Ability : BaseAbility
    {
        public AbilityTier Novice { get; set; }
        public AbilityTier Adept { get; set; }
        public AbilityTier Master { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Ability ability &&
                   base.Equals(obj) &&
                   Novice.Equals(ability.Novice) &&
                   Adept.Equals(ability.Adept) &&
                   Master.Equals(ability.Master);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Novice, Adept, Master);
        }
    }
}
