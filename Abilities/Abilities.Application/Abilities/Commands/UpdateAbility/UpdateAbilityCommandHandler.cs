using Abilities.Application.Abilities.Enums;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Application.Interfaces.Services;
using Abilities.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Abilities.Application.Abilities.Commands.UpdateAbility
{
    public class UpdateAbilityCommandHandler : IRequestHandler<UpdateAbilityCommand>
    {
        private readonly IAbilitiesRepository _repository;
        private readonly IUsersService _usersService;

        public UpdateAbilityCommandHandler(IAbilitiesRepository repository, IUsersService usersService)
        {
            _repository = repository;
            _usersService = usersService;
        }

        public async Task<Unit> Handle(UpdateAbilityCommand request, CancellationToken cancellationToken)
        {
            if (!_usersService.IsUserAuthenticated())
            {
                throw new UnauthorizedAccessException("The use must be logged in to update an ability.");
            }

            var ability = await _repository.GetById(request.AbilityId, cancellationToken);
            if (ability is null)
            {
                throw new Exception("Invalid ability id."); // TODO: Review exception type
            }
            var userId = _usersService.GetUserId();

            if (ability.UserId != userId)
            {
                throw new UnauthorizedAccessException("The user cannot edit this ability.");
            }

            MapUpdateRequest(request.Body, ability);

            await _repository.Update(ability, cancellationToken);

            return Unit.Value;
        }

        public void MapUpdateRequest<TAbility>(UpdateAbilityCommandBody requestBody, TAbility ability)
            where TAbility : BaseAbility
        {
            Type abilityType = ability.GetType();

            if (abilityType == typeof(Ability))
            {
                MapAbility(requestBody, ability as Ability);
            }
            else if (abilityType == typeof(MysticalPower))
            {
                MapMysticalPower(requestBody, ability as MysticalPower);
            }
            else if (abilityType == typeof(Ritual))
            {
                MapRitual(requestBody, ability as Ritual);
            }
            else
            {
                throw new Exception(); // TODO: Better exception
            }
        }

        public void MapBaseProperties<TAbility>(UpdateAbilityCommandBody requestBody, TAbility baseAbility)
            where TAbility : BaseAbility    
        {
            baseAbility.Name = requestBody.Name ?? baseAbility.Name;
            baseAbility.Description = requestBody.Description ?? baseAbility.Description;
        }

        public void MapAbility<TAbility>(UpdateAbilityCommandBody requestBody, TAbility ability)
            where TAbility : Ability
        {
            MapBaseProperties(requestBody, ability);

            ability.Novice.Type = requestBody.NoviceType ?? ability.Novice.Type;
            ability.Novice.Description = requestBody.NoviceDescription ?? ability.Novice.Description;

            ability.Adept.Type = requestBody.AdeptType ?? ability.Adept.Type;
            ability.Adept.Description = requestBody.AdeptDescription ?? ability.Adept.Description;

            ability.Master.Type = requestBody.MasterType ?? ability.Master.Type;
            ability.Master.Description = requestBody.MasterDescription ?? ability.Master.Description;
        }

        public void MapMysticalPower(UpdateAbilityCommandBody requestBody, MysticalPower mysticalPower)
        {
            MapAbility(requestBody, mysticalPower);

            mysticalPower.Material = requestBody.Material ?? mysticalPower.Material;
        }

        public void MapRitual(UpdateAbilityCommandBody requestBody, Ritual ritual)
        {
            MapBaseProperties(requestBody, ritual);

            ritual.Tradition = requestBody.Tradition ?? ritual.Tradition;
        }
    }
}
