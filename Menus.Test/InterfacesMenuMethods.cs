using Menus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menus.Test
{
    /// <summary>
    /// Contains menu action implementations for the interfaces-based menu demo.
    /// Each nested class implements <see cref="IActionExecutor"/> for a specific menu option.
    /// </summary>
    public class InterfacesMenuMethods
    {
        /// <summary>
        /// Action that displays the current date when selected from the menu.
        /// </summary>
        public class ShowDate : IActionExecutor
        {
            /// <inheritdoc/>
            public void Execute()
            {
                Console.WriteLine("The current date is : {0}\n\n", DateTime.Today.ToString("dd/MM/yyyy"));
            }
        }

        /// <summary>
        /// Action that displays the current time when selected from the menu.
        /// </summary>
        public class ShowTime : IActionExecutor
        {
            /// <inheritdoc/>
            public void Execute()
            {
                Console.WriteLine("The current time is : {0}\n\n", DateTime.Now.ToString("HH:mm:ss"));
            }
        }

        /// <summary>
        /// Action that displays the application version when selected from the menu.
        /// </summary>
        public class ShowVersion : IActionExecutor
        {
            /// <inheritdoc/>
            public void Execute()
            {
                Console.WriteLine("Version : 24.2.4.9504\n\n");
            }
        }

        /// <summary>
        /// Action that prompts the user for a sentence and displays the count of capital letters
        /// when selected from the menu.
        /// </summary>
        public class ShowCapitalsCount : IActionExecutor
        {
            /// <inheritdoc/>
            public void Execute()
            {
                Console.WriteLine("Please enter your sentence");
                
                string userInput = Console.ReadLine();
                if (userInput == null)
                {
                    Console.WriteLine("No input provided.\n\n");
                    return;
                }

                int numberOfCapitalLetters = 0;

                for (int i = 0; i < userInput.Length; i++)
                {
                    char currentChar = userInput[i];

                    if (char.IsUpper(currentChar))
                    {
                        numberOfCapitalLetters++;
                    }
                }

                Console.WriteLine("There are {0} capitals in your sentence.\n\n", numberOfCapitalLetters);
            }
        }
    }
}
