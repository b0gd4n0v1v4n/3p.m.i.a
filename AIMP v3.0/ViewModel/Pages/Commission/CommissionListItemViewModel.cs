using System.Windows;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.Model;
using System;
using AIMP_v3._0.PerfectListView;

namespace AIMP_v3._0.ViewModel.Pages.Commission
{
    public class CommissionListItemViewModel : Identity,IFilterRow
    {

        public string Date { get; set; }
        public string Number { get; set; }

        public string NumberProxy { get; set; }

        public string SellerFullName { get; set; }

        public string TrancportFullName { get; set; }

        public int? DocumentSellerId { get; set; }

        public int? PtsId { get; set; }
        public string Commission { get; set; }

        public string Parking { get; set; }
        public Visibility VisibilityOpenDocumentSeller => DocumentSellerId != null ? Visibility.Visible : Visibility.Hidden;


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

        public bool IsVisible { get; set; }
        public bool IsSelected { get; set; }
    }
}
