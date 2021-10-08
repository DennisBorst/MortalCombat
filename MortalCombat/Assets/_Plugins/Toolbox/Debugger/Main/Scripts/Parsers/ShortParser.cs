using System;

namespace ToolBox.Debugger.CommonParsers
{
    public class ShortParser : NumberParser<short>
    {
        public override string formatDescription => throw new NotImplementedException();

        protected override short ParseValue(string value, string parameterName)
        {
            AssertNoText(value, parameterName);
            AssertNoDecimal(value, parameterName);

            try
            {
                return short.Parse(value);
            } 
            catch (OverflowException)
            {
                throw CreateOverflowException(value, parameterName, (ulong)short.MaxValue, (ulong)short.MaxValue);
            }
        }
    }
}
