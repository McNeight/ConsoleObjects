using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleObjects
{
    public class Menu
    {
        private bool exit;

        public ConsoleColor Background { get; set; }

        public ICollection<MenuGroup> Groups { get; private set; } = new List<MenuGroup>();

        public int EndingCursorTop { get; private set; }

        public int Indent { get; set; }

        public Action OnBegin { get; set; }

        public Action OnEnd { get; set; }

        public string Title { get; set; }

        public void AwaitKeyPress()
        {
            OnBegin?.Invoke();
            if (!string.IsNullOrWhiteSpace(Title))
            {
                Console.Title = Title;
            }
            Console.BackgroundColor = Background;
            Console.Clear();

            foreach (var group in Groups)
            {
                if (!string.IsNullOrWhiteSpace(group.Caption))
                {
                    Console.WriteLine();
                    Console.WriteLine(new string(' ', Indent) + group.Caption);
                }

                Console.WriteLine();
                foreach (var item in group.Items)
                {
                    if (!string.IsNullOrWhiteSpace(item.Caption))
                    {
                        Console.WriteLine(new string(' ', Indent) + item.Caption.Replace("{key}", item.Key.ToString()));
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine(" " + new string('┈', Console.BufferWidth - 2) + " ");

            EndingCursorTop = Console.CursorTop;

            while (!exit)
            {
                var key = Console.ReadKey(true).Key;
                var menuItem = Groups.SelectMany(g => g.Items).SingleOrDefault(i => i.Key == key);
                if (menuItem != null)
                {
                    if (menuItem.OnSelection == null)
                    {
                        exit = true;
                    }
                    else
                    {
                        menuItem.OnSelection.Invoke();
                    }
                }
            }
            OnEnd?.Invoke();
        }
    }
}