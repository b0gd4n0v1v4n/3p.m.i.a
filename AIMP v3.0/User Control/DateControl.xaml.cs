using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using UserControl = System.Windows.Controls.UserControl;

namespace AIMP_v3._0.User_Control
{
    /// <summary>
    /// Interaction logic for DateControl.xaml
    /// </summary>
    public partial class DateControl : UserControl, INotifyPropertyChanged
    {
        private void _MoveToNextUIElement()
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

        private void _BackToElement()
        {
            // Creating a FocusNavigationDirection object and setting it to a
            // local field that contains the direction selected.
            FocusNavigationDirection focusDirection = FocusNavigationDirection.Previous;

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

        private string _GetNumTo01(string text)
        {
            if(!string.IsNullOrWhiteSpace(text))
                if (text.Length == 1)
                    return '0'+text;

            return text;
        }

        public DateControl()
        {
            InitializeComponent();
        }
        private void Day_OnKeyDown(object sender, KeyEventArgs e)
        {
            int maxDay = 0;

            if (Int32.TryParse(Day.Text, out maxDay))
            {
                if (maxDay > 31)
                {
                    e.Handled = true;
                }
            }
        }
        private void Day_OnKeyUp(object sender, KeyEventArgs e)
        {
            int length = Day.Text.Length;

            if (length == 2 && Day.SelectionLength != 2)
                    _MoveToNextUIElement();
        }
        private void Day_OnGotFocus(object sender, RoutedEventArgs e)
        {
            Day.SelectionStart = 0;
            Day.SelectionLength = Day.Text.Length;
        }
        private void Month_OnKeyDown(object sender, KeyEventArgs e)
        {
            int maxMount = 0;

            if (Int32.TryParse(Month.Text, out maxMount))
            {
                if (maxMount > 12)
                {
                    e.Handled = true;
                }
            }
        }
        private void Month_OnKeyUp(object sender, KeyEventArgs e)
        {
            int length = Month.Text.Length;

            if (length == 2 && Month.SelectionLength != 2)
                _MoveToNextUIElement();

            if (length == 0)
               _BackToElement();
        }
        private void Month_OnGotFocus(object sender, RoutedEventArgs e)
        {
                Month.SelectionStart = 0;
                Month.SelectionLength = Month.Text.Length;
        }
        private void Year_OnKeyUp(object sender, KeyEventArgs e)
        {
            int length = Year.Text.Length;

            if (length == 4 && Year.SelectionStart == 0 && Year.SelectionLength != 4)
            {
                e.Handled = true;
                _BackToElement();
            }

            if (length == 0)
                _BackToElement();
        }
        private void Year_OnGotFocus(object sender, RoutedEventArgs e)
        {
            Year.SelectionStart = 0;
            Year.SelectionLength = Year.Text.Length;
        }

        private static void _DateChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = source as DateControl;

            if (control.Date != DateTime.MinValue)
            {
                control.Day.Text = control._GetNumTo01(control.Date?.Day.ToString());

                control.Month.Text = control._GetNumTo01(control.Date?.Month.ToString());

                control.Year.Text = control.Date?.Year.ToString();
            }
            control.OnPropertyChanged("Date");
        }

        public DateTime? Date
        {
            get { return (DateTime?)GetValue(DateProperty); }
            set
            {
                SetValue(DateProperty, value);
            }
        }

        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register("Date", typeof(DateTime?), typeof(DateControl),
                new UIPropertyMetadata(null, _DateChanged));

        public int TabIndexControl
        {
            get { return (int)GetValue(TabIndexControlProperty); }
            set
            {
                SetValue(TabIndexControlProperty, value);
            }
        }

        public static readonly DependencyProperty TabIndexControlProperty =
    DependencyProperty.Register("TabIndexControl", typeof(int), typeof(DateControl),
        new UIPropertyMetadata(1, (o, args) =>
        {
            var control = o as DateControl;

            control.Day.TabIndex = control.TabIndexControl;

            control.Month.TabIndex = control.TabIndexControl;

            control.Year.TabIndex = control.TabIndexControl;
        }));

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Day_OnPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (Day.Text.Length != 2)
                e.Handled = true;

            int day = 0;

