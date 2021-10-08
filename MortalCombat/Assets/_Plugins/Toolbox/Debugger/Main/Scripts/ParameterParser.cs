using System;

namespace ToolBox.Debugger
{
    public abstract class ParameterParser
    {
        public abstract string formatDescription { get; }
        public abstract bool AcceptsType(Type type);
        public abstract object Parse(string value, string parameterName);
    }
}
