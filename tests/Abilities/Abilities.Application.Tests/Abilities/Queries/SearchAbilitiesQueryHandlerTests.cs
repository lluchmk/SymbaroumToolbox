using Abilities.Application.Abilities.Enums;
using Abilities.Application.Abilities.Queries.Dtos;
using Abilities.Application.Abilities.Queries.SearchAbilities;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Application.Interfaces.Services;
using Abilities.Domain.Entities;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Abilities.Application.Tests.Abilities.Queries
{
    public class SearchAbilitiesQueryHandlerTests
    {
        private SearchAbilitiesQueryHandler _sut;

        private Mock<IAbilitiesRepository> _abilitiesRepository;
        private Mock<IMapper> _mapper;
        private Mock<IUsersService>  _usersService;

        private IFixture _fixture;

        public SearchAbilitiesQueryHandlerTests()
        {
            _fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            _abilitiesRepository = _fixture.Freeze<Mock<IAbilitiesRepository>>();
            _mapper = _fixture.Freeze<Mock<IMapper>>();
            _usersService = _fixture.Freeze<Mock<IUsersService>>();

            _sut = _fixture.Create<SearchAbilitiesQueryHandler>();
        }

        [Fact]
        public async Task Handle_WhenUnauthenticatedEmptySearch_ReturnsSystemAbilities()
        {
            // Arrange
            var request = new SearchAbilitiesQuery
            {
                Name = string.Empty,
                Types = Enumerable.Empty<AbilityType>()
            };
            var cancellationToken = GetCancellationToken();

            var abilitiesDtos = _fixture.CreateMany<AbilityDto>();
            _mapper.Setup(m => m.Map<IEnumerable<AbilityDto>>(It.IsAny<IEnumerable<Ability>>()))
                .Returns(abilitiesDtos);

            var mysticalPowersDtos = _fixture.CreateMany<MysticalPowerDto>();
            _mapper.Setup(m => m.Map<IEnumerable<MysticalPowerDto>>(It.IsAny<IEnumerable<MysticalPower>>()))
                .Returns(mysticalPowersDtos);

            var ritualDtos = _fixture.CreateMany<RitualDto>();
            _mapper.Setup(m => m.Map<IEnumerable<RitualDto>>(It.IsAny<IEnumerable<Ritual>>()))
                .Returns(ritualDtos);

            // Act
            var response = await _sut.Handle(request, cancellationToken);

            // Assert
           using (new AssertionScope())
            {
                response.Abilities.Should().BeEquivalentTo(abilitiesDtos);
                response.MysticalPowers.Should().BeEquivalentTo(mysticalPowersDtos);
                response.Rituals.Should().BeEquivalentTo(ritualDtos);
            }
        }

        private CancellationToken GetCancellationToken()
        {
            var source = new CancellationTokenSource();
            return source.Token;
        }
    }
}
