using System.Linq;
using Aimp.Model.Documents;
using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.PrintControl;
using AIMP_v3._0.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace AIMP_v3._0.ViewModel.Pages.Commission
{
    public class CommissionPageViewModel : BasePageViewModel<CommissionListItemViewModel>, IPageViewModel
    {
        public override Visibility AddButtonVisible
        {
            get
            {
                return Visibility.Visible;
            }
        }
        private void _FillListCommission()
        {
            try
            {
                using (AimpService service = new AimpService())
                {
                    var response = service.GetCommissions();
                    
                    List =
                        new List<CommissionListItemViewModel>(
                            response
                            .OrderByDescending(x=>x.Date)
                            .ThenByDescending(x=>x.Number)
                            .Select(x => new CommissionListItemViewModel()
                            {
                                IsVisible = true,
                                Id = x.Id,
                                DocumentSellerId = x.DocumentSellerId,
                                PtsId = x.PtsId,
                                Date = x.Date,
                                TrancportFullName = x.TrancportFullName,
                                Number = x.Number,
                                NumberProxy = x.NumberProxy,
                                SellerFullName = x.SellerFullName,
                                Commission = x.Commission,
                                Parking = x.Parking
                            }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Не удалось получить список");
            }
        }

        public string Name { get { return "ДОГОВОРА КОМИССИИ"; } }

        public ObservableCollection<CommissionListItemViewModel> ListCommission { get; set; }

        public CommissionPageViewModel()
        {
            _FillListCommission();
        }

        public Command New
        {
            get
            {
                return new Command(x =>
                {
                    LoadingViewHalper.ShowDialog("Загрузка...", () =>
                    {
                        try
                        {
                            using (var service = new AimpService())
                            {
                                var response = service.GetPrintedList(DocumentType.Commission);

                                var document = new CommissionDocument();
                                var lst = response.Select(p => new PrintItem()
                                {
                                    Name = p.Name,
                                    Type = DocumentType.Commission,
                                    Document = document
                                });
                                var sourcesTrancport = service.GetSourceTrancport();

                                new CommissionView(new CommissionViewModel(document, lst, sourcesTrancport)).ShowDialog();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Неудалось создать документ");
                        }
                    });
                });
            }
        }
        
        public  Command Refresh
        {
            get
            {
                return new Command(x =>
                {
                    LoadingViewHalper.ShowDialog("Загрузка...", () =>
                    {
                        _FillListCommission();
                    });
                });
            }
        }
        
        public override Command PrintList { get; }

        public override Command OpenListItem
        {
            get
            {
                return new Command(x =>
                {
                    LoadingViewHalper.ShowDialog("Загрузка...", () =>
                    {
                        try
                        {
                            if (CurrentItem != null)
                            {
                                CommissionViewModel vm;
                                using (AimpService service = new AimpService())
                                {
                                    var transaction = service.GetCommission(CurrentItem.Id);

                                    var response = service.GetPrintedList(DocumentType.Commission);

                                    var lst = response.Select(p => new PrintItem()
                                    {
                                        Name = p.Name,
                                        Type = DocumentType.Commission,
                                        Document = transaction.Document
                                    });
                                    var sourcesTrancport = service.GetSourceTrancport();

                                    vm = new CommissionViewModel(transaction.Document, lst, sourcesTrancport);
                                }
                                CommissionView CommissionView = new CommissionView(vm);
                                CommissionView.ShowDialog();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Не удалось открыть сделку");
                        }
                    });
                });
            }
        }

        public Visibility PrintButtonVisible
        {
            get
            {
                return Visibility.Hidden;
            }
        }
    }
}
