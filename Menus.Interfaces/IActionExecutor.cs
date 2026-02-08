using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menus.Interfaces
{
    /// <summary>
    /// Defines a contract for objects that can execute an action.
    /// </summary>
    public interface IActionExecutor
    {
        /// <summary>
        /// Performs the action associated with this executor.
        /// </summary>
        void Execute();
    }
}
