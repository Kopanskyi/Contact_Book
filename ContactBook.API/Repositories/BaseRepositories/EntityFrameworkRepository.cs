using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ContactBook.API.DataAccess;
using System.Threading;
using ContactBook.API.Models.BaseModel;

namespace ContactBook.API.Repositories.BaseRepositories
{
    public class EntityFrameworkRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IBaseModel
    {
        protected DataContext DbContext { get; private set; }
        protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

        protected EntityFrameworkRepository() { }

        protected EntityFrameworkRepository(DataContext dbContext)
        {
            //this.SetContext(dbContext);
            DbContext = dbContext;
        }

        //public void SetContext(DbContext dbContext)
        //{
        //    this.DbContext = dbContext;
        //}

        public Task<TEntity> GetByIdAsync(TKey id)
        {
            return this.DbSet.FindAsync(id).AsTask();
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await this.DbSet.ToListAsync();
        }

        public TEntity Add(TEntity entity)
        {
            return DbSet.Add(entity).Entity;
        }

        public void Update(TEntity entity)
        {
            this.DbSet.Attach(entity);
            this.DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateWithModifiedInfo(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            this.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            this.DbSet.Remove(entity);
        }

        public void SoftRemove(TEntity entity)
        {
            entity.DeletedAt = DateTime.UtcNow;

            this.Update(entity);
        }

        public async Task SoftRemove(TKey id)
        {
            var entity = await DbSet.FindAsync(id);

            if (entity != null)
            {
                entity.DeletedAt = DateTime.UtcNow;
                this.Update(entity);
            }
        }

        public void Clear()
        {
            this.DbSet.RemoveRange(this.DbSet);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
