using System;

namespace Flatcode.Presentation
{
    /// <summary>
    /// Represents an inline run on a <see cref="TaskDialogButton.Description"/>.
    /// </summary>
    public class TaskDialogButtonDescriptionRun : TaskDialogButtonDescriptionElement
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TaskDialogButtonDescriptionRun class.
        /// </summary>
        public TaskDialogButtonDescriptionRun()
        {
        }

        /// <summary>
        /// Initializes a new instance of the TaskDialogButtonDescriptionRun with the given text.
        /// </summary>
        /// <param name="text">A <see cref="String"/> that represents the text of the run.</param>
        public TaskDialogButtonDescriptionRun(String text)
        {
            // Initialize instance
            Text = text;
        }

        #endregion
    }
}
