using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menus.Events
{
    /// <summary>
    /// Represents a single item in the hierarchical menu. Can act as either a submenu
    /// (containing child items) or an action item (with a handler subscribed to <see cref="MenuItemChosen"/>).
    /// </summary>
    public class MenuItem
    {
        private readonly string r_Title;
        private readonly List<MenuItem> r_ChildrenMenuItems;

        /// <summary>
        /// Raised when this menu item is selected and it is an action item (has no children).
        /// Subscribe to this event to perform an action when the user chooses this option.
        /// </summary>
        public event Action MenuItemChosen;

        private MenuItem m_ParentMenuItem;
        private const int k_BackAndExitOptionNumber = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem"/> class with the specified title.
        /// </summary>
        /// <param name="i_Title">The title displayed for this menu option.</param>
        public MenuItem(string i_Title)
        {
            r_Title = i_Title;
            r_ChildrenMenuItems = new List<MenuItem>();
            m_ParentMenuItem = null;
        }

        /// <summary>
        /// Gets the title of this menu item.
        /// </summary>
        internal string Title
        {
            get
            {
                return r_Title;
            }
        }

        /// <summary>
        /// Gets or sets the parent menu item. Used internally for navigation hierarchy.
        /// </summary>
        internal MenuItem ParentMenu
        {
            get
            {
                return m_ParentMenuItem;
            }

            set
            {
                m_ParentMenuItem = value;
            }
        }

        /// <summary>
        /// Gets the direct child menu items of this menu.
        /// </summary>
        internal List<MenuItem> ChildrenMenuItems
        {
            get
            {
                return r_ChildrenMenuItems;
            }
        }

        /// <summary>
        /// Adds a child menu item to this menu. Only valid for submenu items (those without
        /// a <see cref="MenuItemChosen"/> handler).
        /// </summary>
        /// <param name="i_MenuItem">The menu item to add as a child.</param>
        /// <exception cref="InvalidOperationException">Thrown when this item is an action item and cannot have children.</exception>
        public void AddMenuItem(MenuItem i_MenuItem)
        {
            if (MenuItemChosen == null)
            {
                r_ChildrenMenuItems.Add(i_MenuItem);
                i_MenuItem.ParentMenu = this;
            }
            else
            {
                throw new InvalidOperationException("The item is a Menu Action Item, so you can't add Menu Items under it.");
            }
        }

        /// <summary>
        /// Removes a child menu item from this menu.
        /// </summary>
        /// <param name="i_MenuItem">The menu item to remove.</param>
        /// <exception cref="Exception">Thrown when the item has no children to remove.</exception>
        /// <exception cref="InvalidOperationException">Thrown when attempting to remove from an action item.</exception>
        public void RemoveMenuItem(MenuItem i_MenuItem)
        {
            if (MenuItemChosen == null)
            {
                if (ChildrenMenuItems.Count > 0)
                {
                    r_ChildrenMenuItems.Remove(i_MenuItem);
                    i_MenuItem.ParentMenu = null;
                }
                else
                {
                    throw new Exception("The Menu Item has no Menu Items under it that can be removed from it.");
                }
            }
            else
            {
                throw new InvalidOperationException("This item is a Menu Action Item, so it has no Menu Items under it that can be removed.");
            }
        }

        /// <summary>
        /// Handles the selection of this menu item. If it has children, shows the submenu;
        /// otherwise invokes the <see cref="MenuItemChosen"/> event.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when a submenu item has no children defined.</exception>
        protected virtual void OnMenuItemChosen()
        {
            if (r_ChildrenMenuItems.Count > 0)
            {
                if (MenuItemChosen == null)
                {
                    Show();
                }
                else
                {
                    throw new InvalidOperationException(
                        "Invalid menu state: item has both children and an action handler.");
                }
            }
            else
            {
                if (MenuItemChosen != null)
                {
                    MenuItemChosen.Invoke();
                    m_ParentMenuItem.OnMenuItemChosen();
                }
                else
                {
                    throw new InvalidOperationException("You defined this Menu Item such that it has other Menu Items under it, " +
                        "so you need to add Menu items under it that you will see them.");
                }
            }
        }

        /// <summary>
        /// Displays this menu's options and prompts the user for a choice.
        /// </summary>
        internal void Show()
        {
            showTitle();
            showMenu();
            getUserChoice();
        }

        private void showTitle()
        {
            Console.WriteLine("**{0}**", Title);
            Console.WriteLine("-----------------------");
        }

        private void showMenu()
        {
            int menuItemCounter = 1;

            foreach (MenuItem item in r_ChildrenMenuItems)
            {
                Console.WriteLine("{0} -> {1}", menuItemCounter, item);
                menuItemCounter++;
            }


            if (m_ParentMenuItem != null)
            {
                Console.WriteLine("{0} -> Back", k_BackAndExitOptionNumber);
            }
            else
            {
                Console.WriteLine("{0} -> Exit", k_BackAndExitOptionNumber);
            }

            Console.WriteLine("-----------------------");
        }

        private void getUserChoice()
        {
            int userChoice = askUserChoice();

            Console.Clear();

            if (userChoice == 0)
            {
                if (m_ParentMenuItem != null)
                {
                    m_ParentMenuItem.OnMenuItemChosen();
                }
            }
            else
            {
                r_ChildrenMenuItems[userChoice - 1].OnMenuItemChosen();
            }
        }

        private int askUserChoice()
        {
            Console.WriteLine("Enter your request: (A number between {0} to {1})",
                k_BackAndExitOptionNumber, r_ChildrenMenuItems.Count);
            StringBuilder userOption = new StringBuilder(Console.ReadLine());

            while (!validUserChoice(userOption))
            {
                Console.WriteLine("The number you entered is invalid, please enter again (only number between {0} to {1}):",
                        k_BackAndExitOptionNumber, r_ChildrenMenuItems.Count);
                userOption.Clear();
                userOption.Append(Console.ReadLine());
            }

            int choice;
            int.TryParse(userOption.ToString(), out choice);

            return choice;
        }

        private bool validUserChoice(StringBuilder i_UserChoiceString)
        {
            int userChoiceNumber;
            bool isParsed = int.TryParse(i_UserChoiceString.ToString(), out userChoiceNumber);
            bool validNumberChoice = isParsed &&
                userChoiceNumber >= k_BackAndExitOptionNumber &&
                userChoiceNumber <= r_ChildrenMenuItems.Count;

            return validNumberChoice;
        }

        /// <summary>
        /// Returns the title of this menu item.
        /// </summary>
        /// <returns>The menu item's display title.</returns>
        public override string ToString()
        {
            return Title;
        }
    }
}