namespace Blog.BuildingBlocks.Application.Contracts.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(
           CancellationToken cancellationToken = default,
           Guid? internalCommandId = null);
    }
}
