using Lms.Core.Models.Entities;

namespace Lms.Core.Repositories
{
    public interface IGameRepository
    {
        PagedList<Game> GetAll(GameParameters gameParameters);
        Game GetById(int id);
        void CreateGame(Game game);
        void UpdateGame(Game game);
        void RemoveGame(Game game);
    }
}