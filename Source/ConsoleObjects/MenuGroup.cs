using System.Collections.Generic;

namespace ConsoleObjects
{
    public class MenuGroup
    {
        public MenuGroup(Menu menu, string caption)
        {
            Menu = menu;
            Caption = caption;
        }

        public string Caption { get; set; }

        public Menu Menu { get; set; }

        public ICollection<MenuItem> Items { get; private set; } = new List<MenuItem>();
    }
}