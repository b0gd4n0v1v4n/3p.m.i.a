using AIMP_v3._0.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AIMP_v3._0.View
{
    /// <summary>
    /// Логика взаимодействия для AuthView.xaml
    /// </summary>
    public partial class AuthView : Window
    {
        AuthViewModel model;
        public AuthView()
        {
            InitializeComponent();
            model = new AuthViewModel();
            passwordBox.Password = model.Password;
            DataContext = model;
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            model.Password = passwordBox.Password;
        }
    }
}
