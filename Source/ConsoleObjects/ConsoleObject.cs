using System;
using System.Text;

namespace ConsoleObjects
{
    public class ConsoleObject : Section
    {
        public ConsoleObject(int width, int height, string title = null) : base(null, ConsoleObjects.Size.FromValues(width, height), title)
        {
            Border.IsVisible = false;
            MinimumSize = Size.FromSize(Size);
            Title.Text = title;

            Console.OutputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;
            Console.SetWindowSize(Size.Width.Value, Size.Height.Value);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight + 1);
            Console.Title = Title.Text;
        }

        public void DecreaseHeight()
        {
            var currentHeight = Console.WindowHeight;
            if (currentHeight >= MinimumSize.Height.Value)
            {
                Console.WindowHeight = currentHeight - 1;
            }
            else
            {
                Console.Beep();
            }
        }

        public void DecreaseWidth()
        {
            var currentWidth = Console.WindowWidth;
            if (currentWidth >= MinimumSize.Width.Value)
            {
                Console.WindowWidth = currentWidth - 1;
            }
            else
            {
                Console.Beep();
            }
        }

        public void IncreaseHeight()
        {
            var currentHeight = Console.WindowHeight;
            if (currentHeight < Console.LargestWindowHeight)
            {
                Console.WindowHeight = currentHeight + 1;
            }
            else
            {
                Console.Beep();
            }
        }

        public void IncreaseWidth()
        {
            var currentWidth = Console.WindowWidth;
            if (currentWidth < Console.LargestWindowWidth)
            {
                Console.WindowWidth = currentWidth + 1;
            }
            else
            {
                Console.Beep();
            }
        }
    }
}