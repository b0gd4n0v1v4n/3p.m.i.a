using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using AIMP_v3._0.Interfaces;
using AIMP_v3._0.View;
using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Extensions;
using Nelibur.ObjectMapper;
using AIMP_v3._0.Helpers;
using Models.Entities;

namespace AIMP_v3._0.ViewModel
{
    public class LegalPersonViewModel : BaseViewModel
    {
        private List<City> _cities;

        public Contractor EditableContractor { get; }
        private Contractor _contractor;
        private IObjectSetValue _window
            ;
        public bool IsPhisicalPerson { get; set; }

        public Visibility IsNew { get; set; }

        public ObservableCollection<City> Cities { get; private set; }

        public List<Region> Regions { get; private set; }

        public string Region
        {
            get
            {
                return EditableContractor.Region.Name;
            }
            set
            {
                if (value != null)
                {
                    var region = Regions.FirstOrDefault(x => x.Name == value);

                    if (region == null)
                    {
                        EditableContractor.Region = new Region()
                        {
                            Name = value
                        };

                        Cities = new ObservableCollection<City>();

                        City = string.Empty;
                    }
                    else
                    {
                        Cities = new ObservableCollection<City>(_cities.Where(x => x.Region.Name == value));

                        EditableContractor.Region = region;
                    }

                    OnPropertyChanged("Region");
                    OnPropertyChanged("Cities");
                }
            }
        }

        public string City
        {
            get
            {
                return EditableContractor.City.Name;
            }
            set
            {
                if (value != null)
                {
                    var city = _cities.FirstOrDefault(x => x.Name == value);

                    if (city == null)
                    {
                        EditableContractor.City = new City()
                        {
                            Name = value
                        };
                    }
                    else
                    {
                        EditableContractor.City = city;
                    }
                    OnPropertyChanged("City");
                }
            }
        }

        private void _Settings()
        {
            using (AimpService service = new AimpService())
            {
                var info = service.GetContractorInfo();
                if (info.Error)
                    throw new Exception(info.Message);
                _cities = info.Cities.ToList();
                Regions = info.Regions.ToList();
            }
        }

        public LegalPersonViewModel(Contractor contractor, IObjectSetValue window)
        {
            _window = window;

            if (contractor.Id > 0)
            {
                IsNew = Visibility.Hidden;

                EditableContractor = TinyMapper.Map<Contractor>(contractor);
                _contractor = contractor;
            }
            else
            {
                IsNew = Visibility.Visible;

                EditableContractor = new Contractor()
                {
                    City = new City(),
                    Region = new Region()
                };
            }
            _Settings();
        }

        private bool _Validation()
        {
            if (!CheckValue.Check(EditableContractor.LegalPerson.Name))
            {
                MessageBox.Show("Поле 'наименование' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableContractor.FirstName))
            {
                MessageBox.Show("Поле 'Имя' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableContractor.LastName))
            {
                MessageBox.Show("Поле 'Фамилия' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableContractor.MiddleName))
            {
                MessageBox.Show("Поле 'Отчество' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableContractor.FirstNameGenitive))
            {
                MessageBox.Show("Поле 'Имя[Р]' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableContractor.LastNameGenitive))
            {
                MessageBox.Show("Поле 'Фамилия[Р]' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableContractor.MiddleNameGenitive))
            {
                MessageBox.Show("Поле 'Отчество[Р]' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableContractor.DateBirth))
            {
                MessageBox.Show("Поле 'дата рождения' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableContractor.Region?.Name))
            {
                MessageBox.Show("Поле 'регион' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableContractor.City?.Name))
            {
                MessageBox.Show("Поле 'город' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableContractor.LegalPerson.Inn))
            {
                MessageBox.Show("Поле 'инн' не заполнено!");
                return false;
            }
            return true;
        }

        public Command SaveChanges
        {
            get
            {
                return new Command((win) =>
                { if (_Validation())
                    {
                        try
                        {

                            LoadingViewHalper.ShowDialog("Сохранение...", () =>
                            {
                                Region region = Regions.FirstOrDefault(x => x.Name == EditableContractor.Region.Name);

                                if (region == null)
                                    region = new Region()
                                    {
                                        Name = EditableContractor.Region.Name
                                    };

                                EditableContractor.Region = region;

                                City city = _cities.FirstOrDefault(x => x.Name == EditableContractor.City.Name);

                                if (city == null)
                                    city = new City()
                                    {
                                        Name = EditableContractor.City.Name,
                                        Region = EditableContractor.Region
                                    };

                                EditableContractor.City = city;
                                using (var service = new AimpService())
                                {
                                    var response = service.SaveContractor(EditableContractor);
                                    if (response.Error)
                                        throw new Exception(response.Message);
                                    EditableContractor.Id = response.Id;
                                }
                                var window = (win as Window);

                                if (window != null)
                                {
                                    _window.SetValue(EditableContractor);

                                    window.Close();
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        } } 
                });
            }
        }

        public Command CloseWindowCommand
        {
            get
            {
                return new Command((win) =>
                {
                    if (EditableContractor.Id == 0)
                        _window.SetValue(null);

                    var window = (win as Window);

                    if (!_contractor.CompareProperties(EditableContractor))
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

        public Command SetPhisicalPerson
        {
            get
            {
                return new Command((win) =>
                {
                    if (IsPhisicalPerson)
                    {
                        var window = (win as Window);

                        EditableContractor.LegalPerson = null;

                        var cv = new ContractorInfoView(new ContractorInfoViewModel(EditableContractor, _window));

                        window.Close();

                        cv.ShowDialog();
                    }
                });
            }
        }
    }
}
