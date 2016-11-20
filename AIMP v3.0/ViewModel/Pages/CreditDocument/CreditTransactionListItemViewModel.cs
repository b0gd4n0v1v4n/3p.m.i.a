using System.Windows;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.Model;
using System;
using Aimp.Model.CreditTransact;
using AIMP_v3._0.PerfectListView;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AIMP_v3._0.ViewModel.Pages.CreditDocument
{
    public class CreditTransactionListItemViewModel : CreditTransactionListItem,IFilterRow,INotifyPropertyChanged
    {
        public Visibility VisibilityOpenDocumentSeller => DocumentSellerId != null ? Visibility.Visible : Visibility.Hidden;

        public Visibility VisibilityOpenDocumentBuyer => DocumentBuyerId != null ? Visibility.Visible : Visibility.Hidden;

        public Visibility VisibilityOpenPts => PtsId != null ? Visibility.Visible : Visibility.Hidden;

        public Visibility VisibilityOpenDkp => DkpId != null ? Visibility.Visible : Visibility.Hidden;

        public Visibility VisibilityOpenAd => AdId != null ? Visibility.Visible : Visibility.Hidden;

        public Visibility VisibilityOpenPhotoSeller => PhotoSellerId != null ? Visibility.Visible : Visibility.Hidden;

        public Visibility VisibilityOpenPhotoBuyer => PhotoBuyerId != null ? Visibility.Visible : Visibility.Hidden;

        public Command OpenDocumentAdCommand
        {
            get
            {
                return new Command(x =>
                {
                    OpenFile(AdId);
                });
            }
        }

        public Command OpenDocumentDkpCommand
        {
            get
            {
                return new Command(x =>
                {
                    OpenFile(DkpId);
                });
            }
        }

        public Command OpenDocumentPhotoBuyerCommand
        {
            get
            {
                return new Command(x =>
                {
                    OpenFile(PhotoBuyerId);
                });
            }
        }

        public Command OpenDocumentPhotoSellerCommand
        {
            get
            {
                return new Command(x =>
                {
                    OpenFile(PhotoSellerId);
                });
            }
        }

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
                    OpenFile((int)PtsId);
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
