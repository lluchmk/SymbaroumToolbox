using Abilities.Application.Abilities.Queries.SearchAbilities;
using Abilities.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Abilities.Application.Interfaces.Repositories
{
    public interface IAbilitiesRepository
    {
        Task<IEnumerable<TSkill>> Search<TSkill>(SearchAbilitiesQuery query, string userId, CancellationToken cancellationToken)
            where TSkill : BaseAbility;

        Task<int> Create<TSkill>(TSkill ability, CancellationToken cancellationToken)
            where TSkill : BaseAbility;
    }
}
