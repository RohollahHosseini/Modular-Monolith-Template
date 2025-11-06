namespace Blog.BuildingBlocks.Model.BusinessRule
{
    public interface IBusinessRule
    {
        bool IsBroken();

        string Message { get; }
    }
}
