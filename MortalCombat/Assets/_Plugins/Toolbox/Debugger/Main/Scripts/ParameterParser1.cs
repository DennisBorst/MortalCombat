using System;

namespace ToolBox.Debugger
{
    public abstract class ParameterParser<T> : ParameterParser
    {
        public override bool AcceptsType(Type type)
        {
            return type == typeof(T);
        }

        public override sealed object Parse(string value, string parameterName)
        {
            return ParseValue(value, parameterName);
        }

        protected abstract T ParseValue(string value, string parameterName);
    } 
}
