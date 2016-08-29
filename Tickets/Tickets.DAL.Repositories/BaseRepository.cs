using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.DAL.Interfaces;

namespace Tickets.DAL.Repositories
{
    public class BaseRepository<T>:IRepository<T> where T:class 
    {
        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public BaseRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(long id)
        {
            return DbSet.Find(id);
        }

        public T GetById(string id)
        {
            return DbSet.Find(id);
        }

        public void Create(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            dbEntityEntry.State = EntityState.Added;
        }

        public void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            T entity = GetById(id);
            if (entity != null)
            {
                DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
                dbEntityEntry.State = EntityState.Deleted;
            }
        }
    }
}
