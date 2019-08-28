namespace Abilities.Domain.Entities
{
    public class MysticalPower : Ability
    {
        public string Material { get; set; }

        public MysticalPower()
        { }

        public MysticalPower(Ability ability)
        {
            Id = ability.Id;
            UserId = ability.UserId;
            Name = ability.Name;
            Description = ability.Description;
            Novice = ability.Novice;
            Adept = ability.Adept;
            Master = ability.Master;
        }
    }
}
