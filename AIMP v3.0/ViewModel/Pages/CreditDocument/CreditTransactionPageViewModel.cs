using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using AIMP_v3._0.User_Control;
using AIMP_v3._0.View;
using AIMP_v3._0.Logging;
using AIMP_v3._0.Aimp.Services;
using Aimp.Infrastructure;
using Aimp.ServiceContracts;
using Aimp.Model.Entities;

namespace AIMP_v3._0.ViewModel.Pages.CreditDocument
{
    public class CreditTransactionPageViewModel : BasePageViewModel<CreditTransactionListItemViewModel>, IPageViewModel
    {
        private void _FillListCreditTransaction()
        {
            try
            {
                using (var service = ServiceClientProvider.GetCreditTransaction())
                {
                    var response = service.GetCreditTransactions();

                    var lst =
                        response
                            .Select(x => new CreditTransactionListItemViewModel()
                            {
                                Id = x.Id,
                                DocumentBuyerId = x.DocumentBuyerId,
                                DocumentSellerId = x.DocumentSellerId,
                                PtsId = x.PtsId,
                                BuyerFullName = x.BuyerFullName,
                                Date = x.Date.ToString(AimpDataFormats.DateFormat),
                                TrancportFullName = x.TrancportFullName,
                                Number = x.Number,
                                NumberProxy = x.NumberProxy,
                                SellerFullName = x.SellerFullName,
                                PhotoSellerId = x.PhotoSellerId,
                                PhotoBuyerId = x.PhotoBuyerId,
                                DkpId = x.DkpId,
                                AdId = x.AdId
                            });

                    List = new ObservableCollection<CreditTransactionListItemViewModel>(lst);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Не удалось получить список");
            }
        }

        public CreditTransactionPageViewModel()
        {
            _FillListCreditTransaction();
            _openListItem = new Command(x =>
                {
                    try
                    {
                        if (CurrentItem != null)
                        {
                            CreditTransactionViewModel vm;
                            using (var service = ServiceClientProvider.GetCreditTransaction())
                            {
                                var transaction = service.GetCreditTransaction(CurrentItem.Id);
                                using (var printeService = ServiceClientProvider.GetPrintedDocument())
                                {
                                    var response = printeService.GetPrintedList(DocumentType.CreditTransaction);

                                    var lst = response.Select(p => new PrintItem()
                                    {
                                        Name = p.Name,
                                        Type = DocumentType.CreditTransaction,
                                        Document = transaction
                                    });
                                    var responseInfo = service.GetCreditTransactionInfo();
                                    vm = new CreditTransactionViewModel(transaction, lst, responseInfo.Creditors, responseInfo.Requisits);
                                }
                                CreditTransactionView CreditTransactionView = new CreditTransactionView(vm);
                                CreditTransactionView.ShowDialog();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Не удалось открыть сделку");
                    }
                });
        }

        public string Name { get { return "СДЕЛКА В КРЕДИТ"; } }
        
        public Command New {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        using (var service = ServiceClientProvider.GetCreditTransaction())
                        {
                            using (var printeService = ServiceClientProvider.GetPrintedDocument())
                            {
                                var response = printeService.GetPrintedList(DocumentType.CreditTransaction);

                                var document = new CreditTransaction();
                                var lst = response.Select(p => new PrintItem()
                                {
                                    Name = p.Name,
                                    Type = DocumentType.CreditTransaction,
                                    Document = document
                                });
                                var responseInfo = service.GetCreditTransactionInfo();

                                new CreditTransactionView(new CreditTransactionViewModel(document, lst, responseInfo.Creditors, responseInfo.Requisits)).ShowDialog();
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
        
        public Command Refresh
        {
            get
            {
                return new Command(x =>
                {
                    _FillListCreditTransaction();
                });
            }
        }
        
        public override Command PrintList { get; }
        private Command _openListItem; 
        public override Command OpenListItem { get { return _openListItem; } }
       
        public Visibility PrintButtonVisible
        {
            get
            {
                return Visibility.Hidden;
            }
        }
    }
}
