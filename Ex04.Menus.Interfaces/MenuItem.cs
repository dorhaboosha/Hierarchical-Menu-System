using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        private readonly string r_Title;
        private readonly List<MenuItem> r_ChildrenMenuItems;
        private readonly IActionExector r_MenuItemAction;
        private MenuItem m_ParentMenuItem;
        private const int k_BackAndExitOptionNumber = 0;

        public MenuItem(string i_Title, IActionExector i_MenuItemAction = null)
        {
            r_Title = i_Title;
            r_ChildrenMenuItems = new List<MenuItem>();
            m_ParentMenuItem = null;
            r_MenuItemAction = i_MenuItemAction;
        }

        internal string Title
        {
            get
            {
                return r_Title;
            }
        }

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

        internal List<MenuItem> ChildrenMenuItems
        {
            get
            {
                return r_ChildrenMenuItems;
            }
        }

        internal IActionExector MenuItemAction
        {
            get
            {
                return r_MenuItemAction; 
            }
        }

        public void AddMenuItem(MenuItem i_MenuItem)
        {
            if (r_MenuItemAction == null)
            {
                r_ChildrenMenuItems.Add(i_MenuItem);
                i_MenuItem.ParentMenu = this;
            }
            else 
            {
                throw new FormatException("The item is a Menu Action Item, so you cann't add Menu Items under it.");
            }
        }

        public void RemoveMenuItem(MenuItem i_MenuItem)
        {
            if (i_MenuItem.MenuItemAction == null)
            {

                if (ChildrenMenuItems.Count > 0)
                {
                    r_ChildrenMenuItems.Remove(i_MenuItem);
                    i_MenuItem.ParentMenu = null;
                }
                else
                {
                    throw new Exception("The Menu Item has no Menu Items unser it that can be removed from it.");
                }
            }
            else
            {
                throw new FormatException("The item is a Menu Action Item so it has no Menu Items under it that can be removed.");
            }
        }

        internal void OnMenuItemChosen()
        {
            if (r_ChildrenMenuItems.Count > 0)
            {
                if (r_MenuItemAction == null)
                {
                    Show();
                }
            }
            else
            {
                if (r_MenuItemAction != null)
                {
                    r_MenuItemAction.Execute();
                    m_ParentMenuItem.OnMenuItemChosen();
                }
                else
                {
                    throw new FormatException("You defined this Menu Item such that it has other Menu Items under it, " +
                        "so you need to add Menu items under it that you will see them.");
                }
            }
        }

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
            bool inputIsNullOrEmpty = string.IsNullOrEmpty(i_UserChoiceString.ToString());
            bool inputIsOneNumber = i_UserChoiceString.Length == 1 && Char.IsDigit(i_UserChoiceString[0]);
            int userChoiceNumber;
            int.TryParse(i_UserChoiceString.ToString(), out userChoiceNumber);
            bool validNumberChoice = userChoiceNumber >= k_BackAndExitOptionNumber &&
                userChoiceNumber <= r_ChildrenMenuItems.Count;

            return !inputIsNullOrEmpty && inputIsOneNumber && validNumberChoice;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}