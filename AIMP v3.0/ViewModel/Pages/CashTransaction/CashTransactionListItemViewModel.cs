using System.Windows;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.Model;
using System;
using AIMP_v3._0.PerfectListView;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AIMP_v3._0.ViewModel.Pages.CashTransaction
{
    public class CashTransactionListItemViewModel : Identity,IFilterRow,INotifyPropertyChanged
    {
        
        public string Date { get; set; }
        public string Number { get; set; }

        public string NumberProxy { get; set; }

        public string SellerFullName { get; set; }

        public string BuyerFullName { get; set; }

        public string TrancportFullName { get; set; }

        public int? DocumentSellerId { get; set; }

        public int? DocumentBuyerId { get; set; }

        public int? PtsId { get; set; }
        public Visibility VisibilityOpenDocumentSeller => DocumentSellerId != null ? Visibility.Visible : Visibility.Hidden;

        public Visibility VisibilityOpenDocumentBuyer => DocumentBuyerId != null ? Visibility.Visible : Visibility.Hidden;

        public Visibility VisibilityOpenPts => PtsId != null ? Visibility.Visible : Visibility.Hidden;

        public Command OpenDocumentSellerCommand
        {
            get
            {
                return new Command(x =>
                {
                    OpenFile(DocumentSellerId);
                });
            }
        }

        public Command OpenDocumentBuyerCommand
        {
            get
            {
                return new Command(x =>
                {
                    OpenFile(DocumentBuyerId);
                });
            }
        }

        public Command OpenPtsCommand
        {
            get
            {
                return new Command(x =>
                {
                    OpenFile(PtsId);
                });
            }
        }

        private void OpenFile(int? id)
        {
            LoadingViewHalper.ShowDialog("Открытие файла...", () =>
            {
            try
            {
                OpenUserFile.GetAndOpen((int)id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Не удалось открыть файл");
            }
            });
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
