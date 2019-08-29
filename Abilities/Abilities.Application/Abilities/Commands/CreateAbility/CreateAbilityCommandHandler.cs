using Abilities.Application.Abilities.Enums;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Application.Interfaces.Services;
using Abilities.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Abilities.Application.Abilities.Commands.CreateAbility
{
    public class CreateAbilityCommandHandler : IRequestHandler<CreateAbilityCommand, int>
    {
        private readonly IAbilitiesRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public CreateAbilityCommandHandler(IAbilitiesRepository repository, IMapper mapper, IUsersService usersService)
        {
            _repository = repository;
            _mapper = mapper;
            _usersService = usersService;
        }

        public async Task<int> Handle(CreateAbilityCommand command, CancellationToken cancellationToken)
        {
            BaseAbility ability;
            switch (command.Type)
            {
                case AbilityType.Ability:
                    ability = _mapper.Map<Ability>(command);
                    break;
                case AbilityType.MysticalPower:
                    ability = _mapper.Map<MysticalPower>(command);
                    break;
                case AbilityType.Ritual:
                    ability = _mapper.Map<Ritual>(command);
                    break;
                default:
                    throw new NotImplementedException("No handler for unrecognized ability type"); // TODO: Better exception? Should't happen with proper validation
            }

            var userId = _usersService.GetUserId();
            ability.UserId = userId;

            var createdId = await _repository.Create(ability, cancellationToken);
            return createdId;
        }
    }
}
