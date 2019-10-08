using Abilities.Application.Abilities.Enums;
using Abilities.Application.Abilities.Queries.SearchAbilities;
using Abilities.Domain.Entities;
using Abilities.Persistence.Repositories;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Abilities.Persistence.Tests
{
    public class AbilitiesRepositoryTests
    {
        public IFixture _fixture;

        public AbilitiesRepositoryTests()
        {
            _fixture = new Fixture();
        }

        // -- Search
        [Fact]
        public async Task Search_WhenNoSearchCriteriaAndNoUser_ReturnsAllDefaultAbilities()
        {
            var abilities = CreateDefaultAbilities();

            var options = GetDbContextOptions("NoCriteriaTest");

            using (var saveContext = new AbilitiesDbContext(options))
            {
                saveContext.AddRange(abilities);
                await saveContext.SaveChangesAsync();
            }

            using var context = new AbilitiesDbContext(options);
            var sut = new AbilitiesRepository(context);

            var request = new SearchAbilitiesQuery
            {
                Name = null,
                Types = Enumerable.Empty<AbilityType>()
            };

            var response = await sut.Search(request, null, CancellationToken);

            response.Should().BeEquivalentTo(abilities);
        }

        [Fact]
        public async Task Search_WhenUserId_ReturnsUserAndDefaultAbilities()
        {
            var defaultAbilities = CreateDefaultAbilities();

            var userId = "userId";
            var userAbilities = CreateUserAbilities(userId);
                
            var options = GetDbContextOptions("UserIdTest");

            using (var saveContext = new AbilitiesDbContext(options))
            {
                saveContext.AddRange(defaultAbilities);
                saveContext.AddRange(userAbilities);
                await saveContext.SaveChangesAsync();
            }

            using var context = new AbilitiesDbContext(options);
            var sut = new AbilitiesRepository(context);

            var request = new SearchAbilitiesQuery
            {
                Name = null,
                Types = Enumerable.Empty<AbilityType>()
            };

            var response = await sut.Search(request, userId, CancellationToken);

            // TODO: Review fluent assertions
            response.Should().BeEquivalentTo(Enumerable.Concat(
                defaultAbilities, userAbilities));
        }

        [Fact]
        public async Task Search_WhenSerchingType_ReturnOnlyAbilitiesOfRequestedTypes()
        {
            var abilities = CreateDefaultAbilities();

            var options = GetDbContextOptions("TypeTest");

            using (var saveContext = new AbilitiesDbContext(options))
            {
                saveContext.AddRange(abilities);
                await saveContext.SaveChangesAsync();
            }

            using var context = new AbilitiesDbContext(options);
            var sut = new AbilitiesRepository(context);

            var request = new SearchAbilitiesQuery
            {
                Name = null,
                Types = new AbilityType[] { AbilityType.Ability, AbilityType.Ritual }
            };

            var response = await sut.Search(request, null, CancellationToken);

            response.All(a => 
                a.GetType() == typeof(Ability) || 
                a.GetType() == typeof(Ritual)
                );
        }

        [Fact]
        public async Task Search_WhenSearchingName_ReturnsOnlyContainingName()
        {
            var abilities = CreateDefaultAbilities();

            var name = "abilityname";
            var abilitiesContainingName = new BaseAbility[]
            {
                new Ability { Name = name },
                new Ability { Name = $"something{name}" },
                new Ability { Name = $"{name}something" },
                new Ability { Name = $"something{name}something" },
            };

            var options = GetDbContextOptions("NameTest");

            using (var saveContext = new AbilitiesDbContext(options))
            {
                saveContext.AddRange(abilities);
                saveContext.AddRange(abilitiesContainingName);
                await saveContext.SaveChangesAsync();
            }

            using var context = new AbilitiesDbContext(options);
            var sut = new AbilitiesRepository(context);

            var request = new SearchAbilitiesQuery
            {
                Name = name,
                Types = Enumerable.Empty<AbilityType>()
            };

            var response = await sut.Search(request, null, CancellationToken);

            response.Should().BeEquivalentTo(abilitiesContainingName);
        }

        // -- Create
        // Inserts ability and saves
        // Returns created ability id

        // -- GetById
        // when existting returns single ability
        // when not existing returns null

        // -- Update
        // updates ability and saves

        // -- Delete
        // remoes ability and saves

        // -- IsAbilityOwner
        // When yes returns true
        // when no returns false

        private IEnumerable<BaseAbility> CreateDefaultAbilities()
        {
            var abilities = _fixture.Build<Ability>()
                .Without(a => a.UserId)
                .CreateMany();

            var mysticalPowers = _fixture.Build<MysticalPower>()
                .Without(m => m.UserId)
                .CreateMany();

            var rituals = _fixture.Build<Ritual>()
                .Without(r => r.UserId)
                .CreateMany();

            var defaultAbilities = Enumerable.Concat<BaseAbility>(
                abilities,
                mysticalPowers
                );

            defaultAbilities = Enumerable.Concat(
                defaultAbilities,
                rituals
                );

            return defaultAbilities;
        }

        private IEnumerable<BaseAbility> CreateUserAbilities(string userId)
        {
            var abilities = _fixture.Build<Ability>()
               .With(a => a.UserId, userId)
               .CreateMany();

            var mysticalPowers = _fixture.Build<MysticalPower>()
                .With(a => a.UserId, userId)
                .CreateMany();

            var rituals = _fixture.Build<Ritual>()
                .With(a => a.UserId, userId)
                .CreateMany();

            var userAbilities = Enumerable.Concat<BaseAbility>(
                abilities,
                mysticalPowers
                );

            userAbilities = Enumerable.Concat(
                userAbilities,
                rituals
                );

            return userAbilities;
        }

        private DbContextOptions<AbilitiesDbContext> GetDbContextOptions(string dbName)
        {
            return new DbContextOptionsBuilder<AbilitiesDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }

        private CancellationToken CancellationToken => new CancellationTokenSource().Token;
    }
}