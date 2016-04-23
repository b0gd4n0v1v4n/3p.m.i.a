using System.Windows.Input;

namespace AIMP_v3._0.User_Control
{
    public class DoubleTextBox : BaseTextBox
    {
        public DoubleTextBox()
        {
            KeyDown += (sender, e) =>
            {
                switch (e.Key)
                {
                    case Key.D0:
                    case Key.D1:
                    case Key.D2:
                    case Key.D3:
                    case Key.D4:
                    case Key.D5:
                    case Key.D6:
                    case Key.D7:
                    case Key.D8:
                    case Key.D9:
                    case Key.NumLock:
                    case Key.NumPad0:
                    case Key.NumPad1:
                    case Key.NumPad2:
                    case Key.NumPad3:
                    case Key.NumPad4:
                    case Key.NumPad5:
                    case Key.NumPad6:
                    case Key.NumPad7:
                    case Key.NumPad8:
                    case Key.NumPad9:
                    case Key.OemComma:
                    case Key.OemPeriod:
                    case Key.Delete:
                    case Key.Back:
                        break;
                    case Key.Enter:
                    case Key.Tab:
                        _MoveToNextUIElement();
                        break;
                    default:
                        e.Handled = true;
                        break;
                }
            };
        }
    }
}
