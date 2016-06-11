namespace ConsoleObjects
{
    public class Margin
    {
        public Margin(int sideMargins, int topAndBottomMargins)
        {
            Top = topAndBottomMargins;
            Bottom = topAndBottomMargins;
            Left = sideMargins;
            Right = sideMargins;
        }

        public Margin(int margin)
        {
            Top = margin;
            Bottom = margin;
            Left = margin;
            Right = margin;
        }

        public Margin(Margin margin)
        {
            Top = margin.Top;
            Bottom = margin.Bottom;
            Left = margin.Left;
            Right = margin.Right;
        }

        public int Bottom { get; private set; }

        public int Left { get; private set; }

        public int Right { get; private set; }

        public int Top { get; private set; }
    }
}
