using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using Aimp.Entities;
using AIMP_v3._0.Aimp.Services;
using Aimp.ServiceContracts.AimpInfo;
using System.Collections.Generic;

namespace AIMP_v3._0.ViewModel
{
    public class SearchContractorViewModel : BaseViewModel
    {
        public SearchContractorViewModel()
        {
            TypeSearch = "фамилия";
        }
        public string TypeSearch { get; set; }

        public string SearchText { get; set; }

        public IContractor CurrentContractor { get; set; }

        public ObservableCollection<IContractor> Contractors { get; set; }

        public Command SearchCommand
        {
            get
            {
                return new Command(x =>
                {

                    //LoadingDialogHelper.Show("Поиск...");

                    try
                    {
                        IEnumerable<IContractor> result = null;

                        using (var service = ServiceClientProvider.GetAimpInfo())
                        {
                            if (string.IsNullOrEmpty(SearchText))
                            {
                                result = service.SearchContractors(TypeSearchContractor.Empty, null);
                            }
                            else
                            {
                                switch (TypeSearch.ToLower())
                                {
                                    case "фамилия":
                                    {
                                        result = service.SearchContractors(TypeSearchContractor.LastName, SearchText);
                                        break;
                                    }
                                    case "организация":
                                    {
                                        result = service.SearchContractors(TypeSearchContractor.Organization, SearchText);
                                        break;
                                    }
                                    case "инн":
                                    {
                                        result = service.SearchContractors(TypeSearchContractor.Inn, SearchText);
                                        break;
                                    }
                                }
                            }
                            if (result.Error)
                            {
                                MessageBox.Show(result.Message);
                                return;
                            }
                            if(result.Contractors == null || result.Contractors.Count() == 0)
                            {
                                MessageBox.Show("Поиск не дал результатов");
                                return;
                            }
                            Contractors = new ObservableCollection<IContractor>(result.Contractors);
                        }

                        OnPropertyChanged("Contractors");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка");
                    }
                    //finally
                    //{
                    //    LoadingDialogHelper.Hide();
                    //}
                });
            }
        }

        public Command SetSearchTypeCommand
        {
            get
            {
                return new Command((element) =>
                {
                    var radio = element as RadioButton;

                    if (radio != null)
                    {
                        TypeSearch = radio.Content.ToString();
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
                    var window = win as Window;

                    if (window != null)
                    {
                        window.DialogResult = false;
                        window.Close();
                    }
                });
            }
        }

        public Command OkCommand
        {
            get
            {
                return new Command((win) =>
                {
                    if (CurrentContractor != null)
                    {
                        var window = win as Window;

                        if (window != null)
                        {
                            window.DialogResult = true;
                            window.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Необходимо выбрать контрагента из списка!");
                    }
                });
            }
        }
    }
}
