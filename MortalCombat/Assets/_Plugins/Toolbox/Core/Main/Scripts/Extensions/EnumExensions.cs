namespace ToolBox
{
    public static class EnumExensions
    {
        public static bool HasBits<T>(this T src, T value) where T : System.Enum
        {
            return ((int)(object)src & (int)(object)value) == (int)(object)value;
        }
    }
}
