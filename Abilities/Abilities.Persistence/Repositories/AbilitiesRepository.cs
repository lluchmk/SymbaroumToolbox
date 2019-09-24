using Abilities.Application.Abilities.Queries.SearchAbilities;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Abilities.Persistence.Repositories
{
    public class AbilitiesRepository : IAbilitiesRepository
    {
        private readonly AbilitiesDbContext _context;

        public AbilitiesRepository(AbilitiesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BaseAbility>> Search(SearchAbilitiesQuery query, string userId, CancellationToken cancellationToken)
        {
            var response = (await _context.Abilities
                .ToListAsync())
                .Where(s => s.UserId == null || s.UserId == userId);

            response = response.Where(a => query.GetRequestedTypes().Contains(a.GetType()));

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                response = response.Where(s => s.Name.IndexOf(query.Name, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return response;
        }

        public async Task<int> Create(BaseAbility baseAbility, CancellationToken cancellationToken)
        {
            await _context.Abilities.AddAsync(baseAbility, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var createdId = baseAbility.Id;
            return createdId;
        }

        public async Task<BaseAbility> GetById(int abilityId, CancellationToken cancellationToken)
        {
            var ability = await _context.Abilities.Where(a => a.Id == abilityId)
                .SingleOrDefaultAsync(cancellationToken);

            return ability;
        }

        public async Task Update(BaseAbility ability, CancellationToken cancellationToken)
        {
            _context.Abilities.Update(ability);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(BaseAbility ability, CancellationToken cancellationToken)
        {
            _context.Abilities.Remove(ability);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> IsAbilityOwner(int abilityId, string userId)
        {
            return await _context.Abilities.AnyAsync(a => a.Id == abilityId && a.UserId == userId);
        }
    }
}
