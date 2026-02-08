using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menus.Interfaces
{
    /// <summary>
    /// Represents the root of a hierarchical menu system using the <see cref="IActionExecutor"/>
    /// interface for menu actions. Provides a simple API for building and displaying
    /// console-based menus with nested submenus.
    /// </summary>
    public class MainMenu
    {
        private readonly MenuItem r_MainMenu;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenu"/> class with the specified title.
        /// </summary>
        /// <param name="i_Title">The title displayed at the top of the menu.</param>
        public MainMenu(string i_Title)
        {
            r_MainMenu = new MenuItem(i_Title);
        }

        /// <summary>
        /// Adds a menu item as a top-level option in the main menu.
        /// </summary>
        /// <param name="i_MenuItem">The menu item to add (can be a submenu or an action item).</param>
        public void AddMenuItem(MenuItem i_MenuItem)
        {
            r_MainMenu.AddMenuItem(i_MenuItem);
        }

        /// <summary>
        /// Removes a menu item from the main menu's top-level options.
        /// </summary>
        /// <param name="i_MenuItem">The menu item to remove.</param>
        public void RemoveMenuItem(MenuItem i_MenuItem)
        {
            r_MainMenu.RemoveMenuItem(i_MenuItem);
        }

        /// <summary>
        /// Displays the menu and handles user navigation until the user chooses to exit.
        /// </summary>
        public void Show()
        {
            r_MainMenu.Show();
        }
    }
}