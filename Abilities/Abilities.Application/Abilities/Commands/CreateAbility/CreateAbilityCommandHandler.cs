using Abilities.Application.Abilities.Enums;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Abilities.Application.Abilities.Commands.CreateAbility
{
    class CreateAbilityCommandHandler : IRequestHandler<CreateAbilityCommand, int>
    {
        private readonly IAbilitiesRepository _repository;
        private readonly IMapper _mapper;

        public CreateAbilityCommandHandler(IAbilitiesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAbilityCommand command, CancellationToken cancellationToken)
        {
            int createdId;
            switch (command.Type)
            {
                case AbilityType.Ability:
                    var ability = _mapper.Map<Ability>(command);
                    createdId = await _repository.Create(ability, cancellationToken);
                    break;
                case AbilityType.MysticalPower:
                    var mysticalPower = _mapper.Map<MysticalPower>(command);
                    createdId = await _repository.Create(mysticalPower, cancellationToken);
                    break;
                case AbilityType.Ritual:
                    var ritual = _mapper.Map<Ritual>(command);
                    createdId = await _repository.Create(ritual, cancellationToken);
                    break;
                default:
                    throw new NotImplementedException("No handler for unrecognized ability type"); // TODO: Better exception
            }
            return createdId;
        }
    }
}
