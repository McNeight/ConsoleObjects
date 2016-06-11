using System;

namespace ConsoleObjects
{
    public class Title
    {
        public Alignment Alignment { get; set; }

        public bool IsVisible { get; set; } = true;

        public int LeftPadding { get; set; }

        public int LeftPush { get; set; }

        public int RightPush { get; set; }

        public int RightPadding { get; set; }

        public string Text { get; set; }
    }
}
