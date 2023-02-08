using Lms.Core.Repositories;
using Lms.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly LmsApiContext _ctx;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IGameRepository _gameRepository;

        public UnitOfWork(LmsApiContext ctx)
        {
            _ctx = ctx;
            _tournamentRepository = new TournamentRepository(ctx);
            _gameRepository = new GameRepository(ctx);
        }

        public ITournamentRepository TournamentRepository => _tournamentRepository;

        public IGameRepository GameRepository => _gameRepository;

        public async Task CompleteAsync()
        {
           await _ctx.SaveChangesAsync();
        }
    }
}
