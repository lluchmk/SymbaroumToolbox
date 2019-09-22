using Abilities.Application.Abilities.Enums;
using Abilities.Application.Abilities.Queries.Dtos;
using Abilities.Application.Abilities.Queries.SearchAbilities;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Application.Interfaces.Services;
using Abilities.Domain.Entities;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Abilities.Application.Tests.Abilities.Queries
{
    public class SearchAbilitiesQueryHandlerTests
    {
        private SearchAbilitiesQueryHandler _sut;

        private Mock<IAbilitiesRepository> _abilitiesRepository;
        private Mock<IMapperService> _mapperService;
        private Mock<IUsersService> _usersService;

        private IFixture _fixture;

        public SearchAbilitiesQueryHandlerTests()
        {
            _fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            _abilitiesRepository = _fixture.Freeze<Mock<IAbilitiesRepository>>();
            _mapperService = _fixture.Freeze<Mock<IMapperService>>();
            _usersService = _fixture.Freeze<Mock<IUsersService>>();

            _sut = _fixture.Create<SearchAbilitiesQueryHandler>();
        }

        [Fact]
        public async Task Handle_ReturnsExpectedResponse()
        {
            // Arrange
            var request = new SearchAbilitiesQuery
            {
                Name = string.Empty,
                Types = Enumerable.Empty<AbilityType>()
            };
            var cancellationToken = GetCancellationToken();

            var abilities = CreateAll();
            _abilitiesRepository.Setup(r => r.Search(request, It.IsAny<string>(), cancellationToken))
                .ReturnsAsync(abilities);

            var expectedResponse = _fixture.Create<AbilitiesListViewModel>();
            _mapperService.Setup(m => m.MapToAbilitiesListViewModel(abilities))
                .Returns(expectedResponse);

            // Act
            var response = await _sut.Handle(request, cancellationToken);

            // Assert
            response.Should().Be(expectedResponse);
        }

        [Fact]
        public async Task Handle_WhenNotAuthenticated_SearchesWithoutUserId()
        {
            // Arrange
            var request = new SearchAbilitiesQuery
            {
                Name = string.Empty,
                Types = Enumerable.Empty<AbilityType>()
            };
            var cancellationToken = GetCancellationToken();

            _usersService.Setup(u => u.IsUserAuthenticated())
                .Returns(false);

            // Act
            var response = await _sut.Handle(request, cancellationToken);

            // Assert
            _abilitiesRepository.Verify(a => a.Search(
                It.Is<SearchAbilitiesQuery>(q => q == request),
                It.Is<string>(u => u == null),
                It.Is<CancellationToken>(c => c == cancellationToken)));
        }

        [Fact]
        public async Task Handle_WhenAuthenticated_SearchesIncludingUserId()
        {
            // Arrange
            var request = new SearchAbilitiesQuery
            {
                Name = string.Empty,
                Types = Enumerable.Empty<AbilityType>()
            };
            var cancellationToken = GetCancellationToken();

            _usersService.Setup(u => u.IsUserAuthenticated())
                .Returns(true);

            var userId = "Some-user-Id-1234";
            _usersService.Setup(u => u.GetUserId())
                .Returns(userId);

            // Act
            var response = await _sut.Handle(request, cancellationToken);

            // Assert
            _abilitiesRepository.Verify(a => a.Search(
                It.Is<SearchAbilitiesQuery>(q => q == request),
                It.Is<string>(u => u == userId),
                It.Is<CancellationToken>(c => c == cancellationToken)));
        }

        [Fact]
        public async Task Handle_CorrectlyMapsResponse()
        {
            // Arrange
            var request = new SearchAbilitiesQuery
            {
                Name = string.Empty,
                Types = Enumerable.Empty<AbilityType>()
            };
            var cancellationToken = GetCancellationToken();

            var abilities = CreateAll();
            _abilitiesRepository.Setup(r => r.Search(request, It.IsAny<string>(), cancellationToken))
                .ReturnsAsync(abilities);

            // Act
            var response = await _sut.Handle(request, cancellationToken);

            // Assert
            _mapperService.Verify(m => m.MapToAbilitiesListViewModel(
                It.Is<IEnumerable<BaseAbility>>(a => a == abilities)));
        }

        private CancellationToken GetCancellationToken()
        {
            var source = new CancellationTokenSource();
            return source.Token;
        }

        private IEnumerable<TAbility> CreateAbilities<TAbility>(string userId = null)
            where TAbility : BaseAbility
        {
            var abilities = _fixture.Build<TAbility>()
                .Without(a => a.UserId)
                .CreateMany();

            if (!string.IsNullOrWhiteSpace(userId))
            {
                var userAbilities = _fixture.Build<TAbility>()
                .With(a => a.UserId, userId)
                .CreateMany();

                abilities = Enumerable.Concat(abilities, userAbilities);
            }

            return abilities;
        }

        private IEnumerable<BaseAbility> CreateAll(string userId = null)
        {
            return new IEnumerable<BaseAbility>[]
                {
                    CreateAbilities<Ability>(userId),
                    CreateAbilities<MysticalPower>(userId),
                    CreateAbilities<Ritual>(userId)
                }
                .SelectMany(a => a);
        }
    }
}
