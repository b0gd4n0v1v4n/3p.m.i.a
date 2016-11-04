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
using AIMP_v3._0.ViewModel;

namespace AIMP_v3._0.View
{
    /// <summary>
    /// Логика взаимодействия для CommissionView.xaml
    /// </summary>
    public partial class CommissionView : Window
    {
        public CommissionView(CommissionViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}
