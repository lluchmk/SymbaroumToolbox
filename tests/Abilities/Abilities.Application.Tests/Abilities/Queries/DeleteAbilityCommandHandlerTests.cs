using Abilities.Application.Abilities.Commands.DeleteAbility;
using Abilities.Application.Abilities.Queries.Dtos;
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

namespace Abilities.Application.Tests.Abilities.Queries
{
    public class DeleteAbilityCommandHandlerTests
    {
        private DeleteAbilityCommandHandler _sut;

        private Mock<IAbilitiesRepository> _repository;
        private Mock<IMapperService> _mapperService;

        private IFixture _fixture;

        public DeleteAbilityCommandHandlerTests()
        {
            _fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            _repository = _fixture.Freeze<Mock<IAbilitiesRepository>>();
            _mapperService = _fixture.Freeze<Mock<IMapperService>>();

            _sut = _fixture.Create<DeleteAbilityCommandHandler>();
        }

        [Fact]
        public async Task Handle_DeletesAbility()
        {
            var request = _fixture.Create<DeleteAbilityCommand>();
            var ability = _fixture.Create<BaseAbility>();
            _repository.Setup(r => r.GetById(request.AbilityId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(ability);

            await _sut.Handle(request, It.IsAny<CancellationToken>());

            _repository.Verify(r => r.Delete(
                It.Is<BaseAbility>(a => a == ability),
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task Handle_ReturnsAbilityDto()
        {
            var ability = _fixture.Create<BaseAbility>();
            _repository.Setup(r => r.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ability);

            var abilityDto = _fixture.Create<BaseAbilityDto>();
            _mapperService.Setup(m => m.MapEntityToBaseAbilityDto(ability))
                .Returns(abilityDto);

            var response = await _sut.Handle(new DeleteAbilityCommand(), It.IsAny<CancellationToken>());

            response.Should().Be(abilityDto);
        }

        [Fact]
        public async Task Handle_WhenPassingInvalidId_ThrowsInvalidAbilityIdException()
        {
            _repository.Setup(r => r.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);

            Func<Task> execution = async () =>
                await _sut.Handle(new DeleteAbilityCommand(), It.IsAny<CancellationToken>());

            await execution.Should().ThrowAsync<InvalidAbilityIdException>();
        }
    }
}
