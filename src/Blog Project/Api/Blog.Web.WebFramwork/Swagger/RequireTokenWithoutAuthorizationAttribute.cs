using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Web.WebFramwork.Swagger
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    public class RequireTokenWithoutAuthorizationAttribute: Attribute
    {
    };
}
