namespace Abilities.Domain.Entities
{
    public abstract class TieredAbility : BaseAbility
    {
        public AbilityTier Novice { get; set; }
        public AbilityTier Adept { get; set; }
        public AbilityTier Master { get; set; }
    }
}
