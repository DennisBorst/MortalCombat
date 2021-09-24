using System;

namespace ToolBox.Injection
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class DependencyAttribute : Attribute
    {
        public DependencyAttribute() { }
    }
}
