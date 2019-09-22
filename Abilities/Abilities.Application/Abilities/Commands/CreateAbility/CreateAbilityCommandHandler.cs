using Abilities.Application.Interfaces.Repositories;
using Abilities.Application.Interfaces.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Abilities.Application.Abilities.Commands.CreateAbility
{
    public class CreateAbilityCommandHandler : IRequestHandler<CreateAbilityCommand, int>
    {
        private readonly IAbilitiesRepository _repository;
        private readonly IMapperService _mapperService;
        private readonly IUsersService _usersService;

        public CreateAbilityCommandHandler(IAbilitiesRepository repository, IMapperService mapperService, IUsersService usersService)
        {
            _repository = repository;
            _mapperService = mapperService;
            _usersService = usersService;
        }

        public async Task<int> Handle(CreateAbilityCommand command, CancellationToken cancellationToken)
        {
            var ability = _mapperService.MapCreateAbilityCommandToEntity(command);

            var userId = _usersService.GetUserId();
            ability.UserId = userId;

            var createdId = await _repository.Create(ability, cancellationToken);
            return createdId;
        }
    }
}
