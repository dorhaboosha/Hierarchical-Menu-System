using System;
using Menus.Core;

namespace Menus.Events
{
    /// <summary>
    /// Represents a single item in the hierarchical menu. Can act as either a submenu
    /// (containing child items) or an action item (with a handler subscribed to <see cref="MenuItemChosen"/>).
    /// </summary>
    public class MenuItem : MenuItemBase<MenuItem>
    {
        /// <summary>
        /// Raised when this menu item is selected and it is an action item (has no children).
        /// Subscribe to this event to perform an action when the user chooses this option.
        /// </summary>
        public event Action MenuItemChosen;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem"/> class with the specified title.
        /// </summary>
        /// <param name="i_Title">The title displayed for this menu option.</param>
        public MenuItem(string i_Title)
            : base(i_Title)
        {
        }

        /// <summary>
        /// Indicates whether this item is an action item (has a handler subscribed).
        /// </summary>
        protected override bool IsActionItem => MenuItemChosen != null;

        /// <summary>
        /// Invokes the subscribed handlers when this action item is selected.
        /// </summary>
        protected override void InvokeAction()
        {
            MenuItemChosen?.Invoke();
        }
    }
}
