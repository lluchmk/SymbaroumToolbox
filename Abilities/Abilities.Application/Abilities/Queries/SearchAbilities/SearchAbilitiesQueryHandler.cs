using Abilities.Application.Abilities.Queries.Dtos;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Application.Interfaces.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Abilities.Application.Abilities.Queries.SearchAbilities
{
    public class SearchAbilitiesQueryHandler : IRequestHandler<SearchAbilitiesQuery, AbilitiesListViewModel>
    {
        private readonly IAbilitiesRepository _repository;
        private readonly IMapperService _mapperService;
        private readonly IUsersService _usersService;

        public SearchAbilitiesQueryHandler(IAbilitiesRepository repository, IMapperService mapperService, IUsersService usersService)
        {
            _repository = repository;
            _mapperService = mapperService;
            _usersService = usersService;
        }

        public async Task<AbilitiesListViewModel> Handle(SearchAbilitiesQuery request, CancellationToken cancellationToken)
        {
            string userId = null;

            if (_usersService.IsUserAuthenticated())
            {
                userId = _usersService.GetUserId();
            }

            var allAbilities = await _repository.Search(request, userId, cancellationToken);

            var viewModel = _mapperService.MapEntitiesToAbilitiesListViewModel(allAbilities);

            return viewModel;
        }
    }
}
