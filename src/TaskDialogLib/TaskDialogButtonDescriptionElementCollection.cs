using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace Flatcode.Presentation
{
    /// <summary>
    /// Represents a collection of <see cref="TaskDialogButtonDescriptionElementCollection"/> objects.
    /// </summary>
    [ContentWrapper(typeof(TaskDialogButtonDescriptionRun))]
    [WhitespaceSignificantCollection]
    public class TaskDialogButtonDescriptionElementCollection :
        TaskDialogElementCollection<TaskDialogButtonDescriptionElement>
    {
        #region Methods

        /// <summary>
        /// Adds a <see cref="TaskDialogButtonDescriptionRun"/> instance to this
        /// <see cref="TaskDialogButtonDescriptionElementCollection"/>.
        /// </summary>
        /// <param name="item">The <see cref="TaskDialogButtonDescriptionRun"/> to be added.</param>
        public void AddRun(TaskDialogButtonDescriptionRun item)
        {
            Add(item);
        }

        #endregion

        #region Methods: Overridden

        /// <summary>
        /// This method is implementation-specific and not intended to be used from third-party
        /// code.
        /// </summary>
        /// <param name="item">This argument is implementation-specific and not intended to be used
        /// from third-party code.</param>
        /// <returns>The return value is implementation-specific and not intended to be used from
        /// third-party code.</returns>
        protected internal override Int32 AddInternal(Object item)
        {
            if (item is String s)
            {
                if (s == " ")
                    return -1;

                item = new TaskDialogButtonDescriptionRun(s);
            }

            return base.AddInternal(item);
        }

        /// <summary>
        /// Returns a string that represents this <see cref="TaskDialogButtonDescriptionElementCollection"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> that represents the current instance.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (TaskDialogButtonDescriptionElement item in this)
            {
                sb.Append(item.ToString());
            }

            return sb.ToString();
        }

        #endregion
    }
}
