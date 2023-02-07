using Lms.Core.Models.Entities;
using Lms.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Repositories
{
    public abstract class RepositoryBase<T> where T : class
    {
        protected LmsApiContext Ctx { get; set; }

        public RepositoryBase(LmsApiContext lmsApiContext)
        {
            Ctx= lmsApiContext;
        }

        public IQueryable<T> FindAll()
        {
            return Ctx.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T,bool>> expression)
        {

            return Ctx.Set<T>()
                .Where(expression)
                .AsNoTracking();
        }

        public void Remove(T entity)
        {
            Ctx.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            Ctx.Set<T>().Update(entity);
        }
        public void Add(T entity)
        {
            Ctx.Set<T>().Add(entity);

        }
    }
}
