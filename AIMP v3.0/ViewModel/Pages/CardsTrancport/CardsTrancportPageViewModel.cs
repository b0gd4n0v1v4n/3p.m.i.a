using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Helpers;
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
        private void _FillListCards()
        {
            try
            {
                using (AimpService service = new AimpService())
                {
                    var response = service.GetCardsTrancport();

                    List = response.Items
                        .OrderByDescending(x => x.DateStart)
                        .ThenByDescending(x=>x.Number)
                        .Select(x=> new CardTrancportListItemViewModel()
                    {
                            IsVisible = true,
                            Id = x.Id,
                        ColorTrancport = x.ColorTrancport,
                        DateSale = x.DateSale.HasValue ? x.DateSale.Value : DateTime.MinValue,
                        DateStart = x.DateStart,
                        MakeModelTrancport = x.MakeModelTrancport,
                        Manager = x.Manager,
                        Number = x.Number,
                        NumberT = x.NumberT.HasValue ? x.NumberT.Value : 0,
                        Price = x.Price,
                        Status = x.Status,
                        Source = x.Source,
                        User = x.User,
                        YearTrancport = x.YearTrancport
                    }).ToList();
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
                return new Command(x => LoadingViewHalper.ShowDialog("Загрузка...", () =>
                {
                    try
                    {
                        var model = new CardTrancportViewModel(null);
                        var view = new CardTrancportView(model);
                        view.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,"Неудалось создать документ");
                    }
                }));
            }
        }

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
                                CardTrancportViewModel vm = new CardTrancportViewModel(CurrentItem.Id);
                                if (vm.CardTrancport.Commission)
                                {
                                    CardCommissionTrancportView CreditTransactionView = new CardCommissionTrancportView(vm);
                                    CreditTransactionView.Show();
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
                    LoadingViewHalper.ShowDialog("Загрузка...", () =>
                    {
                        _FillListCards();
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

        public override Visibility AddButtonVisible
        {
            get
            {
                return Visibility.Hidden;
            }
        }
    }
}
