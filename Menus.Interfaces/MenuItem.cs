using System;
using Menus.Core;

namespace Menus.Interfaces
{
    /// <summary>
    /// Represents a single item in the hierarchical menu. Can act as either a submenu
    /// (containing child items) or an action item (with an <see cref="IActionExecutor"/> that performs the action).
    /// </summary>
    public class MenuItem : MenuItemBase<MenuItem>
    {
        private readonly IActionExecutor r_MenuItemAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem"/> class.
        /// </summary>
        /// <param name="i_Title">The title displayed for this menu option.</param>
        /// <param name="i_MenuItemAction">Optional. The action executor for when this item is selected. If null, this item acts as a submenu.</param>
        public MenuItem(string i_Title, IActionExecutor i_MenuItemAction = null)
            : base(i_Title)
        {
            r_MenuItemAction = i_MenuItemAction;
        }

        /// <summary>
        /// Gets the action executor for this menu item. Null when this item is a submenu.
        /// </summary>
        internal IActionExecutor MenuItemAction => r_MenuItemAction;

        /// <summary>
        /// Indicates whether this item is an action item (has an executor assigned).
        /// </summary>
        protected override bool IsActionItem => r_MenuItemAction != null;

        /// <summary>
        /// Invokes the action executor when this action item is selected.
        /// </summary>
        protected override void InvokeAction()
        {
            r_MenuItemAction.Execute();
        }
    }
}
