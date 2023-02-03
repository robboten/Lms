using Lms.Core.Models.Entities;

namespace Lms.Core.Repositories
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAll();
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game> Get(int id);
        Task<bool> AnyAsync(int id);
        void Add(Game game);
        void Update(Game game);
        void Remove(Game game);
    }
}