using Blog.BuildingBlocks.Peresentation.EndpointFilters.EndpointFilterAbstract;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BuildingBlocks.Peresentation.EndpointFilters
{
    public class ModelStateValidationEndpointFilter : IApiEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var validationSummery = new Dictionary<string, List<string>>();

            foreach (var contextArgument in context.Arguments)
            {
                if (contextArgument is null)
                    continue;

                var validator =
                    context.HttpContext.RequestServices.GetService(
                        typeof(IValidator<>).MakeGenericType(contextArgument.GetType())) as IValidator;

                if (validator is null)
                    continue;

                var validationResult = await validator.ValidateAsync(new ValidationContext<object>(contextArgument));

                if (validationResult.IsValid) continue;

                foreach (var validationResultError in validationResult.Errors)
                {
                    if (validationSummery.TryGetValue(validationResultError.PropertyName, out var value))
                    {
                        value.Add(validationResultError.ErrorMessage);
                        continue;
                    }

                    validationSummery.Add(validationResultError.PropertyName, new() { validationResultError.ErrorMessage });
                }
            }

            if (validationSummery.Count == 0)
                return await next(context);

            return Results.BadRequest(validationSummery);
        }
    }
}
