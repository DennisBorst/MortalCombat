using System;
using System.Reflection;
using ToolBox.Services;

namespace ToolBox.Debugger
{
    public class DebugService : IBootstrapService
    {

    }

    public abstract class DebugCommandBase
    {
        internal string Path { get; }
        internal string Description { get; }
        internal Action<bool> IsEnabled = null;
        internal Delegate action;

        internal ParameterInfo parameterTypes;

        protected void Register(Delegate action)
        {
            ParameterInfo[] info = action.Method.GetParameters();
        }
    }

    public class DebugAction<T> where T : Delegate
    {
        public DebugAction(string path, string description)
        {
        }
    }
}
