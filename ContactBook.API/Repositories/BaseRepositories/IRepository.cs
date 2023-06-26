using ContactBook.API.Models.BaseModel;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContactBook.API.Repositories.BaseRepositories
{
    public interface IRepository<TEntity, in TKey> where TEntity : class, IBaseModel
    {
        Task<TEntity> GetByIdAsync(TKey id);
        Task<ICollection<TEntity>> GetAllAsync();
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void UpdateWithModifiedInfo(TEntity entity);
        void Remove(TEntity entity);
        void SoftRemove(TEntity entity);
        Task SoftRemove(TKey id);
        void Clear();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
