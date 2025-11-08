using System.Reflection;

namespace Blog.Modules.LogSystem.Application
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
