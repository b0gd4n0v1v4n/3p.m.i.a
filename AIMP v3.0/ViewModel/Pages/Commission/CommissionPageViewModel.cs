using Aimp.Infrastructure;
using Aimp.Model.Entities;
using Aimp.ServiceContracts;
using AIMP_v3._0.Aimp.Services;
using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Logging;
using AIMP_v3._0.User_Control;
using AIMP_v3._0.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace AIMP_v3._0.ViewModel.Pages.Commission
{
    public class CommissionPageViewModel : BasePageViewModel<CommissionListItemViewModel>, IPageViewModel
    {
        private void _FillListCommission()
        {
            try
            {
                using (var service = ServiceClientProvider.GetCommissionTransaction())
                {
                    var response = service.GetCommissions();
                    
                    List =
                        new List<CommissionListItemViewModel>(
                            response.Select(x => new CommissionListItemViewModel()
                            {
                                Id = x.Id,
                                DocumentSellerId = x.DocumentSellerId,
                                PtsId = x.PtsId,
                                Date = x.Date.ToString(AimpDataFormats.DateFormat),
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
                    try
                    {
                        using (var service = ServiceClientProvider.GetCommissionTransaction())
                        {
                            using (var printeService = ServiceClientProvider.GetPrintedDocument())
                            {
                                var response = printeService.GetPrintedList(DocumentType.Commission);

                                var document = new CommissionTransaction();
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
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.Log("Неудалось создать документ", "New", ex);
                    }
                });
            }
        }
        
        public  Command Refresh
        {
            get
            {
                return new Command(x =>
                {
                    _FillListCommission();
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
                    try
                    {
                        if (CurrentItem != null)
                        {
                            CommissionViewModel vm;
                            using (var service = ServiceClientProvider.GetCommissionTransaction())
                            {
                                var transaction = service.GetCommission(CurrentItem.Id);
                                using (var printeService = ServiceClientProvider.GetPrintedDocument())
                                {
                                    var response = printeService.GetPrintedList(DocumentType.Commission);

                                    var lst = response.Select(p => new PrintItem()
                                    {
                                        Name = p.Name,
                                        Type = DocumentType.Commission,
                                        Document = transaction.Document
                                    });
                                    var sourcesTrancport = service.GetSourceTrancport();

                                    vm = new CommissionViewModel(transaction.Document, lst, sourcesTrancport);
                                }
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
