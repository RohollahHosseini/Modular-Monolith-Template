using Blog.BuildingBlocks.Model.BusinessRule;

namespace Blog.Modules.Content.Model.Blog.Rules
{
    public record BlogMustHaveValidUserIdRule(Guid? UserId) : IBusinessRule
    {
        public bool IsBroken()=>UserId == Guid.Empty||UserId ==null;
        public string Message => $"Required input {nameof(UserId)} was empty.";
    }
}
