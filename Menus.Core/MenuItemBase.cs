using System;
using System.Collections.Generic;
using System.Text;

namespace Menus.Core
{
    /// <summary>
    /// Base class for hierarchical menu items. Provides shared navigation and display logic.
    /// Subclasses implement the action mechanism (e.g. events or an action executor interface).
    /// </summary>
    /// <typeparam name="TMenuItem">The concrete menu item type (CRTP for self-referential hierarchy).</typeparam>
    public abstract class MenuItemBase<TMenuItem> where TMenuItem : MenuItemBase<TMenuItem>
    {
        protected readonly string r_Title;
        protected readonly List<TMenuItem> r_ChildrenMenuItems;
        protected TMenuItem m_ParentMenuItem;
        protected const int k_BackAndExitOptionNumber = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemBase{TMenuItem}"/> class.
        /// </summary>
        /// <param name="i_Title">The title displayed for this menu option.</param>
        protected MenuItemBase(string i_Title)
        {
            r_Title = i_Title;
            r_ChildrenMenuItems = new List<TMenuItem>();
            m_ParentMenuItem = null;
        }

        /// <summary>
        /// Gets the title of this menu item.
        /// </summary>
        internal string Title => r_Title;

        /// <summary>
        /// Gets or sets the parent menu item. Used internally for navigation hierarchy.
        /// </summary>
        internal TMenuItem ParentMenu
        {
            get => m_ParentMenuItem;
            set => m_ParentMenuItem = value;
        }

        /// <summary>
        /// Gets the direct child menu items of this menu.
        /// </summary>
        internal List<TMenuItem> ChildrenMenuItems => r_ChildrenMenuItems;

        /// <summary>
        /// Indicates whether this item is an action item (vs. a submenu).
        /// </summary>
        protected abstract bool IsActionItem { get; }

        /// <summary>
        /// Invokes the action when this item is selected. Only called when <see cref="IsActionItem"/> is true.
        /// </summary>
        protected abstract void InvokeAction();

        /// <summary>
        /// Adds a child menu item to this menu. Only valid for submenu items.
        /// </summary>
        /// <param name="i_MenuItem">The menu item to add as a child.</param>
        /// <exception cref="InvalidOperationException">Thrown when this item is an action item and cannot have children.</exception>
        public void AddMenuItem(TMenuItem i_MenuItem)
        {
            if (!IsActionItem)
            {
                r_ChildrenMenuItems.Add(i_MenuItem);
                i_MenuItem.ParentMenu = (TMenuItem)this;
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
        /// <exception cref="InvalidOperationException">Thrown when attempting to remove from an action item.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the specified menu item is not a child of this menu.</exception>
        public void RemoveMenuItem(TMenuItem i_MenuItem)
        {
            if (!IsActionItem)
            {
                bool removed = r_ChildrenMenuItems.Remove(i_MenuItem);
                if (removed)
                {
                    i_MenuItem.ParentMenu = null;
                }
                else
                {
                    throw new InvalidOperationException("The specified menu item is not a child of this menu and therefore cannot be removed.");
                }
            }
            else
            {
                throw new InvalidOperationException("This item is a Menu Action Item, so it has no Menu Items under it that can be removed.");
            }
        }

        /// <summary>
        /// Handles the selection of this menu item. If it has children, shows the submenu;
        /// otherwise invokes the action.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when a submenu item has no children defined.</exception>
        internal void OnMenuItemChosen()
        {
            if (r_ChildrenMenuItems.Count > 0)
            {
                if (!IsActionItem)
                {
                    Show();
                }
                else
                {
                    throw new InvalidOperationException(
                        "Invalid menu state: item has both children and an action.");
                }
            }
            else
            {
                if (IsActionItem)
                {
                    InvokeAction();
                    m_ParentMenuItem?.OnMenuItemChosen();
                }
                else
                {
                    throw new InvalidOperationException(
                        "You defined this Menu Item such that it has other Menu Items under it, " +
                        "so you need to add Menu items under it that you will see them.");
                }
            }
        }

        /// <summary>
        /// Displays this menu's options and prompts the user for a choice.
        /// </summary>
        public void Show()
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

            foreach (TMenuItem item in r_ChildrenMenuItems)
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
            string input = Console.ReadLine();
            StringBuilder userOption = new StringBuilder(input ?? string.Empty);

            while (!validUserChoice(userOption))
            {
                Console.WriteLine("The number you entered is invalid, please enter again (only number between {0} to {1}):",
                    k_BackAndExitOptionNumber, r_ChildrenMenuItems.Count);
                userOption.Clear();
                userOption.Append(Console.ReadLine() ?? string.Empty);
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
        public override string ToString() => Title;
    }
}
