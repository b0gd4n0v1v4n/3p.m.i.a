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
using AIMP_v3._0.ViewModel.Dictionaries;

namespace AIMP_v3._0.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public Visibility VisibleAdmin { get { if (CurrentUser.IsAdmin) return Visibility.Visible; else return Visibility.Hidden; } }
        public List<IPageViewModel> Tabs { get; set; }
        private IPageViewModel _currentPage;
        public IPageViewModel CurrentPage { get { return _currentPage; } set { _currentPage = value; OnPropertyChanged("CurrentPage"); } }
        public ObservableCollection<DictionaryMenuItemViewModel> DictionaryList { get; }
        public MainViewModel()
        {
            try
            {
                var idNameColumn = new List<ColumnViewModel>()
                {
                    new ColumnViewModel(){ Name = "Id", DbName = "Id" },
                    new ColumnViewModel(){ Name = "Наименование", DbName = "Name" }
                };
                DictionaryList = new ObservableCollection<DictionaryMenuItemViewModel>()
                {
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "TrancportTypes",
                        Name = "ВИДЫ ТРАНСПОРТА",
                        Columns = idNameColumn
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "TrancportCategories",
                        Name = "КАТИГОРИИ ТРАНСПОРТА",
                        Columns = idNameColumn
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "Creditors",
                        Name = "КРЕДИТОРЫ",
                        Columns = idNameColumn
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "Requisits",
                        Name = "РЕКВИЗИТЫ",
                        Columns = new List<ColumnViewModel>() { new ColumnViewModel() { Name = "Id",DbName = "Id" } }
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "BankStatus",
                        Name = "СТАТУСЫ БАНКА",
                        Columns = idNameColumn
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "ClientStatus",
                        Name = "СТАТУСЫ КЛИЕНТА",
                        Columns = idNameColumn
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "EngineTypes",
                        Name = "ТИПЫ ДВИГАТЕЛЕЙ",
                        Columns = idNameColumn
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "Banks",
                        Name = "БАНКИ",
                        Columns = idNameColumn
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "CreditProgramms",
                        Name = "ПРОГРАММЫ КРЕДИТОВАНИЯ",
                        Columns = idNameColumn
                    },
                    new DictionaryMenuItemViewModel
                    {
                        TableName = "SourceTrancports",
                        Name = "ИСТОЧНИКИ ПОСТУПЛЕНИЯ",
                        Columns = idNameColumn
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

                CurrentPage = Tabs[0];
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
                        var r = CurrentPage;
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