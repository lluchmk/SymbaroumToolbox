using Abilities.Domain.Enums;
using System;

namespace Abilities.Domain.Entities
{
    public class Ritual : BaseAbility
    {
        public Tradition Tradition { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Ritual ritual &&
                   base.Equals(obj) &&
                   Tradition == ritual.Tradition;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Tradition);
        }
    }
}
