using Blog.BuildingBlocks.Peresentation.Endpoints;
using Blog.Modules.Content.Infrastrocture.ServiceConfiguration;
using Blog.Modules.LogSystem.Infrastrocture.ServiceConfiguration;
using Blog.BuildingBlocks.Application.ServiceConfiguration;
using System.Reflection;
using Blog.Web.WebFramwork;
using Blog.BuildingBlocks.Infrastrocture.ServiceConfiguration;
using Blog.BuildingBlocks.Peresentation.EndpointFilterPipeline;
using Blog.BuildingBlocks.Peresentation.EndpointFilters;
using Blog.Web.WebFramwork.Swagger;
using Blog.Web.WebFramwork.ServiceConfiguration;
using Blog.Web.WebFramwork.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var configuration=builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwagger("v1", "v1.1");

builder.Services.AddWebFrameworkServices();
builder.Services.ServiceCollectionExtensionsBuildingBlock();
builder.Services.BlogServiceCollactionExtensions(configuration);
builder.Services.LogSystemServiceCollactionExtensions(configuration);

builder.Services.AddExceptionHandler<ExceptionHandler>();


#region Endpoint Filter
builder.Services.AddScoped<IEndpointFilter, ApiResultFilterAttribute>();
builder.Services.AddScoped<IEndpointFilter, BadRequestResultEndpointFilter>();
builder.Services.AddScoped<IEndpointFilter, ContentResultEndpointFilter>();
builder.Services.AddScoped<IEndpointFilter, ModelStateValidationEndpointFilter>();
builder.Services.AddScoped<IEndpointFilter, NotFoundResultEndpointFilter>();

#endregion


Assembly[] moduleApplicationAssemblies = [
   Blog.Modules.Content.Application.AssemblyReference.Assembly,
   Blog.Modules.LogSystem.Application.AssemblyReference.Assembly];

builder.Services.ServiceCollectionExtensionsBuildingBlockApplication(moduleApplicationAssemblies);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    //Migration
    app.ApplyMigrations();

}

app.UseSwaggerAndUi();

app.UseApiResultMiddleware();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapEndpoints();

app.Run();
