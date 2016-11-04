using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Aimp.Model.TrancportInfo;
using AIMP_v3._0.DataAccess;
using Entities;
using AIMP_v3._0.Helpers;

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

        public Trancport CurrentTrancport { get; set; }

        public ObservableCollection<Trancport> Trancports { get; set; }

        public Command SearchCommand
        {
            get
            {
                return new Command(x =>
                {
                    LoadingViewHalper.ShowDialog("Поиск...", () =>
                    {
                        try
                        {
                            IEnumerable<Trancport> result = null;

                            using (AimpService service = new AimpService())
                            {
                                if (string.IsNullOrEmpty(SearchText))
                                {
                                    result = service.SearchTranports(TypeSearchTrancport.Empty, null);
                                }
                                else
                                {
                                    switch (TypeSearch.ToLower())
                                    {
                                        case "марка":
                                            {
                                                result = service.SearchTranports(TypeSearchTrancport.Make, SearchText);
                                                break;
                                            }
                                        case "модель":
                                            {
                                                result = service.SearchTranports(TypeSearchTrancport.Model, SearchText);
                                                break;
                                            }
                                        case "vin":
                                            {
                                                result = service.SearchTranports(TypeSearchTrancport.Vin, SearchText);
                                                break;
                                            }
                                    }
                                }
                                if (result == null || result.Count() == 0)
                                {
                                    MessageBox.Show("Поиск не дал результатов");
                                    return;
                                }
                                Trancports = new ObservableCollection<Trancport>(result);

                                OnPropertyChanged("Trancports");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка");
                        }
                    });
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
