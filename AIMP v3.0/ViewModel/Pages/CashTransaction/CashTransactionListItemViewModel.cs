using System.Windows;
using AIMP_v3._0.Helpers;
using Models.CashTransact;

namespace AIMP_v3._0.ViewModel.Pages.CashTransaction
{
    public class CashTransactionListItemViewModel : CashTransactionListItem
    {
        public Visibility VisibilityOpenDocumentSeller => DocumentSellerId != null ? Visibility.Visible : Visibility.Hidden;

        public Visibility VisibilityOpenDocumentBuyer => DocumentBuyerId != null ? Visibility.Visible : Visibility.Hidden;

        public Visibility VisibilityOpenPts => PtsId != null ? Visibility.Visible : Visibility.Hidden;

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
