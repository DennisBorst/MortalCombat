using System;

namespace ToolBox.Debugger
{
    [Serializable]
    public class DebuggerFormatException : Exception
    {
        public DebuggerFormatException() { }
        public DebuggerFormatException(string message) : base(message) { }
        public DebuggerFormatException(string message, Exception inner) : base(message, inner) { }
        protected DebuggerFormatException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
