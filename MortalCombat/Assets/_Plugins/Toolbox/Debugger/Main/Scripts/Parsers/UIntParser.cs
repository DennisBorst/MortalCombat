using System;

namespace ToolBox.Debugger.CommonParsers
{
    public class UIntParser : NumberParser<uint>
    {
        public override string formatDescription => "<positive number>";

        protected override uint ParseValue(string value, string parameterName)
        {
            AssertNoText(value, parameterName);
            AssertNoDecimal(value, parameterName);

            try
            {
                return uint.Parse(value);
            }
            catch (OverflowException)
            {
                throw CreateOverflowException(value, parameterName, uint.MinValue, uint.MaxValue);
            }
        }
    }
}
