using System;

namespace ConsoleObjects
{
    public class Position
    {
        private Position(int left, int top)
        {
            Left = left;
            Top = top;
        }

        public int Left { get; set; }

        public int Top { get; set; }

        public void IncreaseLeft(int count)
        {
            Left = Left + count;
        }

        public void IncreaseTop(int count)
        {
            Top = Top + count;
        }

        public void SetConsoleCursorPosition(int? leftIncrement = null, int? topIncrement = null)
        {
            if (leftIncrement.HasValue)
            {
                IncreaseLeft(leftIncrement.Value);
            }

            if (topIncrement.HasValue)
            {
                IncreaseTop(topIncrement.Value);
            }

            Console.CursorLeft = Left;
            Console.CursorTop = Top;
        }

        public static Position NewPosition()
        {
            return new Position(0, 0);
        }

        public static Position FromPosition(Position position, Func<int> getIncrement = null)
        {
            var increment = 0;
            if (getIncrement != null)
            {
                increment = getIncrement.Invoke();
            }

            return Position.FromValues(position.Left + increment, position.Top + increment);
        }

        public static Position FromValues(int left, int top)
        {
            return new Position(left, top);
        }
    }
}