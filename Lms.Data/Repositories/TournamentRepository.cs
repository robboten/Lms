using Lms.Common.Entities;
using Lms.Common.Helpers;
using Lms.Core.Models.Entities;
using Lms.Core.Models.Entities.Helpers;
using Lms.Core.Repositories;
using Lms.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Lms.Data.Repositories
{
    public class TournamentRepository : RepositoryBase<Tournament>, ITournamentRepository
    {

        public TournamentRepository(LmsApiContext ctx) : base(ctx)
        {
        }


        //what to await for here? Everything is just IQuerable... 
        public async Task<PagedList<Tournament>> GetAllAsync(TournamentParameters tournamentParameters)
        {
            var tournaments = FindByCondition(t => t.StartDate.Month >= tournamentParameters.MinMonth && t.StartDate.Month <= tournamentParameters.MaxMonth);

            //bugs if using ToLowerInvariant
            tournaments = FilterByString(tournaments, tournamentParameters.Title, t => t.Title.ToLower().Contains(tournamentParameters.Title.ToLower()));

            tournaments = IncludeEntity(tournaments, tournamentParameters.IncludeGames, t => t.Games.OrderBy(g => g.StartDate));

            tournaments = OrderBySwitch(tournaments, tournamentParameters.SortOrder);

            return await PagedList<Tournament>.ToPagedList(tournaments,
                tournamentParameters.PageNumber,
                tournamentParameters.PageSize);
        }

        private static IQueryable<Tournament> OrderBySwitch(IQueryable<Tournament> tournaments, string sortOrder)
        {
            return sortOrder switch
            {
                "nameDesc" => tournaments = tournaments.OrderByDescending(on => on.Title),
                "dateAsc" => tournaments = tournaments.OrderBy(on => on.StartDate),
                "dateDesc" => tournaments = tournaments.OrderByDescending(on => on.StartDate),
                _ => tournaments = tournaments.OrderBy(on => on.Title),
            };
        }

        public async Task<Tournament?> GetByIdAsync(int id, TournamentParameters tournamentParameters)
        {
            //var tournament = FindByCondition(tournament => tournament.Id.Equals(id));

            //tournament = IncludeEntity(tournament, tournamentParameters.IncludeGames, t => t.Games.OrderBy(g => g.StartDate).ToList());

            //return tournament.FirstOrDefault();

            var tournament = await FindByCondition(tournament => tournament.Id.Equals(id)).FirstOrDefaultAsync();

            if (tournament == null)
                return null;

            //the game array is still showing in the json, is there a way to remove it here?
            if (tournamentParameters.IncludeGames && tournament.Games != null)
                tournament.Games = tournament.Games.OrderBy(g => g.StartDate).ToList();

            return tournament;
        }

        public async Task<Tournament?> GetByIdAsync(int id)
        {
            return await FindByCondition(tournament => tournament.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task CreateTournament(Tournament tournament)
        {
            await Ctx.Tournament.AddAsync(tournament);
        }

        public void RemoveTournament(Tournament tournament)
        {
            Ctx.Tournament.Remove(tournament);
        }

        public void UpdateTournament(Tournament tournament)
        {
            Ctx.Tournament.Update(tournament);
        }



        //
        //older (unused) async methods
        //
        //public async Task<bool> AnyAsync(int id)
        //{
        //    return await Ctx.Tournament?.AnyAsync(e => e.Id == id);
        //}

        //public async Task<Tournament> GetByIdAsync(int id)
        //{
        //    ArgumentNullException.ThrowIfNull(id, nameof(id));
        //    return await Ctx.Tournament.FindAsync(id);

        //}

        //public async Task<IEnumerable<Tournament>> GetAllAsync()
        //{
        //    throw new NotImplementedException();
        //}

        

        //public async Task<IEnumerable<Tournament>> GetAllWithGamesAsync()
        //{
        //    return await Ctx.Tournament.Include(t => t.Games).ToListAsync();
        //}


    }
}
