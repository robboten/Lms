using Lms.Core.Models.Entities;
using Lms.Core.Repositories;
using Lms.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Repositories
{
    internal class GameRepository : IGameRepository
    {
        private LmsApiContext _ctx;
        public GameRepository(LmsApiContext ctx)
        {
            _ctx = ctx;
        }

        public void Add(Game game)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Game> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _ctx.Game.ToListAsync();
        }

        public void Remove(Game game)
        {
            throw new NotImplementedException();
        }

        public void Update(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
