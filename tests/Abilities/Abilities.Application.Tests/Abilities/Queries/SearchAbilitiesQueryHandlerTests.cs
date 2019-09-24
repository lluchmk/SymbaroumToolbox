using Abilities.Application.Abilities.Queries.Dtos;
using Abilities.Application.Abilities.Queries.SearchAbilities;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Application.Interfaces.Services;
using Abilities.Domain.Entities;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
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
            var request = _fixture.Create<SearchAbilitiesQuery>();

            var abilities = _fixture.CreateMany<BaseAbility>();
            _abilitiesRepository.Setup(r => r.Search(request, It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(abilities);

            var expectedResponse = _fixture.Create<AbilitiesListViewModel>();
            _mapperService.Setup(m => m.MapEntitiesToAbilitiesListViewModel(abilities))
                .Returns(expectedResponse);

            var response = await _sut.Handle(request, It.IsAny<CancellationToken>());

            response.Should().Be(expectedResponse);
        }

        [Fact]
        public async Task Handle_WhenNotAuthenticated_SearchesWithoutUserId()
        {
            _usersService.Setup(u => u.IsUserAuthenticated())
                .Returns(false);

            var response = await _sut.Handle(It.IsAny<SearchAbilitiesQuery>(), It.IsAny<CancellationToken>());

            _abilitiesRepository.Verify(a => a.Search(
                It.IsAny<SearchAbilitiesQuery>(),
                It.Is<string>(u => u == null),
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task Handle_WhenAuthenticated_SearchesIncludingUserId()
        {
            _usersService.Setup(u => u.IsUserAuthenticated())
                .Returns(true);

            var userId = "Some-user-Id-1234";
            _usersService.Setup(u => u.GetUserId())
                .Returns(userId);

            // Act
            var response = await _sut.Handle(It.IsAny<SearchAbilitiesQuery>(), It.IsAny<CancellationToken>());

            // Assert
            _abilitiesRepository.Verify(a => a.Search(
                It.IsAny<SearchAbilitiesQuery>(),
                It.Is<string>(u => u == userId),
                It.IsAny<CancellationToken>()));
        }
    }
}
