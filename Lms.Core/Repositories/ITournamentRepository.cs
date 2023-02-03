using Lms.Core.Models.Entities;

namespace CodeEvents.Api.Core.Repositories
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<Tournament>> GetAllAsync(ApiParameters apiParameters);
        Task<IEnumerable<Tournament>> GetAllAsync();
        Task<Tournament> GetAsync(int id);
        Task<bool> AnyAsync(int id);
        void Add(Tournament tournament);
        void Update(Tournament tournament);
        void Remove(Tournament tournament);

        Task<IEnumerable<Tournament>> GetAllWithGamesAsync();
    }
}
