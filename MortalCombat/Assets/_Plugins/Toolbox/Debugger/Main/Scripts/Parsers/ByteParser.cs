using System;

namespace ToolBox.Debugger.CommonParsers
{
    public class ByteParser : NumberParser<byte>
    {
        public override string formatDescription => throw new NotImplementedException();

        protected override byte ParseValue(string value, string parameterName)
        {
            AssertNoText(value, parameterName);
            AssertNoDecimal(value, parameterName);

            try
            {
                return byte.Parse(value);
            }
            catch (OverflowException)
            {
                throw CreateOverflowException(value, parameterName, byte.MinValue, byte.MaxValue);
            }
        }
    }
}
