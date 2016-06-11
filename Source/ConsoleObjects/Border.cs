namespace ConsoleObjects
{
    public class Border
    {
        public Colors Colors { get; set; }

        public bool IsLeftShared { get; set; } = true;

        public bool IsTopShared { get; set; } = true;

        public bool IsVisible { get; set; } = true;

        public Size Size { get; set; }
    }
}