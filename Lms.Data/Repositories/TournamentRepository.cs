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
    public class TournamentRepository : ITournamentRepository
    {
        private LmsApiContext _ctx;

        public TournamentRepository(LmsApiContext ctx)
        {
            _ctx = ctx;
        }
        public void Add(Tournament tournament)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AnyAsync(int id)
        {
           return await _ctx.Tournament?.AnyAsync(e => e.Id == id);
        }

        public async Task<Tournament> GetAsync(int id)
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));
            return await _ctx.Tournament.FindAsync(id);

        }


        public async Task<IEnumerable<Tournament>> GetAllAsync()
        {
            return await _ctx.Tournament.ToListAsync();
        }

        public async Task<IEnumerable<Tournament>> GetAllWithGamesAsync()
        {
            return await _ctx.Tournament.Include(t => t.Games).ToListAsync();
        }

        public void Remove(Tournament tournament)
        {
            throw new NotImplementedException();
        }

        public void Update(Tournament tournament)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync(ApiParameters apiParameters)
        {
            return await _ctx.Tournament
                .OrderBy(on=>on.StartDate)
                .Skip((apiParameters.PageNumber-1)*apiParameters.PageSize)
                .Take(apiParameters.PageSize)
                .ToListAsync()
                ;
        }
    }
}
