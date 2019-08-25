namespace Abilities.Domain.Entities
{
    public class BaseAbility
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
