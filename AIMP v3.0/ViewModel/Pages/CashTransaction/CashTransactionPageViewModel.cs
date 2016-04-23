using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Logging;
using AIMP_v3._0.User_Control;
using AIMP_v3._0.View;
using Models.Documents;
using Models.PrintedDocument.Templates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace AIMP_v3._0.ViewModel.Pages.CashTransaction
{
    public class CashTransactionPageViewModel : BasePageViewModel<CashTransactionListItemViewModel>, IPageViewModel
    {
        private void _FillListCashTransaction()
        {
            try
            {
                using (AimpService service = new AimpService())
                {
                    var response = service.GetCashTransactions();

                    if (response.Error)
                    {
                        MessageBox.Show(response.Message);
                        return;
                    }

                    List =
                        new List<CashTransactionListItemViewModel>(
                            response.Items.Select(x => new CashTransactionListItemViewModel()
                            {
                                Id = x.Id,
                                DocumentBuyerId = x.DocumentBuyerId,
                                DocumentSellerId = x.DocumentSellerId,
                                PtsId = x.PtsId,
                                BuyerFullName = x.BuyerFullName,
                                Date = x.Date,
                                TrancportFullName = x.TrancportFullName,
                                Number = x.Number,
                                NumberProxy = x.NumberProxy,
                                SellerFullName = x.SellerFullName
                            }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Не удалось получить список");
            }
        }

        public string Name { get { return "СДЕЛКА ЗА НАЛИЧНЫЙ РАСЧЕТ"; } }

        public ObservableCollection<CashTransactionListItemViewModel> ListCashTransaction { get; set; }

        public CashTransactionPageViewModel()
        {
            _FillListCashTransaction();
        }

        public Command New
        {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        using(var service = new AimpService())
                        {
                            var response = service.GetPrintedList(DocumentType.CashTransaction);
                            if (response.Error)
                                throw new Exception(response.Message);
                            var document = new CashTransactionDocument();
                            var lst = response.List.Select(p => new PrintItem()
                            {
                                Name = p.Name,
                                Type = DocumentType.CashTransaction,
                                Document = document
                            });
                            new CashTransactionView(new CashTransactionViewModel(document,lst)).ShowDialog();
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
                    _FillListCashTransaction();
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
                            CashTransactionViewModel vm;
                            using (AimpService service = new AimpService())
                            {
                                var transaction = service.GetCashTransaction(CurrentItem.Id);

                                if (transaction.Error)
                                {
                                    MessageBox.Show("Ошибка сервера", transaction.Message);
                                    return;
                                }
                                    var response = service.GetPrintedList(DocumentType.CashTransaction);
                                    if (response.Error)
                                        throw new Exception(response.Message);
                                    var lst = response.List.Select(p => new PrintItem()
                                    {
                                        Name = p.Name,
                                        Type = DocumentType.CashTransaction,
                                        Document = transaction.Document
                                    });
                                 vm = new CashTransactionViewModel(transaction.Document,lst);
                            }
                            CashTransactionView cashTransactionView = new CashTransactionView(vm);
                            cashTransactionView.ShowDialog();
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
