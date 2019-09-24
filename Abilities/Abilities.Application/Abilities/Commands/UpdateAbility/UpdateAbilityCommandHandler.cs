﻿using Abilities.Application.Exceptions;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Application.Interfaces.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Abilities.Application.Abilities.Commands.UpdateAbility
{
    public class UpdateAbilityCommandHandler : IRequestHandler<UpdateAbilityCommand>
    {
        private readonly IAbilitiesRepository _repository;
        private readonly IMapperService _mapperService;

        public UpdateAbilityCommandHandler(IAbilitiesRepository repository, IMapperService mapperService)
        {
            _repository = repository;
            _mapperService = mapperService;
        }

        public async Task<Unit> Handle(UpdateAbilityCommand request, CancellationToken cancellationToken)
        {
            var ability = await _repository.GetById(request.AbilityId, cancellationToken);
            if (ability is null)
            {
                throw new InvalidAbilityIdException();
            }

            var updatedAbility = _mapperService.MapUpdateAbilityCommandToAbility(request.Body, ability);

            await _repository.Update(updatedAbility, cancellationToken);

            return Unit.Value;
        }
    }
}
