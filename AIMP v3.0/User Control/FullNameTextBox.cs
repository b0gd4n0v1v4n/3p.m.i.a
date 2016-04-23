using System.Windows.Controls;

namespace AIMP_v3._0.User_Control
{
    public class FullNameTextBox : CharOnlyTextBox
    {
        public FullNameTextBox()
        {
            KeyUp += (sender, args) =>
            {
                if (Text.Length == 1)
                {
                    Text = Text.ToUpper();
                    SelectionStart = 1;
                }
            };
        }
    }
}
