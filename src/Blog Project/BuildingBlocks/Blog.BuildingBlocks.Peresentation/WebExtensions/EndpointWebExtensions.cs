using Blog.BuildingBlocks.Application.Events.Result;
using Blog.BuildingBlocks.SharedKernel.Extensions;
using Microsoft.AspNetCore.Http;

namespace Blog.BuildingBlocks.Peresentation.WebExtensions
{
    public static class EndpointWebExtensions
    {
        public static IResult ToEndpointResult<TModel>(this OperationResult<TModel> result)
        {
            ArgumentNullException.ThrowIfNull(result, nameof(OperationResult<TModel>));

            if (result.IsSuccess) return result.Result is bool ? Results.Ok() : Results.Ok(result.Result);

            if (result.IsNotFound)
                return result.ErrorMessages.Any()
                    ? Results.NotFound(result.ErrorMessages.ToGroupedDictionary())
                    : Results.NotFound();

            return string.IsNullOrEmpty(result.GetErrorMessage()) ? Results.BadRequest() : Results.BadRequest(result.ErrorMessages.ToGroupedDictionary());
        }
    }
}
