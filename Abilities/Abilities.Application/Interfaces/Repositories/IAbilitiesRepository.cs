using Abilities.Application.Abilities.Queries.SearchAbilities;
using Abilities.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Abilities.Application.Interfaces.Repositories
{
    public interface IAbilitiesRepository
    {
        Task<IEnumerable<BaseAbility>> Search(SearchAbilitiesQuery query, string userId, CancellationToken cancellationToken);

        Task<int> Create(BaseAbility ability, CancellationToken cancellationToken);

        Task<BaseAbility> GetById(int abilityId, CancellationToken cancellationToken);

        Task Update(BaseAbility ability, CancellationToken cancellationToken);

        Task Delete(BaseAbility ability, CancellationToken cancellationToken);

        Task<bool> IsAbilityOwner(int abilityId, string userId);
    }
}
