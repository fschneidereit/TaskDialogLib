using System;
using System.Windows;
using System.Windows.Markup;

namespace Flatcode.Presentation
{
    /// <summary>
    /// Represents formatted content of a <see cref="TaskDialogButton.Description"/>.
    /// </summary>
    [ContentProperty("Contents")]
    public sealed class TaskDialogButtonDescription : DependencyObject
    {
        #region Fields

        readonly TaskDialogButtonDescriptionElementCollection contents;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TaskDialogButtonDescription class.
        /// </summary>
        public TaskDialogButtonDescription()
        {
            contents = new TaskDialogButtonDescriptionElementCollection();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a <see cref="TaskDialogButtonDescriptionElementCollection"/> that represents the contents of
        /// this <see cref="TaskDialogButtonDescription"/>.
        /// </summary>
        public TaskDialogButtonDescriptionElementCollection Contents
        {
            get { return contents; }
        }

        #endregion

        #region Methods: Overridden

        /// <summary>
        /// Returns a string that represents this <see cref="TaskDialogButtonDescription"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> that represent the current instance.</returns>
        public override String ToString()
        {
            return Contents.ToString();
        }

        #endregion
    }
}
