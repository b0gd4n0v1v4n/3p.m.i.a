using System.Windows;
using AIMP_v3._0.Helpers;
using Models.CashTransact;
using Models.Commission;

namespace AIMP_v3._0.ViewModel.Pages.Commission
{
    public class CommissionListItemViewModel : CommissionListItem
    {
        public Visibility VisibilityOpenDocumentSeller => DocumentSellerId != null ? Visibility.Visible : Visibility.Hidden;


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
