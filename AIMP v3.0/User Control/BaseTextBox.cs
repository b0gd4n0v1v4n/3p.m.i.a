using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AIMP_v3._0.User_Control
{
    public abstract class BaseTextBox : TextBox
    {
        protected void _MoveToNextUIElement()
        {
            // Creating a FocusNavigationDirection object and setting it to a
            // local field that contains the direction selected.
            FocusNavigationDirection focusDirection = FocusNavigationDirection.Next;

            // MoveFocus takes a TraveralReqest as its argument.
            TraversalRequest request = new TraversalRequest(focusDirection);

            // Gets the element with keyboard focus.
            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

            // Change keyboard focus.
            if (elementWithFocus != null)
            {
                //if (elementWithFocus.MoveFocus(request)) e.Handled = true;
                elementWithFocus.MoveFocus(request);
            }
        }

        public BaseTextBox()
        {
            
        }
    }
}
