﻿using Abilities.Application.Abilities.Queries.Dtos;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Application.Interfaces.Services;
using Abilities.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Abilities.Application.Abilities.Queries.SearchAbilities
{
    public class SearchAbilitiesQueryHandler : IRequestHandler<SearchAbilitiesQuery, AbilitiesListViewModel>
    {
        private readonly IAbilitiesRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public SearchAbilitiesQueryHandler(IAbilitiesRepository repository, IMapper mapper, IUsersService usersService)
        {
            _repository = repository;
            _mapper = mapper;
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

            IEnumerable<Ability> abilities = Enumerable.Empty<Ability>();
            if (request.SearchAbilities())
            {
                abilities = allAbilities.Where(a => a.GetType() == typeof(Ability))
                    .Cast<Ability>();
            }

            IEnumerable<MysticalPower> mysticalPowers = Enumerable.Empty<MysticalPower>();
            if (request.SearchMysticalPowers())
            {
                mysticalPowers = allAbilities.Where(a => a.GetType() == typeof(MysticalPower))
                    .Cast<MysticalPower>();
            }

            IEnumerable<Ritual> rituals = Enumerable.Empty<Ritual>();
            if (request.SearchRituals())
            {
                rituals = allAbilities.Where(a => a.GetType() == typeof(Ritual))
                    .Cast<Ritual>();
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
