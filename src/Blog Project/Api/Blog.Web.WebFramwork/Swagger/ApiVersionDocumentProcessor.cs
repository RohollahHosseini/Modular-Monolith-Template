using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Web.WebFramwork.Swagger
{
    public class ApiVersionDocumentProcessor : IDocumentProcessor
    {
        public void Process(DocumentProcessorContext context)
        {

            // Filter out operations that do not match the current document version
            var version = context.Document.Info.Version; // e.g., "v1"

            var pathsToRemove = context.Document.Paths
                .Where(pathItem => !RegExHelpers.MatchesApiVersion(version, pathItem.Key))
                .Select(path => path.Key)
                .ToList();


            foreach (var path in pathsToRemove)
            {
                context.Document.Paths.Remove(path);
            }
        }
    }
}
