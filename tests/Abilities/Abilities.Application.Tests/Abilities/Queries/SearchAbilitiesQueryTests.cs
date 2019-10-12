using Abilities.Application.Abilities.Enums;
using Abilities.Application.Abilities.Queries.SearchAbilities;
using Abilities.Domain.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Abilities.Application.Tests.Abilities.Queries
{
    public class SearchAbilitiesQueryTests
    {
        [Fact]
        public void GetRequestedTypes_WhenNotSpecifyingTypes_ReturnsAllTypes()
        {
            var request = new SearchAbilitiesQuery
            {
                Types = Enumerable.Empty<AbilityType>()
            };

            var response = request.GetRequestedTypes();

            var expectedResponse = new Type[]
            {
                typeof(Ability),
                typeof(MysticalPower),
                typeof(Ritual)
            };
            response.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public void GetRequestedTypes_WhenRequestingAllTypes_ReturnsAllTypes()
        {
            var request = new SearchAbilitiesQuery
            {
                Types = new AbilityType[]
                {
                    AbilityType.Ability,
                    AbilityType.MysticalPower,
                    AbilityType.Ritual
                }
            };

            var response = request.GetRequestedTypes();

            var expectedResponse = new Type[]
            {
                typeof(Ability),
                typeof(MysticalPower),
                typeof(Ritual)
            };
            response.Should().BeEquivalentTo(expectedResponse);
        }

        [Theory]
        [InlineData(AbilityType.Ability, typeof(Ability))]
        [InlineData(AbilityType.MysticalPower, typeof(MysticalPower))]
        [InlineData(AbilityType.Ritual, typeof(Ritual))]
        public void GetRequestedTypes_WhenRequestingSingleType_ReturnsCorrectType(AbilityType requestedType, Type expectedType)
        {
            var request = new SearchAbilitiesQuery
            {
                Types = new AbilityType[]
                {
                    requestedType
                }
            };

            var response = request.GetRequestedTypes();

            var expectedResponse = new Type[]
            {
                expectedType
            };
            response.Should().BeEquivalentTo(expectedResponse);
        }

        public static IEnumerable<object[]> ValueCombinations => new[]
        {
            new object[] {
                new AbilityType[] { AbilityType.Ability, AbilityType.MysticalPower },
                new Type[] { typeof(Ability), typeof(MysticalPower) }
            },
            new object[] {
                new AbilityType[] { AbilityType.Ability, AbilityType.Ritual },
                new Type[] { typeof(Ability), typeof(Ritual) }
            },
            new object[] {
                new AbilityType[] { AbilityType.Ritual, AbilityType.MysticalPower },
                new Type[] { typeof(Ritual), typeof(MysticalPower) }
            },
        };

        [Theory]
        [MemberData(nameof(ValueCombinations))]
        public void GetRequestedTypes_WhenRequestingMultipleTypes_ReturnsCorrectTypes(IEnumerable<AbilityType> requestedTypes, IEnumerable<Type> expectedTypes)
        {
            var request = new SearchAbilitiesQuery
            {
                Types = requestedTypes
            };

            var response = request.GetRequestedTypes();

            response.Should().BeEquivalentTo(expectedTypes);
        }
    }
}
