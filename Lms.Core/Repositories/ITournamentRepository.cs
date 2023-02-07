using Lms.Core.Models.Entities;

namespace CodeEvents.Api.Core.Repositories
{
    public interface ITournamentRepository
    {
        Task<PagedList<Tournament>> GetAllAsync(TournamentParameters tournamentParameters);
        Task<IEnumerable<Tournament>> GetAllAsync();
        Task<Tournament> GetAsync(int id);
        Task<bool> AnyAsync(int id);
        Task<IEnumerable<Tournament>> GetAllWithGamesAsync();

        //non async methods
        PagedList<Tournament> GetAll(TournamentParameters tournamentParameters);
        Tournament GetById(int id);
        void CreateTournament(Tournament tournament);
        void UpdateTournament(Tournament tournament);
        void RemoveTournament(Tournament tournament);


    }
}
