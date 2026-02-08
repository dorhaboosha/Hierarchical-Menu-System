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
        public void ShowDate_Chosen() => MenuActionsHelper.ShowDate();

        /// <summary>
        /// Displays the current time when the "Show Time" menu option is selected.
        /// </summary>
        public void ShowTime_Chosen() => MenuActionsHelper.ShowTime();

        /// <summary>
        /// Displays the application version when the "Show Version" menu option is selected.
        /// </summary>
        public void ShowVersion_Chosen() => MenuActionsHelper.ShowVersion();

        /// <summary>
        /// Prompts the user for a sentence and displays the count of capital letters
        /// when the "Count Capitals" menu option is selected.
        /// </summary>
        public void ShowCapitalsCount_Chosen() => MenuActionsHelper.ShowCapitalsCount();
    }
}