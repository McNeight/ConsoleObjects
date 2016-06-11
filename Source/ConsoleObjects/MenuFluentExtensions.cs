using System;

namespace ConsoleObjects
{
    public static class MenuFluentExtensions
    {
        public static void AwaitKeyPress(this MenuGroup group)
        {
            group.Menu.AwaitKeyPress();
        }

        public static MenuGroup AddGroup(this MenuGroup group, string caption = null)
        {
            var newGroup = new MenuGroup(group.Menu, caption);
            group.Menu.Groups.Add(newGroup);
            return newGroup;
        }

        public static MenuGroup AddGroup(this Menu menu, string caption = null)
        {
            var group = new MenuGroup(menu, caption);
            menu.Groups.Add(group);
            return group;
        }

        public static MenuGroup AddItem(this MenuGroup group, ConsoleKey key, string caption, Action onSelection = null)
        {
            group.Items.Add(new MenuItem(group, key, caption, onSelection));
            return group;
        }

        public static MenuGroup AddItem(this MenuGroup group, ConsoleKey key, Action onSelection = null)
        {
            group.Items.Add(new MenuItem(group, key, onSelection));
            return group;
        }

        public static Menu SetBackground(this Menu menu, ConsoleColor color)
        {
            menu.Background = color;
            return menu;
        }

        public static Menu SetIndent(this Menu menu, int indent)
        {
            menu.Indent = indent;
            return menu;
        }

        public static Menu SetTitle(this Menu menu, string title)
        {
            menu.Title = title;
            return menu;
        }

        public static Menu WhenBeginning(this Menu menu, Action onBegin)
        {
            menu.OnBegin = onBegin;
            return menu;
        }

        public static Menu WhenEnding(this Menu menu, Action onEnd)
        {
            menu.OnEnd = onEnd;
            return menu;
        }
    }
}
