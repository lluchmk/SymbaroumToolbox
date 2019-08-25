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

        public async Task<IEnumerable<TSkill>> Search<TSkill>(SearchAbilitiesQuery query, string userId, CancellationToken cancellationToken)
            where TSkill : BaseAbility
        {
            var skills = _context.Abilities.Where(a => a.GetType() == typeof(TSkill));

            var response = skills.OfType<TSkill>()
                .Where(s => s.UserId == null || s.UserId == userId);

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                response = response.Where(s => s.Name.Contains(query.Name));
            }

            return await response.ToListAsync(cancellationToken);
        }

        public async Task<int> Create<TSkill>(TSkill baseAbility, CancellationToken cancellationToken)
            where TSkill : BaseAbility
        {
            await _context.Abilities.AddAsync(baseAbility, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var createdId = baseAbility.Id;
            return createdId;
        }
    }
}
