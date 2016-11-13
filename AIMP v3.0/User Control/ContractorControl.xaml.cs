using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using AIMP_v3._0.Interfaces;
using AIMP_v3._0.View;
using AIMP_v3._0.ViewModel;
using Entities;

namespace AIMP_v3._0.User_Control
{
    /// <summary>
    /// Interaction logic for ContractorControl.xaml
    /// </summary>
    public partial class ContractorControl : UserControl, INotifyPropertyChanged,IObjectSetValue
    {

        private static void _ContractorChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = source as ContractorControl;

            control.OnPropertyChanged("ContractorSignature");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static readonly DependencyProperty ContractorProperty =
            DependencyProperty.Register("Contractor", typeof(Contractor), typeof(ContractorControl),
                new UIPropertyMetadata(null, _ContractorChanged));

        public Contractor Contractor
        {
            get { return (Contractor)GetValue(ContractorProperty); }
            set
            {
                SetValue(ContractorProperty, value);
            }
        }
        public static readonly DependencyProperty ContractorSignatureProperty =
    DependencyProperty.Register("ContractorSignature", typeof(string), typeof(UserFileControl),
        new UIPropertyMetadata(string.Empty, _ContractorSignatureChanged));

        private static void _ContractorSignatureChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            (source as ContractorControl).OnPropertyChanged("ContractorSignature");
        }
        public string ContractorSignature
        {
            get
            {
                return Contractor != null
                    ? Contractor.LegalPerson != null ? $"ОРГАНИЗАЦИЯ: {Contractor.LegalPerson.Name}" : $"ФИО: {Contractor.LastName} {Contractor.FirstName} {Contractor.MiddleName}"
                    : string.Empty;
            }
            set
            {
                SetValue(ContractorSignatureProperty, value);
            }
        }

        public ContractorControl()
        {
            InitializeComponent();
        }

        private void Open_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Contractor == null)
                    Contractor = new Contractor()
                    {
                        City = new City(),
                        Region = new Region()
                    };

                if (Contractor.LegalPerson == null)
                {
                    var viewModel = new ContractorInfoViewModel(Contractor, this);

                    var view = new ContractorInfoView(viewModel);

                    view.ShowDialog();

                    OnPropertyChanged("ContractorSignature");
                }
                else
                {
                    var viewModel = new LegalPersonViewModel(Contractor, this);

                    var view = new LegalPersonInfoView(viewModel);

                    view.ShowDialog();

                    OnPropertyChanged("ContractorSignature");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Contractor = null;
            OnPropertyChanged("ContractorSignature");
        }

        private void Search_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchContractorViewModel searchContractorViewModel = new SearchContractorViewModel();

                SearchContractorView searchContractorView = new SearchContractorView(searchContractorViewModel);

                if (searchContractorView.ShowDialog() == true)
                    Contractor = searchContractorViewModel.CurrentContractor;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        public void SetValue(object obj)
        {
            SetValue(ContractorProperty, obj);
        }
    }
}
