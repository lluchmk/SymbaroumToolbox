using Abilities.Application.Abilities.Dtos;
using Abilities.Application.Exceptions;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Application.Interfaces.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Abilities.Application.Abilities.Commands.DeleteAbility
{
    public class DeleteAbilityCommandHandler : IRequestHandler<DeleteAbilityCommand, BaseAbilityDto>
    {
        private readonly IAbilitiesRepository _repository;
        private readonly IMapperService _mapperService;

        public DeleteAbilityCommandHandler(IAbilitiesRepository repository, IMapperService mapperService)
        {
            _repository = repository;
            _mapperService = mapperService;
        }

        public async Task<BaseAbilityDto> Handle(DeleteAbilityCommand request, CancellationToken cancellationToken)
        {
            var ability = await _repository.GetById(request.AbilityId, cancellationToken);
            if (ability is null)
            {
                throw new InvalidAbilityIdException();
            }

            await _repository.Delete(ability, cancellationToken);

            var response = _mapperService.MapEntityToDto(ability);
            return response;
        }
    }
}
