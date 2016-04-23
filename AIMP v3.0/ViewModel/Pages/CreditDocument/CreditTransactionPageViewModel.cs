using System;
using System.Collections.ObjectModel;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.DataAccess;
using System.Windows;
using System.Linq;
using Models.Documents;
using AIMP_v3._0.User_Control;
using AIMP_v3._0.View;
using AIMP_v3._0.Logging;

namespace AIMP_v3._0.ViewModel.Pages.CreditDocument
{
    public class CreditTransactionPageViewModel : BasePageViewModel<CreditTransactionListItemViewModel>, IPageViewModel
    {
        private void _FillListCreditTransaction()
        {
            try
            {
                using (AimpService service = new AimpService())
                {
                    var response = service.GetCreditTransactions();

                    if (response.Error)
                    {
                        MessageBox.Show(response.Message);
                        return;
                    }
                    var lst =
                        response.Items
                            .Select(x => new CreditTransactionListItemViewModel()
                            {
                                Id = x.Id,
                                DocumentBuyerId = x.DocumentBuyerId,
                                DocumentSellerId = x.DocumentSellerId,
                                PtsId = x.PtsId,
                                BuyerFullName = x.BuyerFullName,
                                Date = x.Date.ToString(Models.DataFormats.DateFormat),
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
        }

        public string Name { get { return "СДЕЛКА В КРЕДИТ"; } }
        
        public Command New {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        using (var service = new AimpService())
                        {
                            var response = service.GetPrintedList(DocumentType.CreditTransaction);
                            if (response.Error)
                                throw new Exception(response.Message);
                            var document = new CreditTransactionDocument();
                            var lst = response.List.Select(p => new PrintItem()
                            {
                                Name = p.Name,
                                Type = DocumentType.CreditTransaction,
                                Document = document
                            });
                            var responseInfo = service.GetCreditInfo();
                            if (responseInfo.Error)
                                throw new Exception(responseInfo.Message);
                            new CreditTransactionView(new CreditTransactionViewModel(document, lst, responseInfo.Creditors, responseInfo.Requisits)).ShowDialog();
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
        public override Command OpenListItem {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        if (CurrentItem != null)
                        {
                            CreditTransactionViewModel vm;
                            using (AimpService service = new AimpService())
                            {
                                var transaction = service.GetCreditTransaction(CurrentItem.Id);

                                if (transaction.Error)
                                {
                                    MessageBox.Show("Ошибка сервера", transaction.Message);
                                    return;
                                }
                                var response = service.GetPrintedList(DocumentType.CreditTransaction);
                                if (response.Error)
                                    throw new Exception(response.Message);
                                var lst = response.List.Select(p => new PrintItem()
                                {
                                    Name = p.Name,
                                    Type = DocumentType.CreditTransaction,
                                    Document = transaction.Document
                                });
                                var responseInfo = service.GetCreditInfo();
                                if (responseInfo.Error)
                                    throw new Exception(responseInfo.Message);
                                vm = new CreditTransactionViewModel(transaction.Document, lst, responseInfo.Creditors, responseInfo.Requisits);
                            }
                            CreditTransactionView CreditTransactionView = new CreditTransactionView(vm);
                            CreditTransactionView.ShowDialog();
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
