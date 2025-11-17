using Blog.BuildingBlocks.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Blog.BuildingBlocks.Infrastrocture.BaseRespository
{
    public class BaseAsyncRepository<TEntity,TDbContext>
        where TEntity : class,IEntity
        where TDbContext: DbContext
    {
        public readonly TDbContext DbContext ;
        protected DbSet<TEntity> Entities { get; }
        protected virtual IQueryable<TEntity> Table => Entities;
        protected virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTrackingWithIdentityResolution();

        protected BaseAsyncRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
            Entities = DbContext.Set<TEntity>(); // City => Cities
        }

        protected virtual async Task<List<TEntity>> ListAllAsync()
        {
            return await Entities.ToListAsync();
        }

        protected virtual async Task AddAsync(TEntity entity,CancellationToken cancellationToken=default)
        {
            await Entities.AddAsync(entity,cancellationToken);

        }

        protected virtual async Task UpdateAsync(
            Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression)
        {
            await Entities.ExecuteUpdateAsync(updateExpression);
        }

        protected virtual async Task UpdateAsync(
            Expression<Func<TEntity, bool>> whereExpression, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression)
        {
            await Entities.Where(whereExpression).ExecuteUpdateAsync(updateExpression);
        }

        protected virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> deleteExpression)
        {
            await Entities.Where(deleteExpression).ExecuteDeleteAsync();
        }



    }
}
