using Microsoft.AspNetCore.Routing;

namespace Blog.BuildingBlocks.Peresentation.Endpoints
{
    public interface IEndpoint
    {
        void MapEndpoint(IEndpointRouteBuilder app);
    }
}
