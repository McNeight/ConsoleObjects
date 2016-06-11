using System;

namespace ConsoleObjects
{
    public class MenuItem
    {
        public MenuItem(MenuGroup group, ConsoleKey key, string caption, Action onSelection) : this(group, key, onSelection)
        {
            Caption = caption;
        }

        public MenuItem(MenuGroup group, ConsoleKey key, Action onSelection)
        {
            Group = group;
            Key = key;
            OnSelection = onSelection;
        }

        public MenuGroup Group { get; set; }

        public ConsoleKey Key { get; set; }

        public Action OnSelection { get; set; }

        public string Caption { get; set; }
    }
}