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
    internal class GameRepository : RepositoryBase<Game>,IGameRepository
    {
        public GameRepository(LmsApiContext ctx): base(ctx) { }



        public PagedList<Game> GetAll(GameParameters gameParameters)
        {
            var items = FindByCondition(t => t.StartDate.Month >= gameParameters.MinMonth && t.StartDate.Month <= gameParameters.MaxMonth);

            GetByName(ref items, gameParameters.Title);

            return PagedList<Game>.ToPagedList(items.OrderBy(on => on.Title),
                gameParameters.PageNumber,
                gameParameters.PageSize);
        }

        private void GetByName(ref IQueryable<Game> games, string name)
        {
            if (!games.Any() || string.IsNullOrWhiteSpace(name))
            {
                return;
            }
            games = games.Where(t => t.Title.ToLower().Contains(name.ToLower()));
        }

        public Game GetById(int id)
        {
            var game = FindByCondition(g =>g.Id.Equals(id)).FirstOrDefault();
            return game;
        }
        public void CreateGame(Game game)
        {
            Ctx.Game.Add(game);
        }

        public void RemoveGame(Game game)
        {
            Ctx.Game.Remove(game);
        }

        public void UpdateGame(Game game)
        {
            Ctx.Game.Update(game);
        }
    }
}
