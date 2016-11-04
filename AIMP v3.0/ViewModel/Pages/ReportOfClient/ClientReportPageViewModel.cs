using Aimp.Model.ReportOfClient;
using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.Logging;
using AIMP_v3._0.View;
using AIMP_v3._0.ViewModel.ClientOfReport;
using Entities;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace AIMP_v3._0.ViewModel.Pages.ReportOfClient
{
    public class ClientReportPageViewModel : BasePageViewModel<ClientReportListItemViewModel>, IPageViewModel
    {

        private bool _isOneLoad;
        private IEnumerable<Bank> _banks;
        private void _FillListReportOfClient()
        {
            try
            {
                using (var aimp = new AimpService())
                {
                    var result = aimp.GetClientReports();

                    _banks = result.Banks;
                    BanksColumnName = new ObservableCollection<string>(result.Banks.Select(x => x.Name));

                    List = TinyMapper.Map<List<ClientReportListItemViewModel>>(result.Items);


                    if (!_isOneLoad && result.ClientStatusesForFilter != null &&
                        result.ClientStatusesForFilter.Count() > 0)
                    {
                        _isOneLoad = true;
                        SetFilter("ClientStatusReportClient", result.ClientStatusesForFilter.ToArray());
                        KASTIL_BRASH_FOR_CLIENT_STATUS = Brushes.Orange;
                        OnPropertyChanged("KASTIL_BRASH_FOR_CLIENT_STATUS");
                    }
                    else
                    {
                        ClearFilteres();
                    }
                    if (!_isOneLoad && !string.IsNullOrEmpty(result.UserLastNameForFilter))
                    {
                        _isOneLoad = true;
                        SetFilter("ManagerReportClient", new[] {result.UserLastNameForFilter});
                        KASTIL_BRASH_FOR_MANAGER = Brushes.Orange;
                        OnPropertyChanged("KASTIL_BRASH_FOR_MANAGER");
                    }
                    else
                    {
                        ClearFilteres();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Log("Не удалось получить список", "_FillListReportOfClient", ex);
            }
        }

        public ClientReportPageViewModel()
        {
            _FillListReportOfClient();
        }

        public ObservableCollection<string> BanksColumnName
        {
            get; private set;
        }

        public string Name
        {
            get
            {
                return "ОТЧЕТЫ КЛИЕНТОВ";
            }
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
                            ReportOfClientView view = new ReportOfClientView(new ClientReportViewModel(null));
                            view.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.Log("Неудалось создать документ", "New", ex);
                        }
                    });
                });
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
                            if (CurrentItem?.Id != null)
                            {
                                var model = new ClientReportViewModel(CurrentItem.Id);

                                ReportOfClientView view = new ReportOfClientView(model);
                                view.ShowDialog();
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.Log("Неудалось открыть документ", "New", ex);
                        }
                    });
                });
            }
        }

        public override Command PrintList
        {
            get
            {
                return new Command(x =>
                {
                    LoadingViewHalper.ShowDialog("Формирование документа...", () =>
                    {
                        try
                        {
                            using (var aimp = new AimpService())
                            {
                                var clientReports = new ClientReports();
                                clientReports.Banks = _banks;
                                clientReports.Items = FilteringList.Select(y => new ClientReportListItem()
                                {
                                    DateReportClient = y.DateReportClient,
                                    FullNameReportClient = y.FullNameReportClient,
                                    TelefonReportClient = y.TelefonReportClient,
                                    TrancportNameReportClient = y.TrancportNameReportClient,
                                    PriceTrancportReportClient = y.PriceTrancportReportClient,
                                    TotalContributionReportClient = y.TotalContributionReportClient,
                                    ProgrammCreditReportClient = y.ProgrammCreditReportClient,
                                    BankStatusesReportClient = y.BankStatusesReportClient,
                                    ClientStatusReportClient = y.ClientStatusReportClient,
                                    SourceInfoReportClient = y.SourceInfoReportClient
                                });
                                var result = aimp.GetClientReportList(clientReports);

                                    OpenUserFile.Open(result.FileName, result.File);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.Log("Не удалось сформировать отчет", "PrintList", ex);
                        }
                    });
                });
            }
        }

        public Command Refresh
        {
            get
            {
                return new Command(x =>
                {
                    LoadingViewHalper.ShowDialog("Загрузка...", () =>
                    {
                        _FillListReportOfClient();
                    });
                });
            }
        }

        public Visibility PrintButtonVisible
        {
            get
            {
                return Visibility.Visible;
            }
        }
        public override Visibility AddButtonVisible
        {
            get
            {
                return Visibility.Visible;
            }
        }
    }
}
