using Aimp.Model.Documents;
using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Extensions;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.Logging;
using AIMP_v3._0.View;
using Entities;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AIMP_v3._0.ViewModel.CardsTrancport
{
    public class CardTrancportViewModel : BaseViewModel
    {
        private CardTrancport _cardTrancportForCompare;
        private CardTrancportDocument _document;
        public CardTrancportViewModel(int? id)
        {
            using(var service = new AimpService())
            {
                if (id != null)
                {
                    var response = service.GetCardTrancport((int)id);

                    _document = response;
                    CardTrancport = response.CardTrancport;
                    _cardTrancportForCompare = TinyMapper.Map<CardTrancport>(CardTrancport);
                    var preChecks = response.PreChecks.Select(x => new PreCheckCardTrancportViewModel()
                    {
                        CardTrancportId = x.Id,
                        Date = x.Date,
                        Enable = true,
                        Name = x.Name,
                        Paid = x.Paid,
                        Card = x.Card,
                        PriceForClient = x.PriceForClient,
                        Summ = x.Summ
                    });
                    PreCheks = new ObservableCollection<PreCheckCardTrancportViewModel>(preChecks);
                }
                else
                {
                    _document = new CardTrancportDocument();
                    CardTrancport = new CardTrancport();
                    _cardTrancportForCompare = new CardTrancport();
                    PreCheks = new ObservableCollection<PreCheckCardTrancportViewModel>();
                }
                var responseStatuses = service.GetStatusesCard();

                CardTrancport.StatusCardTrancport = responseStatuses.FirstOrDefault(x => x.Id == CardTrancport.StatusCardTrancport?.Id);
                StatusesCardTrancport = new ObservableCollection<StatusCardTrancport>(responseStatuses);
            }
        }
        public CardTrancport CardTrancport { get; set; }
        public string SellerFullName { get
            {
                var user =
                CardTrancport?.CommissionTransaction?.Seller;
                return $"{user?.LastName} {user?.FirstName}. {user?.MiddleName}";
                    }
            set { }
        }
        public string TrancportFullName
        {
            get
            {
                var user =
                CardTrancport?.CommissionTransaction?.Trancport;
                return $"{user?.Make?.Name}, {user?.Model?.Name}";
            }
            set
            {

            }
        }
        public ObservableCollection<PreCheckCardTrancportViewModel> PreCheks { get; set; }
        public ObservableCollection<StatusCardTrancport> StatusesCardTrancport { get; set; }
        public Command SaveChangesCommand
        {
            get
            {
                return new Command(x =>
                {
                    LoadingViewHalper.ShowDialog("Сохранение...", () =>
                    {
                        try
                        {
                            using (var service = new AimpService())
                            {
                                var preChecs = PreCheks.Where(y => y.Enable).Select(y => new PreCheckCardTrancport()
                                {
                                    Id = y.CardTrancportId,
                                    Card = y.Card,
                                    Date = y.Date,
                                    Name = y.Name,
                                    Paid = y.Paid,
                                    Summ = y.Summ,
                                    CardTrancport = CardTrancport,
                                    PriceForClient = y.PriceForClient
                                });
                                _document.PreChecks = preChecs;
                                var response = service.SaveCardTrancport(_document);

                                CardTrancport.Id = response;
                                _document.CardTrancport.Id = response;
                            }
                            _cardTrancportForCompare = TinyMapper.Map<CardTrancport>(CardTrancport);
                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.Log("Не удалось сохранить", "SaveChangesCommand", ex);
                        }
                    });
                });
            }
        }
        public Command CloseCommand
        {
            get
            {
                return new Command((win) =>
                {
                    var window = (win as Window);

                    if (!_cardTrancportForCompare.CompareProperties(CardTrancport))
                    {
                        if (new QuestClosingView("Закрыть без сохранения?").ShowDialog() == true)
                        {
                            if (window != null)
                            {
                                window.Close();
                            }
                        }
                    }
                    else
                    {
                        if (window != null)
                        {
                            window.Close();
                        }
                    }
                });
            }
        }
        public PreCheckCardTrancportViewModel CurrentPreCheck { get; set; }
        public Command SetCommissionTrancportCommand
        {
            get
            {
                return new Command((win) =>
                {
                    var window = (win as Window);

                    if (CardTrancport.Commission)
                    {
                        window?.Close();
                        CardCommissionTrancportView view = new CardCommissionTrancportView(this);
                        view.ShowDialog();
                    }else
                    {
                       window?.Close(); CardTrancportView view = new CardTrancportView(this);
                        view.ShowDialog();
                        
                    }
                });
            }
        }
        public Command NewPreCheckCommand
        {
            get
            {
                return new Command(x =>
                {
                    PreCheckCardTrancportViewModel model = new PreCheckCardTrancportViewModel();
                    PreCheckCardTrancportView view = new PreCheckCardTrancportView(model);
                    view.ShowDialog();
                    if (model.IsAdd)
                    {
                        model.Enable = true;
                        PreCheks.Add(model);
                    }
                });
            }
        }
        public Command OpenPreCheckCommand
        {
            get
            {
                return new Command(x =>
                {
                    LoadingViewHalper.ShowDialog("Загрузка...", () =>
                    {
                        try
                        {
                            PreCheckCardTrancportView view = new PreCheckCardTrancportView(CurrentPreCheck);
                            view.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    });
                });
            }
        }
    }
}
