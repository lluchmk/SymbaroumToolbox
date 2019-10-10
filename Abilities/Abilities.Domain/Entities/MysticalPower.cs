using System;

namespace Abilities.Domain.Entities
{
    public class MysticalPower : Ability
    {
        public string Material { get; set; }

        public MysticalPower()
        { }

        public override bool Equals(object obj)
        {
            return obj is MysticalPower power &&
                   base.Equals(obj) &&
                   Material == power.Material;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Material);
        }
    }
}