            if (Int32.TryParse(Day.Text, out day))
            {
                if (day == 0 || day > 31)
                    e.Handled = true;
                else
                {
                    int year = 0;

                    if (Int32.TryParse(Year.Text, out year))
                    {
                        int month = 0;

                        if (Int32.TryParse(Month.Text, out month))
                        {
                            if (DateTime.DaysInMonth(year, month) < day)
                            {
                                {
                                    Day.Background = Brushes.Red;
                                    SetValue(DateProperty,DateTime.MinValue);
                                    e.Handled = true;
                                }
                            }
                            else
                            {
                                Day.Background = Brushes.White;
                                SetValue(DateProperty, new DateTime(year,month,day));
                            }
                        }
                    }
                }
            }
            else
                e.Handled = true;
        }

        private void Month_OnPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (_monthKeyDownBack)
            {
                _monthKeyDownBack = false;
                return;
            }

            if (Month.Text.Length != 2)
                e.Handled = true;

            int month = 0;

            if (Int32.TryParse(Month.Text, out month))
            {
                if (month == 0 || month > 12)
                    e.Handled = true;
                else
                {
                    int year = 0;

                    if (Int32.TryParse(Year.Text, out year))
                    {
                        int day = 0;

                        if (Int32.TryParse(Day.Text, out day))
                        {
                            if (DateTime.DaysInMonth(year, month) < day)
                            {
                                {
                                    Day.Background = Brushes.Red;
                                    SetValue(DateProperty, DateTime.MinValue);
                                }
                            }
                            else
                            {
                                Day.Background = Brushes.White;
                                SetValue(DateProperty, new DateTime(year, month, day));
                            }
                        }
                    }
                }
            }
            else
                e.Handled = true;
        }

        private void Year_OnPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (_yearKeyDownBack)
            {
                _yearKeyDownBack = false;
                return;
            }

            if (Year.Text.Length != 4)
                e.Handled = true;

            int year = 0;

            if (Int32.TryParse(Year.Text, out year))
            {
                if (year == 0 || year > DateTime.MaxValue.Year || year < DateTime.MinValue.Year)
                    e.Handled = true;
                else
                {
                    if (Month.Text.Length != 2)
                    {
                        Day.Background = Brushes.Red;
                        SetValue(DateProperty, DateTime.MinValue);
                    }

                    int month = 0;

                    if (Int32.TryParse(Month.Text, out month))
                    {
                        if (month == 0 || month > 12)
                        {
                            Day.Background = Brushes.Red;
                            SetValue(DateProperty, DateTime.MinValue);
                        }
                        else
                        {
                            if (Day.Text.Length != 2)
                            {
                                Day.Background = Brushes.Red;
                                SetValue(DateProperty, DateTime.MinValue);
                            }
                            else
                            {
                                int day = 0;

                                if (Int32.TryParse(Day.Text, out day))
                                {
                                    if (day == 0 || day > 31)
                                    {
                                        Day.Background = Brushes.Red;
                                        SetValue(DateProperty, DateTime.MinValue);
                                    }
                                    else
                                    {
                                        if (DateTime.DaysInMonth(year, month) < day)
                                        {
                                            {
                                                Day.Background = Brushes.Red;
                                                SetValue(DateProperty,DateTime.MinValue);
                                            }
                                        }
                                        else
                                        {
                                            Day.Background = Brushes.White;
                                            SetValue(DateProperty, new DateTime(year, month, day));
                                        }
                                    }
                                }
                                else
                                {
                                    Day.Background = Brushes.Red;
                                    SetValue(DateProperty, DateTime.MinValue);
                                }
                            }
                        }
                    }
                    else
                        e.Handled = true;
                }
            }
            else
                e.Handled = true;
        }

        private bool _yearKeyDownBack;

        private bool _monthKeyDownBack;

        private void Year_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
                _yearKeyDownBack = true;
            else
                _yearKeyDownBack = false;
        }

        private void Month_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
                _monthKeyDownBack = true;
            else
                _monthKeyDownBack = false;
        }

        private void Month_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(Day.Text))
                Day.Text = "01";
        }

        private void Year_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(Day.Text))
                Day.Text = "01";
            if (string.IsNullOrEmpty(Month.Text))
                Month.Text = "01";
        }
    }
}
