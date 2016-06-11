using System;

namespace ConsoleObjects
{
    public class Output
    {
        public Output(string text, char filler = ' ')
        {
            Text = text;
            Filler = filler;
        }

        public char Filler { get; }

        public string Text { get; }

        public string GetOutputText(int maximumWidth)
        {
            var width = Text.Length;
            if (width > maximumWidth)
            {
                width = maximumWidth;
            }

            return Text.Substring(0, width) + new string(Filler, maximumWidth - width);
        }
    }
}
