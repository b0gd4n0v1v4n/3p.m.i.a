﻿
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using AIMP_v3._0.Interfaces;
using AIMP_v3._0.View;
using AIMP_v3._0.Extensions;
using Nelibur.ObjectMapper;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.Aimp.Services;
using Aimp.Entities;
using Aimp.Model.Entities;

namespace AIMP_v3._0.ViewModel
{
    public class ContractorInfoViewModel : BaseViewModel
    {
        private List<ICity> _cities;

        private IObjectSetValue _window;

        private bool _Validation()
        {
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
            if (!CheckValue.Check(EditableContractor.DateDocument))
            {
                MessageBox.Show("Поле 'дата выдачи' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableContractor.SerialDocument))
            {
                MessageBox.Show("Поле 'серия' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableContractor.NumberDocument))
            {
                MessageBox.Show("Поле 'номер' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableContractor.ByDocument))
            {
                MessageBox.Show("Поле 'кем выдан' не заполнено!");
                return false;
            }
            return true;
        }

        public IContractor EditableContractor { get; }
        private IContractor _contractor;
        public bool IsLegalPerson { get; set; }

        public Visibility IsNew { get; set; }

        public ObservableCollection<ICity> Cities { get; private set; }

        public List<IRegion> Regions { get; private set; }

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

                        Cities = new ObservableCollection<ICity>();

                        City = string.Empty;
                    }
                    else
                    {
                        Cities = new ObservableCollection<ICity>(_cities.Where(x => x.Region.Name == value));

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
            using (var service = ServiceClientProvider.GetAimpInfo())
            {
                var info = service.GetContractorInfo();
                _cities = info.Cities?.ToList();
                Regions = info.Regions?.ToList();
            }
        }

        public ContractorInfoViewModel(IContractor contractor, IObjectSetValue window)
        {
            _window = window;

            if (contractor.Id > 0)
            {
                IsNew = Visibility.Hidden;

                EditableContractor = TinyMapper.Map<IContractor>(contractor);
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

        public Command SaveChanges
        {
            get
            {
                return new Command((win) =>
                {
                    try
                    {
                    if (_Validation())
                    {
                        var region = Regions.FirstOrDefault(x => x.Name == EditableContractor.Region.Name);

                        if (region == null)
                            region = new Region()
                            {
                                Name = EditableContractor.Region.Name
                            };

                        EditableContractor.Region = region;

                        var city = _cities.FirstOrDefault(x => x.Name == EditableContractor.City.Name);

                        if (city == null)
                            city = new City()
                            {
                                Name = EditableContractor.City.Name,
                                Region = EditableContractor.Region
                            };

                        EditableContractor.City = city;
                        using (var service = ServiceClientProvider.GetAimpInfo())
                        {
                            var response = service.SaveContractor(EditableContractor);

                            EditableContractor.Id = response.Id;
                        }
                        var window = (win as Window);

                        if (window != null)
                        {
                           _window.SetValue(EditableContractor);

                            window.Close();
                        }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
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

        public Command SetLegalPerson
        {
            get
            {
                return new Command((win) =>
                {
                    if (IsLegalPerson)
                    {
                        var window = (win as Window);

                        EditableContractor.LegalPerson = new LegalPerson();

                        var lpv = new LegalPersonInfoView(new LegalPersonViewModel(EditableContractor, _window));

                        window.Close();

                        lpv.ShowDialog();
                    }
                });
            }
        }
    }
}
