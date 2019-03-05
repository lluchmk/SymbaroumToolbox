using Abilities.Domain.Entities;
using Abilities.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abilities.Persistence.EFInMemory.DataSeeding
{
    public class AbilitiesInitializer
    {
        public static void Initialize(AbilitiesDbContext context)
        {
            var initializer = new AbilitiesInitializer();
            initializer.Seed(context);
        }

        public void Seed(AbilitiesDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Abilities.Any())
            {
                return; //DB already seeded
            }

            SeedAbilities(context);
            SeedMysticalPowers(context);
            SeedRituals(context);
        }

        private void SeedAbilities(AbilitiesDbContext context)
        {
            var abilities = new Ability[]
            {
                new Ability
                {
                    Name = "Ability 1",
                    Description = "Ability 1 description",
                    Novice = new AbilityTier
                    {
                         Description = "Tier 1",
                         Type = ActionType.Active
                    },
                    Adept = new AbilityTier
                    {
                        Description = "Tier 2",
                        Type = ActionType.OncePerAdventure
                    },
                    Master = new AbilityTier
                    {
                        Description = "Tier 3",
                        Type = ActionType.OncePerAdventure
                    }
                },
                new Ability
                {
                    Name = "Ability 2",
                    Description = "Ability 2 description",
                    Novice = new AbilityTier
                    {
                         Description = "Tier 1",
                         Type = ActionType.Active
                    },
                    Adept = new AbilityTier
                    {
                        Description = "Tier 2",
                        Type = ActionType.OncePerAdventure
                    },
                    Master = new AbilityTier
                    {
                        Description = "Tier 3",
                        Type = ActionType.OncePerAdventure
                    }
                },
            };

            context.Abilities.AddRange(abilities);
            context.SaveChanges();
        }

        private void SeedMysticalPowers(AbilitiesDbContext context)
        {
            var mysticalPowers = new MysticalPower[]
            {
                new MysticalPower
                {
                    Name = "Mystical power 1",
                    Description = "Mystical power 1 description",
                    Novice = new AbilityTier
                    {
                         Description = "Tier 1",
                         Type = ActionType.Active
                    },
                    Adept = new AbilityTier
                    {
                        Description = "Tier 2",
                        Type = ActionType.OncePerAdventure
                    },
                    Master = new AbilityTier
                    {
                        Description = "Tier 3",
                        Type = ActionType.OncePerAdventure
                    },
                    Material = "Material 1"
                },
                new MysticalPower
                {
                    Name = "Mystical power 2",
                    Description = "Mystical power 2 description",
                    Novice = new AbilityTier
                    {
                         Description = "Tier 1",
                         Type = ActionType.Active
                    },
                    Adept = new AbilityTier
                    {
                        Description = "Tier 2",
                        Type = ActionType.OncePerAdventure
                    },
                    Master = new AbilityTier
                    {
                        Description = "Tier 3",
                        Type = ActionType.OncePerAdventure
                    },
                    Material = "Material 2"
                },
            };

            context.MysticalPowers.AddRange(mysticalPowers);
            context.SaveChanges();
        }

        private void SeedRituals(AbilitiesDbContext context)
        {
            var rituals = new Ritual[]
            {
                new Ritual
                {
                    Name = "Ritual 1",
                    Description = "Ritual 1 description",
                    Tradition = Tradition.Sorcery
                },
            };

            context.Rituals.AddRange(rituals);
            context.SaveChanges();
        }
    }
}
