using Ardalis.GuardClauses;

namespace Blog.BuildingBlocks.Model.BusinessRule.PublicRules
{
    public class GuidParameterValidationRule(Guid? parameter) : IBusinessRule
    {
        public string Message => Guard.Against.NullOrEmpty(parameter.ToString(), message: "Invalida UserId");

        public bool IsBroken()=> parameter == Guid.Empty || parameter == null;
    }
}
