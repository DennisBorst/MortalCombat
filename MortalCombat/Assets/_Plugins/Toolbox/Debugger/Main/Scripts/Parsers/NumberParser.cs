using System;

namespace ToolBox.Debugger.CommonParsers
{
    public abstract class NumberParser<T> : ParameterParser<T>
    {
        public void AssertNoDecimal(string value, string parameterName)
        {
            if (value.Contains("."))
                throw new DebuggerFormatException($"Parameter {parameterName} of type {typeof(T).Name} cannot contain a decimal.");
        }

        public void AssertNoText(string value, string parameterName)
        {
            for (int i = 0; i < value.Length; i++)
                if (!char.IsNumber(value[i]) && value[i] != '.') // dot is required for decimals
                    throw new DebuggerFormatException($"Parameter {parameterName} of type {typeof(T).Name} cannot contain text characters.");
        }

        public Exception CreateOverflowException(string value, string parameterName, ulong positiveMinValue, ulong maxValue)
        {
            string minValue = positiveMinValue > 0 ? $"-{positiveMinValue}" : $"{positiveMinValue}";

            return new DebuggerFormatException($"The value '{value}' for parameter '{parameterName}' was either too large or too small for number type {typeof(T).Name}.\n" +
                $"Make sure the number is bigger than {minValue} and lower than {maxValue}.");
        }

        public Exception CreateFloatOverflowException(string value, string parameterName)
        {
            return new DebuggerFormatException($"The value '{value}' for parameter '{parameterName}' was either too large or too small for number type {typeof(T).Name}.\n" +
                $"Make sure the number is bigger than {float.MinValue} and lower than {float.MaxValue}.");
        }

        public Exception CreateDecimalOverflowException(string value, string parameterName)
        {
            return new DebuggerFormatException($"The value '{value}' for parameter '{parameterName}' was either too large or too small for number type {typeof(T).Name}.\n" +
                $"Make sure the number is bigger than {decimal.MinValue} and lower than {decimal.MaxValue}.");
        }
    }
}
