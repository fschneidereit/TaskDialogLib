using Flatcode.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Flatcode.Presentation
{
    public class TaskDialogButtonDescriptionLineBreak : TaskDialogButtonDescriptionElement
    {
        public static readonly DependencyProperty ParagraphProperty =
            DependencyProperty.Register(nameof(Paragraph), typeof(bool), typeof(TaskDialogButtonDescriptionLineBreak),
                                        new PropertyMetadata(null));

        public bool Paragraph
        {
            get { return (bool)GetValue(ParagraphProperty); }
            set { SetValue(ParagraphProperty, value); }
        }

        public override string ToString()
        {
            if (Paragraph)
                return "\n\n";

            return "\n";
        }
    }
}
