using Lms.Core.Models.Entities;
using Lms.Core.Models.Entities.Helpers;

namespace Lms.Core.Repositories
{
    public interface IGameRepository
    {
        Task<PagedList<Game>> GetAllAsync(GameParameters parameters);
        Task<Game?> GetByIdAsync(int id);
        void CreateGame(Game game);
        void UpdateGame(int id, Game game);
        void RemoveGame(Game game);
    }
}