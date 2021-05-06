using FavoDeMel.Domain.Interfaces;
using FavorDeMel.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FavoDeMel.Infrastructure.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly FavoDeMelDbContext _context;
        protected readonly DbSet<TEntity> DbSet;

        public RepositoryBase(FavoDeMelDbContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
            _context.SaveChanges();
        }

        public virtual TEntity GetById(long id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
            _context.SaveChanges();
        }

        public virtual void Remove(long id)
        {
            DbSet.Remove(DbSet.Find(id));
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
