using System.Windows;
using AIMP_v3._0.Helpers;
using Models.CashTransact;
using AIMP_v3._0.Model;

namespace AIMP_v3._0.ViewModel.Pages.CashTransaction
{
    public class CashTransactionListItemViewModel : Identity
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
