using Abilities.Application.Abilities.Queries.SearchAbilities;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<TAbility>> Search<TAbility>(SearchAbilitiesQuery query, string userId, CancellationToken cancellationToken)
            where TAbility : BaseAbility
        {
            var abilities = _context.Abilities.Where(a => a.GetType() == typeof(TAbility));

            var response = abilities.OfType<TAbility>()
                .Where(s => s.UserId == null || s.UserId == userId);

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                response = response.Where(s => s.Name.Contains(query.Name));
            }

            return await response.ToListAsync(cancellationToken);
        }

        public async Task<int> Create<TAbility>(TAbility baseAbility, CancellationToken cancellationToken)
            where TAbility : BaseAbility
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
    }
}
