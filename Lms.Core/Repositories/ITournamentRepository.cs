using Lms.Core.Models.Entities;
using Lms.Core.Models.Entities.Helpers;

namespace Lms.Core.Repositories
{
    public interface ITournamentRepository
    {
        Task<PagedList<Tournament>> GetAllAsync(TournamentParameters tournamentParameters);
        //Task<IEnumerable<Tournament>> GetAllAsync();
        ////Task<Tournament> GetByIdAsync(int id);
        //Task<bool> AnyAsync(int id);
        //Task<IEnumerable<Tournament>> GetAllWithGamesAsync();

        Task<Tournament?> GetByIdAsync(int id, TournamentParameters tournamentParameters);
        Task<Tournament?> GetByIdAsync(int id);
        Task CreateTournament(Tournament tournament);
        void UpdateTournament(Tournament tournament);
        void RemoveTournament(Tournament tournament);


    }
}
