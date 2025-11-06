using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Modules.LogSystem.Domain.Log.Repository
{
    public interface ILogRepository
    {
        Task AddLogAsync(LogEntity logEntity);
    }
}
