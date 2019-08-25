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

        Task<int> CreateAbility(Ability ability, CancellationToken cancellationToken);

        Task<int> CreateMysticalPower(MysticalPower mysticalPower, CancellationToken cancellationToken);

        Task<int> CreateRitual(Ritual ritual, CancellationToken cancellationToken);
    }
}
