using System;
using System.Globalization;

namespace ToolBox.Debugger.CommonParsers
{
    public class FloatParser : NumberParser<float>
    {
        public override string formatDescription => "<float>";

        protected override float ParseValue(string value, string parameterName)
        {
            AssertNoText(value, parameterName);

            try
            {
                return float.Parse(value, CultureInfo.InvariantCulture);
            }
            catch (OverflowException)
            {
                throw CreateFloatOverflowException(value, parameterName);
            }
        }
    }
}
