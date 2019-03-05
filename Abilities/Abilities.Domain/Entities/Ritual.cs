using Abilities.Domain.Enums;

namespace Abilities.Domain.Entities
{
    public class Ritual : BaseAbility
    {
        public Tradition Tradition { get; set; }
    }
}
