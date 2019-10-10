using System;

namespace Abilities.Domain.Entities
{
    public class BaseAbility
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            return obj is BaseAbility ability &&
                   Id == ability.Id &&
                   UserId == ability.UserId &&
                   Name == ability.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, UserId, Name);
        }
    }
}
