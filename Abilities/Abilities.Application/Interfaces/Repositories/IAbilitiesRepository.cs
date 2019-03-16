using Abilities.Application.Abilities.Queries.SearchAbilities;
using Abilities.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Abilities.Application.Interfaces.Repositories
{
    public interface IAbilitiesRepository
    {
        Task<IEnumerable<Ability>> SearchAbilities(SearchAbilitiesQuery query, CancellationToken cancellationToken);

        Task<IEnumerable<MysticalPower>> SearchMysticalPowers(SearchAbilitiesQuery query, CancellationToken cancellationToken);

        Task<IEnumerable<Ritual>> SearchRituals(SearchAbilitiesQuery query, CancellationToken cancellationToken);

        Task<int> CreateAbility(Ability ability, CancellationToken cancellationToken);

        Task<int> CreateMysticalPower(MysticalPower mysticalPower, CancellationToken cancellationToken);

        Task<int> CreateRitual(Ritual ritual, CancellationToken cancellationToken);
    }
}
