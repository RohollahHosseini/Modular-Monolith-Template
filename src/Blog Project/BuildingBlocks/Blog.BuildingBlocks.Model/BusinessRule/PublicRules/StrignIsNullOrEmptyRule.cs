using Ardalis.GuardClauses;

namespace Blog.BuildingBlocks.Model.BusinessRule.PublicRules
{
    public class StrignIsNullOrEmptyRule(string parameter) : IBusinessRule
    {
        public string Message => $"{Guard.Against.NullOrEmpty(parameter)},{Guard.Against.NullOrEmpty(parameter)}";

        public bool IsBroken()=>string.IsNullOrEmpty(Message)||string.IsNullOrWhiteSpace(Message);
    }
}
