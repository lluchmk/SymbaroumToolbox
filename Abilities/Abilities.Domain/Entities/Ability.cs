using System;
using System.Collections.Generic;

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
                   EqualityComparer<AbilityTier>.Default.Equals(Novice, ability.Novice) &&
                   EqualityComparer<AbilityTier>.Default.Equals(Adept, ability.Adept) &&
                   EqualityComparer<AbilityTier>.Default.Equals(Master, ability.Master);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Novice, Adept, Master);
        }
    }
}
