using Aimp.Infrastructure;
using AIMP_v3._0.Aimp.Services;
using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Logging;
using AIMP_v3._0.View;
using AIMP_v3._0.ViewModel.CardsTrancport;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace AIMP_v3._0.ViewModel.Pages.CardsTrancport
{
    public class CardsTrancportPageViewModel : BasePageViewModel<CardTrancportListItemViewModel>, IPageViewModel
    {
        private bool _isOneLoad;

        private void _FillListCards()
        {
            try
            {
                using (var service = ServiceClientProvider.GetTrancportCard())
                {
                    var response = service.GetCardsTrancport();

                    List = response.Items.Select(x=> new CardTrancportListItemViewModel()
                    {
                        Id = x.Id,
                        ColorTrancport = x.ColorTrancport,
                        DateSale = x.DateSale?.ToString(AimpDataFormats.DateFormat),
                        DateStart = x.DateStart.ToString(AimpDataFormats.DateFormat),
                        MakeModelTrancport = x.MakeModelTrancport,
                        Manager = x.Manager,
                        Number = x.Number,
                        NumberT = x.NumberT,
                        Price = x.Price,
                        Status = x.Status,
                        Source = x.Source,
                        User = x.User,
                        YearTrancport = x.YearTrancport
                    });
                    if (!_isOneLoad && response.StatusesCardForFilerStart != null && response.StatusesCardForFilerStart.Count() > 0)
                    {
                        _isOneLoad = true;
                        SetFilter("Status", response.StatusesCardForFilerStart);
                        KASTIL_BRASH_FOR_STATUS_CARDTRANCPORT = Brushes.Orange;
                        OnPropertyChanged("KASTIL_BRASH_FOR_STATUS_CARDTRANCPORT");
                    }
                    else
                    {
                        ClearFilteres();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Не удалось получить список а\м");
            }
        }
        public CardsTrancportPageViewModel()
        {
            _FillListCards();
        }
        public  string Name
        {
            get
            {
                return "СПИСОК А/М";
            }
        }

        public Command New
        {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        var model = new CardTrancportViewModel(null);
                        var view = new CardTrancportView(model);
                        view.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.Log("Неудалось создать документ", "New", ex);
                    }
                });
            }
        }

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
                            CardTrancportViewModel vm = new CardTrancportViewModel(CurrentItem.Id);
                            if (vm.CardTrancport.Commission)
                            {
                                CardCommissionTrancportView CreditTransactionView = new CardCommissionTrancportView(vm);
                                CreditTransactionView.ShowDialog();
                            }
                            else
                            {
CardTrancportView CreditTransactionView = new CardTrancportView(vm);
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
        }

        public override Command PrintList
        {
            get;
        }

        public Command Refresh
        {
            get
            {
                return new Command(x =>
                {
                    _FillListCards();
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
