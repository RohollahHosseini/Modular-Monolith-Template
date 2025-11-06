using Blog.BuildingBlocks.Application.CQRS.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Modules.LogSystem.Application.Features
{
    public record CreateLogCommand(string Description):ICommand<bool>;
}
