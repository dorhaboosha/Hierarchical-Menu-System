using System;
using System.Linq;

namespace Menus.Test
{
    /// <summary>
    /// Shared helper methods for menu actions. Used by both the events-based
    /// and interfaces-based menu implementations to avoid code duplication.
    /// </summary>
    internal static class MenuActionsHelper
    {
        /// <summary>
        /// Displays the current date.
        /// </summary>
        internal static void ShowDate()
        {
            Console.WriteLine("The current date is : {0}\n\n", DateTime.Today.ToString("dd/MM/yyyy"));
        }

        /// <summary>
        /// Displays the current time.
        /// </summary>
        internal static void ShowTime()
        {
            Console.WriteLine("The current time is : {0}\n\n", DateTime.Now.ToString("HH:mm:ss"));
        }

        /// <summary>
        /// Displays the application version.
        /// </summary>
        internal static void ShowVersion()
        {
            Console.WriteLine("Version : 24.2.4.9504\n\n");
        }

        /// <summary>
        /// Prompts the user for a sentence and displays the count of capital letters.
        /// </summary>
        internal static void ShowCapitalsCount()
        {
            Console.WriteLine("Please enter your sentence");

            string userInput = Console.ReadLine();
            if (userInput == null)
            {
                Console.WriteLine("No input provided.\n\n");
                return;
            }

            int numberOfCapitalLetters = userInput.Count(char.IsUpper);
            Console.WriteLine("There are {0} capitals in your sentence.\n\n", numberOfCapitalLetters);
        }
    }
}
