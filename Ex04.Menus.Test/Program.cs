using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus;
using static Ex04.Menus.Test.IenterfacesMenuMethods;

namespace Ex04.Menus.Test
{
    public class Program
    {
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