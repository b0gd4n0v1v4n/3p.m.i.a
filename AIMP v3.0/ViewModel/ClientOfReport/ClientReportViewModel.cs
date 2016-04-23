using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.Logging;
using AIMP_v3._0.View;
using Models.Documents;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using AIMP_v3._0.Extensions;
using Nelibur.ObjectMapper;

namespace AIMP_v3._0.ViewModel.ClientOfReport
{
    public class ClientReportViewModel : BaseViewModel
    {
        public ClientReport _clientReportForCompare;
        private bool _Validation()
        {
            if (!CheckValue.Check(ClientReport.Date))
            {
                MessageBox.Show("Поле 'Дата' не заполнено");
                return false;
            }

            if (!CheckValue.Check(ClientReport.FullName))
            {
                MessageBox.Show("Поле 'ФИО' не заполнено");
                return false;
            }

            if (!CheckValue.Check(ClientReport.Trancport))
            {
                MessageBox.Show("Поле 'Транспорт' не заполнено");
                return false;
            }

            if (!CheckValue.Check(ClientReport.Price))
            {
                MessageBox.Show("Поле 'Стоимость' не заполнено");
                return false;
            }

            if (!CheckValue.Check(ClientReport.CreditProgramm))
            {
                MessageBox.Show("Программа кредитования не выбрана");
                return false;
            }

            if (!CheckValue.Check(ClientReport.ClientStatus))
            {
                MessageBox.Show("Статус клиента не выбран");
                return false;
            }

            var selectedBanks = ClientBankStatus.Where(x => x.Enable);

            if (selectedBanks.Count() == 0)
            {
                MessageBox.Show("Не один из банков не выбран");
                return false;
            }

            if(selectedBanks.Any(x=>x.SelectedBankStatus == null))
            {
                MessageBox.Show("У одного из банков не выбран статус");
                return false;
            }

            return true;
        }

        private ClientReportDocument _clientReportDocument;
        private ClientReport _clientReport;
        private void _Settings(int? id)
        {
            using(var service = new AimpService())
            {
                var response = service.GetClientReport(id);

                if (id == null)
                {
                    ClientReport = new ClientReport();
                    _clientReportDocument = new ClientReportDocument();
                    _clientReport = new ClientReport();
                    ClientBankStatus = new ObservableCollection<ClientBankStatusViewModel>(response.Banks
                                                                                    .Select(x => new ClientBankStatusViewModel()
                                                                                    {
                                                                                        Bank = x,
                                                                                        BankStatuses = response.BankStatuses,
                                                                                        SelectedBankStatus = response.BankStatuses.FirstOrDefault()
                                                                                    }));
                }
                else
                {
                    ClientReport = response.Document.BankReportClients.First().ClientReport;
                    _clientReport = TinyMapper.Map<ClientReport>(ClientReport);
                    ClientReport.ClientStatus = response.ClientStatuses.FirstOrDefault(x => x.Id == ClientReport.ClientStatus?.Id);

                    ClientReport.CreditProgramm = response.CreditProgramms.FirstOrDefault(x => x.Id == ClientReport.CreditProgramm?.Id);

                    _clientReportDocument = response.Document;

                    var clientBankStatuses = (from bRep in response.Banks
                                              join bClient in response.Document.BankReportClients
                                              on bRep.Id equals bClient.Bank.Id into bankDefault
                                              from bClient in bankDefault.DefaultIfEmpty()
                                              select new { bClient?.Id,
                                                           Enabled = bClient != null ? true : false,
                                                           Bank = bRep,
                                                           BankStatus = bClient?.BankStatus })
                                             .Select(x => new ClientBankStatusViewModel()
                                             {
                                                 Id = x.Id != null ? (int)x.Id : 0,
                                                 Bank = x.Bank,
                                                 Enable = x.Enabled,
                                                 BankStatuses = response.BankStatuses,
                                                 SelectedBankStatus = response.BankStatuses.FirstOrDefault(y=>y.Id == x.BankStatus?.Id)
                                             });

                    ClientBankStatus = new ObservableCollection<ClientBankStatusViewModel>(clientBankStatuses);
                }

                ClientStatuses = new ObservableCollection<ClientStatus>(response.ClientStatuses);

                CreditProgramms = new ObservableCollection<CreditProgramm>(response.CreditProgramms);
            }
        }

        public ClientReportViewModel(int? id)
        {
            _Settings(id);
        }

        public ClientReport ClientReport { get; private set; }
        
        public ObservableCollection<ClientStatus> ClientStatuses { get; private set; }

        public ObservableCollection<CreditProgramm> CreditProgramms { get; private set; }

        public ObservableCollection<ClientBankStatusViewModel> ClientBankStatus { get; private set; }

        public Command SaveChangesCommand
        {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        if (_Validation())
                        {
                            using (var service = new AimpService())
                            {
                                _clientReportDocument.BankReportClients = null;

                                var newBankReportClients = new List<BankReportClient>();

                                foreach(var iClientBankStatuses in ClientBankStatus.Where(y => y.Enable))
                                {
                                    newBankReportClients.Add(new BankReportClient()
                                    {
                                        Id = iClientBankStatuses.Id,
                                        Bank = iClientBankStatuses.Bank,
                                        BankStatus = iClientBankStatuses.SelectedBankStatus,
                                        ClientReport = ClientReport
                                    });
                                }

                                _clientReportDocument.BankReportClients = newBankReportClients.ToArray();

                                var response = service.SaveClientReport(_clientReportDocument);

                                if (response.Error)
                                    throw new Exception(response.Message);
                                else
                                    MessageBox.Show(response.Message);
                            }
                            _clientReport = TinyMapper.Map<ClientReport>(ClientReport);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.Log("Не удалось сохранить клиентский отчет", "SaveChangesCommand", ex);
                    }
                });
            }
        }
        public Command DeleteChangesCommand
        {
            get
            {
                return new Command((window) =>
                {
                    try
                    {
                        if (new QuestClosingView("Удалить документ?").ShowDialog() == true)
                        {
                            using (var service = new AimpService())
                            {
                                var response = service.DeleteClientReport(_clientReportDocument);

                                if (response.Error)
                                    throw new Exception(response.Message);
                                else
                                {
                                    var win = window as Window;
                                    win.Hide();
                                    MessageBox.Show(response.Message);
                                    win.Close();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.Log("Не удалось удалить документ", "DeleteChangesCommand", ex);
                    }
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

                    if (!_clientReport.CompareProperties(ClientReport))
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
    }
}
