using Lms.Core.Models.Entities;
using Lms.Core.Models.Entities.Helpers;
using Lms.Core.Repositories;
using Lms.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Lms.Data.Repositories
{
    internal class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(LmsApiContext ctx) : base(ctx) { }

        public async Task<PagedList<Game>> GetAllAsync(GameParameters parameters)
        {
            var items = FindByCondition(t => t.StartDate.Month >= parameters.MinMonth && t.StartDate.Month <= parameters.MaxMonth);

            items = FilterByString(items, parameters.Title, t => t.Title.ToLower().Contains(parameters.Title.ToLower()));

            items = OrderBySwitch(items, parameters.SortOrder);

            return await PagedList<Game>.ToPagedList(items,
                parameters.PageNumber,
                parameters.PageSize);
        }

        private static IQueryable<Game> OrderBySwitch(IQueryable<Game> items, string sortOrder)
        {
            return sortOrder switch
            {
                "nameDesc" => items = items.OrderByDescending(on => on.Title),
                "dateAsc" => items = items.OrderBy(on => on.StartDate),
                "dateDesc" => items = items.OrderByDescending(on => on.StartDate),
                _ => items = items.OrderBy(on => on.Title),
            };
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            return await FindByCondition(g => g.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public void CreateGame(Game game)
        {
            Ctx.Game.Add(game);
        }

        public void RemoveGame(Game game)
        {
            Ctx.Game.Remove(game);
        }

        public void UpdateGame(int id, Game game)
        {
            Ctx.Game.Update(game);
        }
    }
}
