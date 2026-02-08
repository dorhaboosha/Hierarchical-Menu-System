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
            public void Execute() => MenuActionsHelper.ShowDate();
        }

        /// <summary>
        /// Action that displays the current time when selected from the menu.
        /// </summary>
        public class ShowTime : IActionExecutor
        {
            /// <inheritdoc/>
            public void Execute() => MenuActionsHelper.ShowTime();
        }

        /// <summary>
        /// Action that displays the application version when selected from the menu.
        /// </summary>
        public class ShowVersion : IActionExecutor
        {
            /// <inheritdoc/>
            public void Execute() => MenuActionsHelper.ShowVersion();
        }

        /// <summary>
        /// Action that prompts the user for a sentence and displays the count of capital letters
        /// when selected from the menu.
        /// </summary>
        public class ShowCapitalsCount : IActionExecutor
        {
            /// <inheritdoc/>
            public void Execute() => MenuActionsHelper.ShowCapitalsCount();
        }
    }
}
