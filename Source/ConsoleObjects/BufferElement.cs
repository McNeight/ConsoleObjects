using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleObjects
{
    public class BufferElement
    {
        private char character;
        private Colors colors;

        public BufferElement(Buffer buffer, BufferRow row)
        {
            Buffer = buffer;
            Row = row;
        }

        public Buffer Buffer { get; private set; }

        public char Character
        {
            get { return character; }
            set
            {
                if (character == value) return;

                character = value;
                Buffer.RaiseElementChanged(this);
            }
        }

        public BufferRow Row { get; private set; }

        public Position BufferPosition => Position.FromValues(Row.Elements.IndexOf(this), Buffer.Rows.IndexOf(Row));

        public BufferElement GetRelatedElement(Direction direction, int skip = 1)
        {
            var rowIndex = Buffer.Rows.IndexOf(Row);
            var elementIndex = Row.Elements.IndexOf(this);

            switch (direction)
            {
                case Direction.Right:
                    elementIndex += skip;
                    break;
                case Direction.Left:
                    elementIndex -= skip;
                    break;
                case Direction.Up:
                    rowIndex -= skip;
                    break;
                case Direction.Down:
                    rowIndex += skip;
                    break;
            }

            if (rowIndex < 0 || rowIndex >= Buffer.Rows.Count)
            {
                return null;
            }

            if (elementIndex < 0 || elementIndex >= Buffer.Rows[rowIndex].Elements.Count)
            {
                return null;
            }

            return Buffer.Rows[rowIndex].Elements[elementIndex];
        }

        public IEnumerable<BufferElement> GetRelatedElements(Direction direction, int skip, int length)
        {
            return Enumerable.Range(0, length).Select(i => GetRelatedElement(direction, skip + i));
        }

        public Colors Colors
        {
            get { return colors; }
            set
            {
                if (colors == value) return;

                colors = value;
                Buffer.RaiseElementChanged(this);
            }
        }

        public bool IsInscribed { get; set; }

        public bool IsLastInRow => Row.Elements.Last() == this;

        public void SetCharacter(char character, params BufferElement[] elements)
        {
            if (elements.All(e => e != null && e.IsInscribed)) Character = character;
        }

        public void WriteToConsole(Position position)
        {
            if (Colors.IsNullOrNone())
            {
                Colors = Colors.FromColors(Buffer.Colors);
            }
            Colors.SetConsoleColors();

            position.SetConsoleCursorPosition();

            Console.Write(Character);
        }

        public override string ToString()
        {
            return Character.ToString();
        }
    }
}