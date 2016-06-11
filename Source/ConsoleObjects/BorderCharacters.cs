namespace ConsoleObjects
{
    public class BorderCharacters
    {
        public BorderCharacters(string characters = "─│┌┐└┘├┤┬┴┼")
        {
            Horizontal = characters.ToCharArray(0, 1)[0];
            Vertical = characters.ToCharArray(1, 1)[0];
            DownAndRight = characters.ToCharArray(2, 1)[0];
            DownAndLeft = characters.ToCharArray(3, 1)[0];
            UpAndRight = characters.ToCharArray(4, 1)[0];
            UpAndLeft = characters.ToCharArray(5, 1)[0];
            VerticalAndRight = characters.ToCharArray(6, 1)[0];
            VerticalAndLeft = characters.ToCharArray(7, 1)[0];
            DownAndHorizontal = characters.ToCharArray(8, 1)[0];
            UpAndHorizontal = characters.ToCharArray(9, 1)[0];
            VerticalAndHorizontal = characters.ToCharArray(10, 1)[0];
        }

        public char DownAndLeft { get; set; }

        public char DownAndRight { get; set; }

        public char Horizontal { get; set; }

        public char UpAndLeft { get; set; }

        public char UpAndRight { get; set; }

        public char Vertical { get; set; }

        public char VerticalAndLeft { get; set; }

        public char VerticalAndRight { get; set; }

        public char DownAndHorizontal { get; set; }

        public char UpAndHorizontal { get; set; }

        public char VerticalAndHorizontal { get; set; }
    }
}