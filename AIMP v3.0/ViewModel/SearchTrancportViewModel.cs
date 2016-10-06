using Aimp.Entities;
using Aimp.ServiceContracts.AimpInfo;
using AIMP_v3._0.Aimp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AIMP_v3._0.ViewModel
{
    public class SearchTrancportViewModel : BaseViewModel
    {
        public SearchTrancportViewModel()
        {
            TypeSearch = "марка";
        }

        public string TypeSearch { get; set; }

        public string SearchText { get; set; }

        public ITrancport CurrentTrancport { get; set; }

        public ObservableCollection<ITrancport> Trancports { get; set; }

        public Command SearchCommand
        {
            get
            {
                return new Command(x =>
                {

                    //LoadingDialogHelper.Show("Поиск...");
                    
                    try
                    {
                        IEnumerable<ITrancport> result = null;

                        using (var service = ServiceClientProvider.GetAimpInfo())
                        {
                            if (string.IsNullOrEmpty(SearchText))
                            {
                                result = service.SearchTrancports(TypeSearchTrancport.Empty, null);
                            }
                            else
                            {
                                switch (TypeSearch.ToLower())
                                {
                                    case "марка":
                                    {
                                        result = service.SearchTrancports(TypeSearchTrancport.Make, SearchText);
                                        break;
                                    }
                                    case "модель":
                                    {
                                        result = service.SearchTrancports(TypeSearchTrancport.Model, SearchText);
                                        break;
                                    }
                                    case "vin":
                                    {
                                        result = service.SearchTrancports(TypeSearchTrancport.Vin, SearchText);
                                        break;
                                    }
                                }
                            }
                            if (result == null || result.Count() == 0)
                            {
                                MessageBox.Show("Поиск не дал результатов");
                                return;
                            }
                            Trancports = new ObservableCollection<ITrancport>(result);

                            OnPropertyChanged("Trancports");
                        }
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
                    if (CurrentTrancport != null)
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
                        MessageBox.Show("Необходимо выбрать ТС из списка!");
                    }
                });
            }
        }
    }
}
