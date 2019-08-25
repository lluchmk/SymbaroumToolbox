using Abilities.Application.Abilities.Queries.Dtos;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Abilities.Application.Abilities.Queries.SearchAbilities
{
    public class SearchAbilitiesQueryHandler : IRequestHandler<SearchAbilitiesQuery, AbilitiesListViewModel>
    {
        private readonly IAbilitiesRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SearchAbilitiesQueryHandler(IAbilitiesRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AbilitiesListViewModel> Handle(SearchAbilitiesQuery request, CancellationToken cancellationToken)
        {
            string userId = null;

            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            IEnumerable<Ability> abilities = Enumerable.Empty<Ability>();
            if (request.SearchAbilities())
            {
                abilities = await _repository.SearchAbilities(request, userId, cancellationToken);
            }

            IEnumerable<MysticalPower> mysticalPowers = Enumerable.Empty<MysticalPower>();
            if (request.SearchMysticalPowers())
            {
                mysticalPowers = await _repository.SearchMysticalPowers(request, userId, cancellationToken);
            }

            IEnumerable<Ritual> rituals = Enumerable.Empty<Ritual>();
            if (request.SearchRituals())
            {
                rituals = await _repository.SearchRituals(request, userId, cancellationToken);
            }

            var viewModel = new AbilitiesListViewModel
            {
                Abilities = _mapper.Map<IEnumerable<AbilityDto>>(abilities),
                MysticalPowers = _mapper.Map<IEnumerable<MysticalPowerDto>>(mysticalPowers),
                Rituals = _mapper.Map<IEnumerable<RitualDto>>(rituals)
            };

            return viewModel;
        }
    }
}
