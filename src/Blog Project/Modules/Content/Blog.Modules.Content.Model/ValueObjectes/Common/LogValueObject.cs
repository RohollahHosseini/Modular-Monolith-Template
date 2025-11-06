using Blog.BuildingBlocks.Model;

namespace Blog.Modules.Content.Model.ValueObjectes.Blog
{
    public class LogValueObject: ValueObject<LogValueObject>
    {

        public required DateTime NetryDate { get; set; }
        public required string  Message { get; set; }
        public string? AdditionsDescription { get; set; }
    }
    
}
