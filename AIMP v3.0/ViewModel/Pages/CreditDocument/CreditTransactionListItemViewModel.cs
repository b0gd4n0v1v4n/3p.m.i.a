using System.Windows;
using AIMP_v3._0.Helpers;
using Models.CreditTransact;

namespace AIMP_v3._0.ViewModel.Pages.CreditDocument
{
    public class CreditTransactionListItemViewModel : CreditTransactionListItem
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
                    OpenUserFile.GetAndOpen((int)AdId);
                });
            }
        }

        public Command OpenDocumentDkpCommand
        {
            get
            {
                return new Command(x =>
                {
                    OpenUserFile.GetAndOpen((int)DkpId);
                });
            }
        }

        public Command OpenDocumentPhotoBuyerCommand
        {
            get
            {
                return new Command(x =>
                {
                    OpenUserFile.GetAndOpen((int)PhotoBuyerId);
                });
            }
        }

        public Command OpenDocumentPhotoSellerCommand
        {
            get
            {
                return new Command(x =>
                {
                    OpenUserFile.GetAndOpen((int)PhotoSellerId);
                });
            }
        }

        public Command OpenDocumentSellerCommand
        {
            get
            {
                return new Command(x =>
                {
                    OpenUserFile.GetAndOpen((int)DocumentSellerId);
                });
            }
        }

        public Command OpenDocumentBuyerCommand
        {
            get
            {
                return new Command(x =>
                {
                    OpenUserFile.GetAndOpen((int)DocumentBuyerId);
                });
            }
        }

        public Command OpenPtsCommand
        {
            get
            {
                return new Command(x =>
                {
                    OpenUserFile.GetAndOpen((int)PtsId);
                });
            }
        }
    }
}
