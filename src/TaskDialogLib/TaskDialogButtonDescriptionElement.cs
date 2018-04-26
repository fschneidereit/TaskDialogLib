using System;
using System.Windows;
using System.Windows.Markup;

namespace Flatcode.Presentation
{
    /// <summary>
    /// Represents the base class for all task dialog button description elements.
    /// </summary>
    [ContentProperty("Text")]
    public abstract class TaskDialogButtonDescriptionElement : TaskDialogElement
    {
        #region Fields: Dependency Properties

        /// <summary>
        /// Identifies the <see cref="Text"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(String), typeof(TaskDialogButtonDescriptionElement),
                                        new PropertyMetadata(null));

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TaskDialogButtonDescriptionElement class.
        /// </summary>
        protected TaskDialogButtonDescriptionElement()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text of this <see cref="TaskDialogButtonDescriptionElement"/>.
        /// </summary>
        public String Text
        {
            get { return GetValue(TextProperty) as String; }
            set { SetValue(TextProperty, value); }
        }

        #endregion

        #region Methods: Overridden

        /// <summary>
        /// Returns a string that represents this <see cref="TaskDialogButtonDescriptionElement"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> that represents the current instance.</returns>
        public override String ToString()
        {
            return Text;
        }

        #endregion
    }
}