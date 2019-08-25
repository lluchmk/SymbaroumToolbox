using Abilities.Application.Abilities.Enums;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abilities.Application.Abilities.Commands.CreateAbility
{
    class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, int>
    {
        private readonly IAbilitiesRepository _repository;
        private readonly IMapper _mapper;

        public CreateSkillCommandHandler(IAbilitiesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateSkillCommand command, CancellationToken cancellationToken)
        {
            int createdId;
            switch (command.Type)
            {
                case SkillType.Ability:
                    var ability = _mapper.Map<Ability>(command);
                    createdId = await _repository.CreateAbility(ability, cancellationToken);
                    break;
                case SkillType.MysticalPower:
                    var mysticalPower = _mapper.Map<MysticalPower>(command);
                    createdId = await _repository.CreateMysticalPower(mysticalPower, cancellationToken);
                    break;
                case SkillType.Ritual:
                    var ritual = _mapper.Map<Ritual>(command);
                    createdId = await _repository.CreateRitual(ritual, cancellationToken);
                    break;
                default:
                    throw new NotImplementedException("No handler for unrecognized skill type"); // TODO: Better exception
            }
            return createdId;
        }
    }
}
