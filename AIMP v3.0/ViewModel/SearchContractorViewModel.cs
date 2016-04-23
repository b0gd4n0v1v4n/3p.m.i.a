using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using AIMP_v3._0.DataAccess;
using Models.Entities;
using Models.ContractorInfo;
using System.Linq;

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

        public Contractor CurrentContractor { get; set; }

        public ObservableCollection<Contractor> Contractors { get; set; }

        public Command SearchCommand
        {
            get
            {
                return new Command(x =>
                {

                    //LoadingDialogHelper.Show("Поиск...");

                    try
                    {
                        SearchContractorResult result = null;

                        using (AimpService service = new AimpService())
                        {
                            if (string.IsNullOrEmpty(SearchText))
                            {
                                result = service.SearchContractor(TypeSearchContractor.Empty, null);
                            }
                            else
                            {
                                switch (TypeSearch.ToLower())
                                {
                                    case "фамилия":
                                    {
                                        result = service.SearchContractor(TypeSearchContractor.LastName, SearchText);
                                        break;
                                    }
                                    case "организация":
                                    {
                                        result = service.SearchContractor(TypeSearchContractor.Organization, SearchText);
                                        break;
                                    }
                                    case "инн":
                                    {
                                        result = service.SearchContractor(TypeSearchContractor.Inn, SearchText);
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
                            Contractors = new ObservableCollection<Contractor>(result.Contractors);
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
