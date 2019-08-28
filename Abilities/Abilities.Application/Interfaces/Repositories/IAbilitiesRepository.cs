using Abilities.Application.Abilities.Queries.SearchAbilities;
using Abilities.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Abilities.Application.Interfaces.Repositories
{
    public interface IAbilitiesRepository
    {
        Task<IEnumerable<TAbility>> Search<TAbility>(SearchAbilitiesQuery query, string userId, CancellationToken cancellationToken)
            where TAbility : BaseAbility;

        Task<int> Create<TAbility>(TAbility ability, CancellationToken cancellationToken)
            where TAbility : BaseAbility;

        Task<BaseAbility> GetById(int abilityId, CancellationToken cancellationToken);

        Task Update(BaseAbility ability, CancellationToken cancellationToken);
    }
}
