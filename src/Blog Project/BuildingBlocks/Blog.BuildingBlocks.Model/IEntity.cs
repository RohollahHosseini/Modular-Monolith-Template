namespace Blog.BuildingBlocks.Model
{
    public interface IEntity
    {
        bool IsDeleted { get;}
        DateTime CreatedTime { get;}
        DateTime? ModifiedTime { get; set; }
    }
}
