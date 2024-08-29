using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu
    {
        private readonly MenuItem r_MainMenu;

        public MainMenu(string i_Title)
        {
            r_MainMenu = new MenuItem(i_Title);
        }

        public void AddMenuItem(MenuItem i_MenuItem)
        {
            r_MainMenu.AddMenuItem(i_MenuItem);
        }

        public void RemoveMenuItem(MenuItem i_MenuItem)
        {
            r_MainMenu.RemoveMenuItem(i_MenuItem);
        }

        public void Show()
        {
            r_MainMenu.Show();
        }
    }
}