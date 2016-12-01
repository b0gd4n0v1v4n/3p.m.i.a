using System;
using System.Collections.ObjectModel;
using System.Linq;
using Aimp.Model.Documents;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.DataAccess;
using System.Windows;
using AIMP_v3._0.PrintControl;
using AIMP_v3._0.View;
using System.Collections;
using System.Collections.Generic;
using Nelibur.ObjectMapper;

namespace AIMP_v3._0.ViewModel.Pages.CreditDocument
{
    public class CreditTransactionPageViewModel : BasePageViewModel<CreditTransactionListItemViewModel>, IPageViewModel
    {
        public override Visibility AddButtonVisible
        {
            get
            {
                return Visibility.Visible;
            }
        }
        private void _FillListCreditTransaction()
        {
            LoadingViewHalper.ShowDialog("Загрузка...", () =>
            {
                try
            {
                using (AimpService service = new AimpService())
                {
                    var response = service.GetCreditTransactions();
                    var lst = response
                            .OrderByDescending(x => x.Date)
                            .ThenByDescending(x => x.Number)
                            .Select(x => new CreditTransactionListItemViewModel()
                             {
                                IsVisible = true,
                                Date = x.Date,
                                 Id = x.Id,
                                 DocumentBuyerId = x.DocumentBuyerId,
                                 DocumentSellerId = x.DocumentSellerId,
                                 PtsId = x.PtsId,
                                 BuyerFullName = x.BuyerFullName,
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
            });
        }

        public CreditTransactionPageViewModel()
        {
            OnSelected += () =>
            {
                if (IsOneSelected)
                {
                    IsOneSelected = false;
                    _FillListCreditTransaction();
                }
            };
            _openListItem = new Command(x =>
                {
                    LoadingViewHalper.ShowDialog("Загрузка...", () =>
                    {
                        try
                        {
                            if (CurrentItem != null)
                            {
                                CreditTransactionViewModel vm;
                                using (AimpService service = new AimpService())
                                {
                                    var transaction = service.GetCreditTransaction(CurrentItem.Id);

                                    var response = service.GetPrintedList(DocumentType.CreditTransaction);

                                    var lst = response.Select(p => new PrintItem()
                                    {
                                        Name = p.Name,
                                        Type = DocumentType.CreditTransaction,
                                        Document = transaction
                                    });
                                    var responseInfo = service.GetCreditInfo();

                                    vm = new CreditTransactionViewModel(transaction, lst, responseInfo.Creditors, responseInfo.Requisits);
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
                });
        }

        public string Name { get { return "СДЕЛКА В КРЕДИТ"; } }
        
        public Command New {
            get
            {
                return new Command(x => LoadingViewHalper.ShowDialog("Загрузка...", () =>
                {
                    try
                    {
                        using (var service = new AimpService())
                        {
                            var response = service.GetPrintedList(DocumentType.CreditTransaction);

                            var document = new CreditTransactionDocument();
                            var lst = response.Select(p => new PrintItem()
                            {
                                Name = p.Name,
                                Type = DocumentType.CreditTransaction,
                                Document = document
                            });
                            var responseInfo = service.GetCreditInfo();

                            new CreditTransactionView(new CreditTransactionViewModel(document, lst, responseInfo.Creditors, responseInfo.Requisits)).ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Неудалось создать документ");
                    }
                }));
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
