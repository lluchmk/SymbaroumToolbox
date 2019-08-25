namespace Abilities.Domain.Entities
{
    public class Ability : BaseAbility
    {
        public AbilityTier Novice { get; set; }
        public AbilityTier Adept { get; set; }
        public AbilityTier Master { get; set; }
    }
}
