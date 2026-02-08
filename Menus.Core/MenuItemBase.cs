using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        private readonly ReadOnlyCollection<TMenuItem> r_ChildrenMenuItemsReadOnly;
        protected TMenuItem m_ParentMenuItem;
        protected const int k_BackAndExitOptionNumber = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemBase{TMenuItem}"/> class.
        /// </summary>
        /// <param name="i_Title">The title displayed for this menu option.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="i_Title"/> is null or empty.</exception>
        protected MenuItemBase(string i_Title)
        {
            if (string.IsNullOrEmpty(i_Title))
            {
                throw new ArgumentException("Menu title cannot be null or empty.", nameof(i_Title));
            }

            r_Title = i_Title;
            r_ChildrenMenuItems = new List<TMenuItem>();
            r_ChildrenMenuItemsReadOnly = r_ChildrenMenuItems.AsReadOnly();
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
        /// Gets a read-only view of the direct child menu items of this menu.
        /// </summary>
        internal IReadOnlyList<TMenuItem> ChildrenMenuItems => r_ChildrenMenuItemsReadOnly;

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
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="i_MenuItem"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when this item is an action item and cannot have children.</exception>
        public void AddMenuItem(TMenuItem i_MenuItem)
        {
            if (i_MenuItem == null)
            {
                throw new ArgumentNullException(nameof(i_MenuItem));
            }

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
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="i_MenuItem"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when attempting to remove from an action item.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the specified menu item is not a child of this menu.</exception>
        public void RemoveMenuItem(TMenuItem i_MenuItem)
        {
            if (i_MenuItem == null)
            {
                throw new ArgumentNullException(nameof(i_MenuItem));
            }

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
        /// Displays this menu's options and handles user navigation until the user chooses to exit.
        /// Uses an iterative loop to avoid unbounded stack growth from deep menu hierarchies.
        /// </summary>
        public void Show()
        {
            TMenuItem current = (TMenuItem)this;

            while (current != null)
            {
                current.showTitle();
                current.showMenu();
                int userChoice = current.askUserChoice();

                Console.Clear();

                current = current.GetNextMenuAfterChoice(userChoice);
            }
        }

        /// <summary>
        /// Returns the next menu to display based on the user's choice, or null to exit.
        /// For choice 0: returns the parent (or null at root). For choice N: either returns
        /// the child submenu to display, or invokes the action and returns the current menu
        /// so the user stays on the same menu after the action.
        /// </summary>
        /// <param name="i_Choice">The user's choice (0 = back/exit, 1..N = child index).</param>
        /// <returns>The next menu to display, or null to exit.</returns>
        private TMenuItem GetNextMenuAfterChoice(int i_Choice)
        {
            if (i_Choice == k_BackAndExitOptionNumber)
            {
                return m_ParentMenuItem;
            }

            TMenuItem selectedItem = r_ChildrenMenuItems[i_Choice - 1];

            if (selectedItem.IsActionItem)
            {
                if (selectedItem.r_ChildrenMenuItems.Count > 0)
                {
                    throw new InvalidOperationException(
                        "Invalid menu state: item has both children and an action.");
                }

                selectedItem.InvokeAction();
                return (TMenuItem)this;
            }
            else
            {
                return selectedItem;
            }
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

        private int askUserChoice()
        {
            Console.WriteLine("Enter your request: (A number between {0} to {1})",
                k_BackAndExitOptionNumber, r_ChildrenMenuItems.Count);
            string userOption = Console.ReadLine() ?? string.Empty;

            int choice;
            while (!tryParseUserChoice(userOption, out choice))
            {
                Console.WriteLine("The number you entered is invalid, please enter again (only number between {0} to {1}):",
                    k_BackAndExitOptionNumber, r_ChildrenMenuItems.Count);
                userOption = Console.ReadLine() ?? string.Empty;
            }

            return choice;
        }

        private bool tryParseUserChoice(string i_UserChoiceString, out int o_Choice)
        {
            int userChoiceNumber;
            bool isParsed = int.TryParse(i_UserChoiceString, out userChoiceNumber);
            bool validNumberChoice = isParsed &&
                userChoiceNumber >= k_BackAndExitOptionNumber &&
                userChoiceNumber <= r_ChildrenMenuItems.Count;

            o_Choice = userChoiceNumber;
            return validNumberChoice;
        }

        /// <summary>
        /// Returns the title of this menu item.
        /// </summary>
        public override string ToString() => Title;
    }
}
