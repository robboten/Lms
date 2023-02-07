using CodeEvents.Api.Core.Repositories;
using Lms.Core.Models.Entities;
using Lms.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Repositories
{
    public class TournamentRepository : RepositoryBase<Tournament>, ITournamentRepository
    {

        public TournamentRepository(LmsApiContext ctx) : base(ctx)
        {
        }

        //
        //methods implemented by base class
        //
        public PagedList<Tournament> GetAll(TournamentParameters tournamentParameters)
        {
            var tournaments= FindByCondition(t=>t.StartDate.Month>=tournamentParameters.MinMonth && t.StartDate.Month <=tournamentParameters.MaxMonth);

            GetByName(ref tournaments, tournamentParameters.Title);

            var sorted = tournaments.OrderBy(on => on.Title);

            if (!string.IsNullOrWhiteSpace(tournamentParameters.SortOrder))
            {
                switch (tournamentParameters.SortOrder)
                {
                    //case "nameAsc":
                    //    sorted = tournaments.OrderBy(on => on.Title);
                    //    break;
                    case "nameDesc":
                        sorted = tournaments.OrderByDescending(on => on.Title);
                        break;
                    case "dateAsc":
                        sorted = tournaments.OrderBy(on => on.StartDate);
                        break;
                    case "dateDesc":
                        sorted = tournaments.OrderByDescending(on => on.StartDate);
                        break;
                }
            }

            return PagedList<Tournament>.ToPagedList(sorted, 
                tournamentParameters.PageNumber, 
                tournamentParameters.PageSize);
        }

        private void GetByName(ref IQueryable<Tournament> tournaments, string name)
        {
            if(!tournaments.Any() || string.IsNullOrWhiteSpace(name))
            {
                return;
            }
            tournaments = tournaments.Where(t=>t.Title.ToLower().Contains(name.ToLower()));
        }

        public Tournament GetById(int id)
        {
            var tournament = FindByCondition(tournament => tournament.Id.Equals(id)).FirstOrDefault();
            return tournament;
        }

        public void CreateTournament(Tournament tournament)
        {
            Ctx.Tournament.Add(tournament);
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
        public async Task<bool> AnyAsync(int id)
        {
            return await Ctx.Tournament?.AnyAsync(e => e.Id == id);
        }

        public async Task<Tournament> GetAsync(int id)
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));
            return await Ctx.Tournament.FindAsync(id);

        }

        public async Task<IEnumerable<Tournament>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<Tournament>> GetAllAsync(TournamentParameters tournamentParameters)
        {
            var tournaments = Ctx.Tournament;

            var sorted = tournaments.OrderBy(on => on.Title);

            if (!string.IsNullOrWhiteSpace(tournamentParameters.SortOrder))
            {
                switch (tournamentParameters.SortOrder)
                {
                    //case "nameAsc":
                    //    sorted = tournaments.OrderBy(on => on.Title);
                    //    break;
                    case "nameDesc":
                        sorted = tournaments.OrderByDescending(on => on.Title);
                        break;
                    case "dateAsc":
                        sorted = tournaments.OrderBy(on => on.StartDate);
                        break;
                    case "dateDesc":
                        sorted = tournaments.OrderByDescending(on => on.StartDate);
                        break;
                }
            }

            return PagedList<Tournament>.ToPagedList(
                sorted,
                tournamentParameters.PageNumber, tournamentParameters.PageSize)
                ;
        }

        public async Task<IEnumerable<Tournament>> GetAllWithGamesAsync()
        {
            return await Ctx.Tournament.Include(t => t.Games).ToListAsync();
        }

    }
}
