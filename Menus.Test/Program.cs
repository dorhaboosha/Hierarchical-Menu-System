using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Menus;
using static Menus.Test.InterfacesMenuMethods;

namespace Menus.Test
{
    /// <summary>
    /// Demo application that demonstrates both the interfaces-based and events-based
    /// hierarchical menu implementations with identical menu structures.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Builds the main menu using the interfaces-based implementation
        /// (<see cref="IActionExecutor"/> for menu actions).
        /// </summary>
        /// <returns>A configured interfaces-based main menu.</returns>
        private Interfaces.MainMenu buildInterfacesMainMenu()
        {
            Interfaces.MainMenu interfaceMainMenu = new Interfaces.MainMenu("Interfaces Main Menu");

            Interfaces.MenuItem showVersionAndCountCapitals = new Interfaces.MenuItem("Versions And Capitals");
            Interfaces.MenuItem showDateAndTime = new Interfaces.MenuItem("Show Date/Time");

            Interfaces.MenuItem showDate = new Interfaces.MenuItem("Show Date", new ShowDate());
            Interfaces.MenuItem showTime = new Interfaces.MenuItem("Show Time", new ShowTime());
            Interfaces.MenuItem showVersion = new Interfaces.MenuItem("Show Version", new ShowVersion());
            Interfaces.MenuItem showCapitalsCount = new Interfaces.MenuItem("Count Capitals", new ShowCapitalsCount());

            showDateAndTime.AddMenuItem(showDate);
            showDateAndTime.AddMenuItem(showTime);
            showVersionAndCountCapitals.AddMenuItem(showVersion);
            showVersionAndCountCapitals.AddMenuItem(showCapitalsCount);

            interfaceMainMenu.AddMenuItem(showVersionAndCountCapitals);
            interfaceMainMenu.AddMenuItem(showDateAndTime);

            return interfaceMainMenu;
        }

        /// <summary>
        /// Builds the main menu using the events-based implementation
        /// (delegates for menu actions).
        /// </summary>
        /// <returns>A configured events-based main menu.</returns>
        private Events.MainMenu buildEventsMainMenu()
        {
            Events.MainMenu eventsMainMenu = new Events.MainMenu("Delegates Main Menu");
            EventsMenuMethods eventsMenuMethods = new EventsMenuMethods();

            Events.MenuItem showVersionAndCountCapitals = new Events.MenuItem("Versions And Capitals");
            Events.MenuItem showDateAndTime = new Events.MenuItem("Show Date/Time");

            Events.MenuItem showDate = new Events.MenuItem("Show Date");
            showDate.MenuItemChosen += eventsMenuMethods.ShowDate_Chosen;
            
            Events.MenuItem showTime = new Events.MenuItem("Show Time");
            showTime.MenuItemChosen += eventsMenuMethods.ShowTime_Chosen;

            Events.MenuItem showVersion = new Events.MenuItem("Show Version");
            showVersion.MenuItemChosen += eventsMenuMethods.ShowVersion_Chosen;

            Events.MenuItem showCapitalsCount = new Events.MenuItem("Count Capitals");
            showCapitalsCount.MenuItemChosen += eventsMenuMethods.ShowCapitalsCount_Chosen;

            showVersionAndCountCapitals.AddMenuItem(showVersion);
            showVersionAndCountCapitals.AddMenuItem(showCapitalsCount);
            showDateAndTime.AddMenuItem(showDate);
            showDateAndTime.AddMenuItem(showTime);

            eventsMainMenu.AddMenuItem(showVersionAndCountCapitals);
            eventsMainMenu.AddMenuItem(showDateAndTime);

            return eventsMainMenu;
        }

        /// <summary>
        /// Application entry point. Builds and displays both menu implementations in sequence.
        /// </summary>
        public static void Main()
        {
            Program MainMenusprogram = new Program();

            Interfaces.MainMenu interfaceMainMenu = MainMenusprogram.buildInterfacesMainMenu();
            Events.MainMenu eventsMainMenu = MainMenusprogram.buildEventsMainMenu();

            interfaceMainMenu.Show();
            eventsMainMenu.Show();
        }
    }
}