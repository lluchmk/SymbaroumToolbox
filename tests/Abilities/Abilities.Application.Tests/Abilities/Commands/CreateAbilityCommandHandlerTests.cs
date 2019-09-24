using Abilities.Application.Abilities.Commands.CreateAbility;
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

namespace Abilities.Application.Tests.Abilities.Commands
{
    public class CreateAbilityCommandHandlerTests
    {
        private CreateAbilityCommandHandler _sut;

        private Mock<IAbilitiesRepository> _repository;
        private Mock<IUsersService> _usersService;
        private Mock<IMapperService> _mapperService;

        private IFixture _fixture;

        public CreateAbilityCommandHandlerTests()
        {
            _fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            _repository = _fixture.Freeze<Mock<IAbilitiesRepository>>();
            _usersService = _fixture.Freeze<Mock<IUsersService>>();
            _mapperService = _fixture.Freeze<Mock<IMapperService>>();

            _sut = _fixture.Create<CreateAbilityCommandHandler>();
        }

        [Fact]
        public async Task Handle_ReturnsCreatedAbilityId()
        {
            var request = _fixture.Create<CreateAbilityCommand>();

            var entity = _fixture.Create<BaseAbility>();
            _mapperService.Setup(m => m.MapCreateAbilityCommandToEntity(request))
                .Returns(entity);

            var createdId = 987;
            _repository.Setup(r => r.Create(entity, It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdId);

            var response = await _sut.Handle(request, It.IsAny<CancellationToken>());

            response.Should().Be(createdId);
        }

        [Fact]
        public async Task Handle_CreatesAbilityWithCurrentUserId()
        {
            var userId = "Some-user-Id-1234";
            _usersService.Setup(u => u.GetUserId())
                .Returns(userId);

            await _sut.Handle(It.IsAny<CreateAbilityCommand>(), It.IsAny<CancellationToken>());

            _repository.Verify(r => r.Create(
                It.Is<BaseAbility>(a => a.UserId == userId),
                It.IsAny<CancellationToken>()));
        }
    }
}
