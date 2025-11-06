using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BuildingBlocks.Infrastrocture
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(
           CancellationToken cancellationToken = default,
           Guid? internalCommandId = null);
    }
}
