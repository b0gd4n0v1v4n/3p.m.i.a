﻿using Aimp.Model.Documents;
using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.View;
using Entities;
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
                    ClientReport.ClientStatus = response.ClientStatuses.FirstOrDefault(x => x.Id == ClientReport.ClientStatusId);

                    ClientReport.CreditProgramm = response.CreditProgramms.FirstOrDefault(x => x.Id == ClientReport.CreditProgrammId);

                    _clientReportDocument = response.Document;

                    var clientBankStatuses = (from bRep in response.Banks
                                              join bClient in response.Document.BankReportClients
                                              on bRep.Id equals bClient.BankId into bankDefault
                                              from bClient in bankDefault.DefaultIfEmpty()
                                              select new
                                              {
                                                  bClient?.Id,
                                                  Enabled = bClient != null ? true : false,
                                                  Bank = bRep,
                                                  BankStatusId = bClient?.BankStatusId
                                              })
                                             .Select(x => new ClientBankStatusViewModel()
                                             {
                                                 Id = x.Id != null ? (int)x.Id : 0,
                                                 Bank = x.Bank,
                                                 Enable = x.Enabled,
                                                 BankStatuses = response.BankStatuses,
                                                 SelectedBankStatus = response.BankStatuses.FirstOrDefault(y => y.Id == x.BankStatusId)
                                             }).ToList();

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
                    if (_Validation())
                    {
                        LoadingViewHalper.ShowDialog("Сохранение...", () =>
                        {
                            try
                            {
                                using (var service = new AimpService())
                                {
                                    _clientReportDocument.BankReportClients = null;

                                    var newBankReportClients = new List<BankReportClient>();

                                    foreach (var iClientBankStatuses in ClientBankStatus.Where(y => y.Enable))
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

                                    ClientReport.Id = service.SaveClientReport(_clientReportDocument);
                                }
                                _clientReport = TinyMapper.Map<ClientReport>(ClientReport);
                            }

                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Не удалось сохранить клиентский отчет");
                            }
                        });
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

                    if (new QuestClosingView("Удалить документ?").ShowDialog() == true)
                    {
                        LoadingViewHalper.ShowDialog("Удаление...", () =>
                        {
                        try
                        {

                            using (var service = new AimpService())
                            {
                                service.DeleteClientReport(_clientReportDocument);

                                var win = window as Window;
                                win.Close();
                            }

                        }
                        catch (Exception ex)
                        {
                                MessageBox.Show(ex.Message, "Не удалось удалить документ");
                        }
                        });
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
