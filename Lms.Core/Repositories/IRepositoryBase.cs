using System.Linq.Expressions;

namespace Lms.Core.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T entity);
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Remove(T entity);
        void Update(T entity);
    }
}