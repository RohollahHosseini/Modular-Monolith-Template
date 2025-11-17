using Blog.BuildingBlocks.Application.CQRS.Command;
using static Blog.BuildingBlocks.Application.CQRS.Command.ICommand;

namespace Blog.BuildingBlocks.Application.Proccessing.InternalCommand
{
    public abstract record InternalCommandBase : ICommand
    {
        protected InternalCommandBase(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    public abstract record InternalCommandBase<TResult> : ICommand<TResult>
    {
        protected InternalCommandBase()
        {
            Id = Guid.NewGuid();
        }

        protected InternalCommandBase(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
