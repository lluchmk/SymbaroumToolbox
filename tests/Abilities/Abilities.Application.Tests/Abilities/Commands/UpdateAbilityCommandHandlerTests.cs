using Abilities.Application.Abilities.Commands.UpdateAbility;
using Abilities.Application.Exceptions;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Application.Interfaces.Services;
using Abilities.Domain.Entities;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Abilities.Application.Tests.Abilities.Commands
{
    public class UpdateAbilityCommandHandlerTests
    {
        private UpdateAbilityCommandHandler _sut;

        private Mock<IAbilitiesRepository> _repository;
        private Mock<IMapperService> _mapperService;

        private IFixture _fixture;

        public UpdateAbilityCommandHandlerTests()
        {
            _fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            _repository = _fixture.Freeze<Mock<IAbilitiesRepository>>();
            _mapperService = _fixture.Freeze<Mock<IMapperService>>();

            _sut = _fixture.Create<UpdateAbilityCommandHandler>();
        }

        [Fact]
        public async Task Handle_UpdatesAbility()
        {
            var request = _fixture.Create<UpdateAbilityCommand>();
            var ability = _fixture.Create<BaseAbility>();
            _repository.Setup(r => r.GetById(request.AbilityId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(ability);

            var updatedAbility = _fixture.Create<BaseAbility>();
            _mapperService.Setup(m => m.MapUpdateAbilityCommandToAbility(request.Body, ability))
                .Returns(updatedAbility);

            await _sut.Handle(request, It.IsAny<CancellationToken>());

            _repository.Verify(r => r.Update(
                It.Is<BaseAbility>(a => a == updatedAbility),
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task Handle_WhenPassingInvalidId_ThrowsInvalidAbilityIdException()
        {
            _repository.Setup(r => r.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);

            Func<Task> execution = async () =>
                await _sut.Handle(new UpdateAbilityCommand(), It.IsAny<CancellationToken>());

            await execution.Should().ThrowAsync<InvalidAbilityIdException>();
        }
    }
}
