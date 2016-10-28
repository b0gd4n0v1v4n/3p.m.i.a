using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Helpers;
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

                    if (response.Error)
                    {
                        MessageBox.Show(response.Message);
                        return;
                    }

                    List =
                        new List<CommissionListItemViewModel>(
                            response.Items.Select(x => new CommissionListItemViewModel()
                            {
                                Id = x.Id,
                                DocumentSellerId = x.DocumentSellerId,
                                PtsId = x.PtsId,
                                Date = x.Date.ToString(Models.DataFormats.DateFormat),
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
                                if (response.Error)
                                    throw new Exception(response.Message);
                                var document = new CommissionDocument();
                                var lst = response.List.Select(p => new PrintItem()
                                {
                                    Name = p.Name,
                                    Type = DocumentType.Commission,
                                    Document = document
                                });
                                var sourcesTrancport = service.GetSourceTrancport();
                                if (sourcesTrancport.Error)
                                    throw new Exception(response.Message);
                                new CommissionView(new CommissionViewModel(document, lst, sourcesTrancport.Items)).ShowDialog();
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.Log("Неудалось создать документ", "New", ex);
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

                                    if (transaction.Error)
                                    {
                                        MessageBox.Show("Ошибка сервера", transaction.Message);
                                        return;
                                    }
                                    var response = service.GetPrintedList(DocumentType.Commission);
                                    if (response.Error)
                                        throw new Exception(response.Message);
                                    var lst = response.List.Select(p => new PrintItem()
                                    {
                                        Name = p.Name,
                                        Type = DocumentType.Commission,
                                        Document = transaction.Document
                                    });
                                    var sourcesTrancport = service.GetSourceTrancport();
                                    if (sourcesTrancport.Error)
                                        throw new Exception(response.Message);
                                    vm = new CommissionViewModel(transaction.Document, lst, sourcesTrancport.Items);
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
