using System;

namespace ConsoleObjects
{
    public class Colors
    {
        private Colors previousConsoleColors = Colors.None;

        public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.White;

        public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.White;

        public static Colors FromColors(Colors colors)
        {
            return new Colors
            {
                BackgroundColor = colors.BackgroundColor,
                ForegroundColor = colors.ForegroundColor
            };
        }

        public static Colors FromConsoleColors()
        {
            return new Colors
            {
                BackgroundColor = Console.BackgroundColor,
                ForegroundColor = Console.ForegroundColor
            };
        }

        public void SetConsoleColors()
        {
            previousConsoleColors = FromConsoleColors();

            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = ForegroundColor;
        }

        public void RestorePreviousConsoleColors()
        {
            Console.BackgroundColor = previousConsoleColors.BackgroundColor;
            Console.ForegroundColor = previousConsoleColors.ForegroundColor;
        }

        public static Colors Default { get; } = new Colors { BackgroundColor = ConsoleColor.Black, ForegroundColor = ConsoleColor.Gray };

        public static Colors GreenScreenTheme { get; } = new Colors { BackgroundColor = ConsoleColor.Black, ForegroundColor = ConsoleColor.Green };

        public static Colors DarkBlueTheme { get; } = new Colors { BackgroundColor = ConsoleColor.DarkBlue, ForegroundColor = ConsoleColor.White };

        public static Colors DarkCyanTheme { get; } = new Colors { BackgroundColor = ConsoleColor.DarkCyan, ForegroundColor = ConsoleColor.White };

        public static Colors HotDogStandTheme { get; } = new Colors { BackgroundColor = ConsoleColor.DarkRed, ForegroundColor = ConsoleColor.White };

        public static Colors None { get; } = new Colors { BackgroundColor = ConsoleColor.White, ForegroundColor = ConsoleColor.White };
    }
}