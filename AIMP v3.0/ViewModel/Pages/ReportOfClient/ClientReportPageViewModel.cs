using Aimp.Model.ReportOfClient;
using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Helpers;
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
    public class ClientReportPageViewModel : BasePageViewModel<ClientReportListItem>, IPageViewModel
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

                    List = result.Items.GroupBy(g => g.ClientReportId)
                    .Select(x => new ClientReportListItem()
                    {
                        Id = x.Key,
                        BankStatusesReportClient = (from b in result.Banks
                                                    join bs in x.Select(y => new { y.BankId, y.BankStatus.MiddleName })
                                                        on b.Id equals bs.BankId
                                                        into statusDefault
                                                    from bs in statusDefault.DefaultIfEmpty()
                                                    select bs?.MiddleName).ToArray(),
                        ClientStatusReportClient = x.First().ClientReport.ClientStatus.Name,
                        DateReportClient = x.First().ClientReport.Date,
                        FullNameReportClient = x.First().ClientReport.FullName,
                        ManagerReportClient = x.First().ClientReport.User.LastName,
                        PriceTrancportReportClient = x.First().ClientReport.Price.ToString(),
                        ProgrammCreditReportClient = x.First().ClientReport.CreditProgramm.Name,
                        SourceInfoReportClient = x.First().ClientReport.Source,
                        TelefonReportClient = x.First().ClientReport.Telefon,
                        TotalContributionReportClient = x.First().ClientReport.TotalContribution.ToString(),
                        TrancportNameReportClient = x.First().ClientReport.Trancport
                    })
                    .OrderByDescending(x => x.DateReportClient)
                    .ToList();
                    
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
                MessageBox.Show(ex.Message, "Не удалось получить список");
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
                            MessageBox.Show(ex.Message, "Неудалось создать документ");
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
                            MessageBox.Show(ex.Message, "Неудалось открыть документ");
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
                                var result = aimp.GetClientReportList(_banks, FilteringList);

                                OpenUserFile.Open(result.FileName, result.File);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Не удалось сформировать отчет");
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
