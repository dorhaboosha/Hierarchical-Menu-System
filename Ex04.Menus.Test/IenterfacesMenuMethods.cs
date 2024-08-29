using Ex04.Menus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Test
{
    public class IenterfacesMenuMethods
    {
        public class ShowDate : IActionExector
        {
            public void Execute()
            {
                Console.WriteLine("The current date is : {0}\n\n", DateTime.Today.ToString("dd/MM/yyyy"));
            }
        }

        public class ShowTime : IActionExector
        {
            public void Execute()
            {
                Console.WriteLine("The current time is : {0}\n\n", DateTime.Now.ToString("HH:mm:ss"));
            }
        }

        public class ShowVersion : IActionExector
        {
            public void Execute()
            {
                Console.WriteLine("Version : 24.2.4.9504\n\n");
            }
        }

        public class ShowCapitalsCount : IActionExector
        {
            public void Execute()
            {
                Console.WriteLine("Please enter your sentence");
                
                string userInput = Console.ReadLine();
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