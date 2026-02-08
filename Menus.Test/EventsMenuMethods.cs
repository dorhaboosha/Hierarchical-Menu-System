using Menus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menus.Test
{
    /// <summary>
    /// Contains menu action handlers for the events-based menu demo.
    /// These methods are invoked when the user selects corresponding menu options.
    /// </summary>
    public class EventsMenuMethods
    {
        /// <summary>
        /// Displays the current date when the "Show Date" menu option is selected.
        /// </summary>
        public void ShowDate_Chosen()
        {
            Console.WriteLine("The current date is : {0}\n\n", DateTime.Today.ToString("dd/MM/yyyy"));
        }

        /// <summary>
        /// Displays the current time when the "Show Time" menu option is selected.
        /// </summary>
        public void ShowTime_Chosen()
        {
            Console.WriteLine("The current time is : {0}\n\n", DateTime.Now.ToString("HH:mm:ss"));
        }

        /// <summary>
        /// Displays the application version when the "Show Version" menu option is selected.
        /// </summary>
        public void ShowVersion_Chosen()
        {
            Console.WriteLine("Version : 24.2.4.9504\n\n");
        }

        /// <summary>
        /// Prompts the user for a sentence and displays the count of capital letters
        /// when the "Count Capitals" menu option is selected.
        /// </summary>
        public void ShowCapitalsCount_Chosen()
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