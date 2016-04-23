using System.Windows.Input;

namespace AIMP_v3._0.User_Control
{
    public class CharOnlyTextBox : BaseTextBox
    {
        public CharOnlyTextBox()
        {
            KeyDown += (sender, e) =>
            {
                switch (e.Key)
                {
                    case Key.A:
                    case Key.B:
                    case Key.C:
                    case Key.D:
                    case Key.E:
                    case Key.F:
                    case Key.G:
                    case Key.H:
                    case Key.I:
                    case Key.J:
                    case Key.K:
                    case Key.L:
                    case Key.M:
                    case Key.N:
                    case Key.O:
                    case Key.P:
                    case Key.Q:
                    case Key.R:
                    case Key.S:
                    case Key.T:
                    case Key.U:
                    case Key.V:
                    case Key.W:
                    case Key.X:
                    case Key.Y:
                    case Key.Z:
                    case Key.Oem4:
                    case Key.Oem6:
                    case Key.Oem1:
                    case Key.Oem7:
                    case Key.OemComma:
                    case Key.OemPeriod:
                    case Key.RightAlt:
                    case Key.LeftAlt:
                    case Key.Left:
                    case Key.Right:
                    case Key.CapsLock:
                    case Key.LeftShift:
                    case Key.RightShift:
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
