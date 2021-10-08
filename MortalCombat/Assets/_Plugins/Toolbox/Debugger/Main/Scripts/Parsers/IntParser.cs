using System;

namespace ToolBox.Debugger.CommonParsers
{
    public class IntParser : NumberParser<int>
    {
        public override string formatDescription => "<number>";

        protected override int ParseValue(string value, string parameterName)
        {
            AssertNoText(value, parameterName);
            AssertNoDecimal(value, parameterName);

            try
            {
                return int.Parse(value);
            }
            catch (OverflowException)
            {
                throw CreateOverflowException(value, parameterName, int.MaxValue, int.MaxValue);
            }
        }
    }
}
