using Abilities.Application.Abilities.Queries.Dtos;
using Abilities.Application.Exceptions;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Abilities.Application.Abilities.Commands.DeleteAbility
{
    public class DeleteAbilityCommandHandler : IRequestHandler<DeleteAbilityCommand, BaseAbilityDto>
    {
        private readonly IAbilitiesRepository _repository;
        private readonly IMapper _mapper;

        public DeleteAbilityCommandHandler(IAbilitiesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseAbilityDto> Handle(DeleteAbilityCommand request, CancellationToken cancellationToken)
        {
            var ability = await _repository.GetById(request.AbilityId, cancellationToken);
            if (ability is null)
            {
                throw new InvalidAbilityIdException();
            }

            await _repository.Delete(ability, cancellationToken);

            Type dtoType;

            if (ability.GetType() == typeof(Ability))
            {
                dtoType = typeof(AbilityDto);
            }
            else if (ability.GetType() == typeof(MysticalPower))
            {
                dtoType = typeof(MysticalPowerDto);
            }
            else
            {
                dtoType = typeof(Ritual);
            }

            var response = _mapper.Map(ability, ability.GetType(), dtoType) as BaseAbilityDto;

            return response;
        }
    }
}
