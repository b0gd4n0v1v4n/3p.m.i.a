using System.Collections;
using AIMP_v3._0.ViewModel.Pages.ReportOfClient;
using AIMP_v3._0.ViewModel.Pages.CashTransaction;
using AIMP_v3._0.View;
using AIMP_v3._0.ViewModel.UserRight;
using AIMP_v3._0.DataAccess;
using System.Windows;
using System;
using System.Collections.ObjectModel;
using Models;
using Models.Entities;
using AIMP_v3._0.ViewModel.Pages.CreditDocument;
using AIMP_v3._0.ViewModel.Pages.Commission;
using AIMP_v3._0.ViewModel.CardsTrancport;
using AIMP_v3._0.ViewModel.Pages.CardsTrancport;
using System.Collections.Generic;
using AIMP_v3._0.ViewModel.Pages;
using AIMP_v3._0.Model;

namespace AIMP_v3._0.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public Visibility VisibleAdmin { get { if (CurrentUser.IsAdmin) return Visibility.Visible; else return Visibility.Hidden; } }
        public List<IPageViewModel> Tabs { get; set; }
        public IPageViewModel CurrentPage { get; set; }
        public ObservableCollection<DictionaryMenuItemViewModel> DictionaryList { get; }
        public MainViewModel()
        {
            try
            {
                CurrentUser.Auth("admin", "admin");
                DictionaryList = new ObservableCollection<DictionaryMenuItemViewModel>()
                {
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "TrancportTypes",
                        Name = "ВИДЫ ТРАНСПОРТА"
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "TrancportCategories",
                        Name = "КАТИГОРИИ ТРАНСПОРТА"
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "Creditors",
                        Name = "КРЕДИТОРЫ"
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "Requisits",
                        Name = "РЕКВИЗИТЫ"
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "BankStatuses",
                        Name = "СТАТУСЫ БАНКА"
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "ClientStatuses",
                        Name = "СТАТУСЫ КЛИЕНТА"
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "EngineTypes",
                        Name = "ТИПЫ ДВИГАТЕЛЕЙ"
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "Banks",
                        Name = "БАНКИ"
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "CreditProgramms",
                        Name = "ПРОГРАММЫ КРЕДИТОВАНИЯ"
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "SourceTrancports",
                        Name = "ИСТОЧНИКИ ПОСТУПЛЕНИЯ"
                    }
                };
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
            Tabs = new List<IPageViewModel>();
            if (CurrentUser.IsAdmin || CurrentUser.IsView)
            {
                Tabs.Add(new ClientReportPageViewModel());

                Tabs.Add(new CashTransactionPageViewModel());

                Tabs.Add(new CreditTransactionPageViewModel());

                Tabs.Add(new CommissionPageViewModel());

                Tabs.Add(new CardsTrancportPageViewModel());

                CurrentPage = Tabs[1];
            }
        }
        public Command EditPrintedDocTemplateCommand
        {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        PrintedDocumentListView view = new PrintedDocumentListView(new PrintedDocument.PrintedDocumentsListViewModel());
                        view.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });
            }
        }
        public Command EditUserRightsCommand
        {
            get
            {
                return new Command(x => 
                {
                    try
                    {
                        UserRightsView view = new UserRightsView(new UsersViewModel());
                        view.ShowDialog();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });
            }
        }
    }
}