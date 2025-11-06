using Blog.BuildingBlocks.Model;

namespace Blog.Modules.LogSystem.Domain.Log
{
    public class LogEntity:BaseEntity<Guid>
    {
        public string  LogDescription { get; set; }
    }
}
