namespace ConsoleObjects
{
    public static class ColorsExtensions
    {
        public static bool IsNullOrNone(this Colors colors)
        {
            return colors == null || colors == Colors.None;
        }
    }
}
