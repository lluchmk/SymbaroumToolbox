using Abilities.Application.Abilities.Queries.SearchAbilities;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Abilities.Persistence.EFInMemory.Repositories
{
    public class AbilitiesRepository : IAbilitiesRepository
    {
        private readonly AbilitiesDbContext _context;

        public AbilitiesRepository(AbilitiesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ability>> SearchAbilities(SearchAbilitiesQuery query, CancellationToken cancellationToken) =>
            await Search(query, _context.Abilities, cancellationToken);

        public async Task<IEnumerable<MysticalPower>> SearchMysticalPowers(SearchAbilitiesQuery query, CancellationToken cancellationToken) =>
            await Search(query, _context.MysticalPowers, cancellationToken);

        public async Task<IEnumerable<Ritual>> SearchRituals(SearchAbilitiesQuery query, CancellationToken cancellationToken) =>
            await Search(query, _context.Rituals, cancellationToken);

        private async Task<IEnumerable<TSkill>> Search<TSkill>(SearchAbilitiesQuery query, DbSet<TSkill> dbSet, CancellationToken cancellationToken)
            where TSkill : BaseAbility
        {
            return string.IsNullOrWhiteSpace(query.Name) ?
                await dbSet.ToListAsync(cancellationToken) :
                await dbSet.Where(s => s.Name.Contains(query.Name)).ToListAsync(cancellationToken);
        }

        public async Task<int> CreateAbility(Ability ability, CancellationToken cancellationToken) =>
            await Create(ability, _context.Abilities, cancellationToken);

        public async Task<int> CreateMysticalPower(MysticalPower mysticalPower, CancellationToken cancellationToken) =>
            await Create(mysticalPower, _context.MysticalPowers, cancellationToken);

        public async Task<int> CreateRitual(Ritual ritual, CancellationToken cancellationToken) =>
            await Create(ritual, _context.Rituals, cancellationToken);

        public async Task<int> Create<TSkill>(TSkill baseAbility, DbSet<TSkill> dbSet, CancellationToken cancellationToken)
            where TSkill : BaseAbility
        {
            await dbSet.AddAsync(baseAbility, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var createdId = baseAbility.Id;
            return createdId;
        }
    }
}
