using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Interfaces;
using System.Linq;
using Entities;
using Nelibur.ObjectMapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Windows;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.DataInformation;
using AIMP_v3._0.Extensions;
using AIMP_v3._0.View;

namespace AIMP_v3._0.ViewModel
{
    public class TrancportInfoViewModel : BaseViewModel
    {
        private IObjectSetValue _window;

        private IEnumerable<ModelTrancport> _models;
        private Trancport _trancport;
        private bool _Validation()
        {
            if (!CheckValue.Check(EditableTrancport.Make?.Name))
            {
                MessageBox.Show("Поле 'марка' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableTrancport.Model?.Name))
            {
                MessageBox.Show("Поле 'модель' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableTrancport.Category?.Name))
            {
                MessageBox.Show("Поле 'категория ТС' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableTrancport.Type?.Name))
            {
                MessageBox.Show("Поле 'вид ТС' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableTrancport.Maker))
            {
                MessageBox.Show("Поле 'изготовитель' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableTrancport.Year))
            {
                MessageBox.Show("Поле 'год выпуска' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableTrancport.Vin))
            {
                MessageBox.Show("Поле 'vin' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableTrancport.SerialPts))
            {
                MessageBox.Show("Поле 'серия птс' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableTrancport.NumberPts))
            {
                MessageBox.Show("Поле 'номер птс' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableTrancport.DatePts))
            {
                MessageBox.Show("Поле 'дата выдачи птс' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableTrancport.ByPts))
            {
                MessageBox.Show("Поле 'кем выдан птс' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableTrancport.SerialPts))
            {
                MessageBox.Show("Поле 'серия стс' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableTrancport.NumberSts))
            {
                MessageBox.Show("Поле 'номер стс' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableTrancport.DateSts))
            {
                MessageBox.Show("Поле 'дата выдачи стс' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableTrancport.BySts))
            {
                MessageBox.Show("Поле 'кем выдан стс' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableTrancport.EngineMake))
            {
                MessageBox.Show("Поле 'марка(модель двигателя)' не заполнено!");
                return false;
            }
            if (!CheckValue.Check(EditableTrancport.EngineType?.Name))
            {
                MessageBox.Show("Поле 'тип двигателя' не заполнено!");
                return false;
            }
            return true;
        }

        public bool AsVinBodyNumber { get; set; }

        public Command AsVinBodyNumberCommand
        {
            get
            {
                return new Command(x =>
                 {
                     if (AsVinBodyNumber)
                     {
                         EditableTrancport.BodyNumber = EditableTrancport.Vin;
                         EmptyBodyNumber = false;
                         OnPropertyChanged("EmptyBodyNumber");
                         OnPropertyChanged("EditableTrancport");
                     }
                 });
            }
        }

        public bool EmptyBodyNumber { get; set; }

        public Command EmptyBodyNumberCommand
        {
            get
            {
                return new Command(x =>
                {
                    if (EmptyBodyNumber)
                    {
                        AsVinBodyNumber = false;
                        EditableTrancport.BodyNumber = "ОТСУТСТВУЕТ";
                        OnPropertyChanged("AsVinBodyNumber");
                        OnPropertyChanged("EditableTrancport");
                    }
                });
            }
        }
        public bool AsVinShassi { get; set; }

        public Command AsVinShassiCommand
        {
            get
            {
                return new Command(x =>
                {
                    if (AsVinShassi)
                    {
                        EditableTrancport.ChassisNumber = EditableTrancport.Vin;
                        EmptyShassiNumber = false;
                        OnPropertyChanged("EmptyShassiNumber");
                        OnPropertyChanged("EditableTrancport");
                    }
                });
            }
        }

        public bool EmptyShassiNumber { get; set; }

        public Command EmptyShassiNumberCommand
        {
            get
            {
                return new Command(x =>
                {
                    if (EmptyShassiNumber)
                    {
                        AsVinShassi = false;
                        EditableTrancport.ChassisNumber = "ОТСУТСТВУЕТ";
                        OnPropertyChanged("AsVinShassi");
                        OnPropertyChanged("EditableTrancport");
                    }
                });
            }
        }

        public ObservableCollection<ModelTrancport> Models { get; set; }

        public ObservableCollection<MakeTrancport> Makes { get; set; }

        public string MakeTrancport
        {
            get
            {
                return EditableTrancport?.Make?.Name;
            }
            set
            {
                if (value != null)
                {
                    var make = Makes.FirstOrDefault(x => x.Name == value);

                    if (make == null)
                    {
                        EditableTrancport.Make = new MakeTrancport()
                        {
                            Name = value
                        };

                        Models = new ObservableCollection<ModelTrancport>();

                        if (EditableTrancport.Make.Id > 0)
                        {
                            ModelTrancport = string.Empty;
                        }
                    }
                    else
                    {
                        EditableTrancport.Make = make;

                        Models = new ObservableCollection<ModelTrancport>(_models.Where(x => x.Make.Name == value));
                    }

                    OnPropertyChanged("Models");
                    OnPropertyChanged("MakeTrancport");
                }
            }
        }

        public string ModelTrancport
        {
            get
            {
                return EditableTrancport?.Model?.Name;
            }
            set
            {
                if (value != null)
                {
                    var model = _models.FirstOrDefault(x => x.Name == value);

                    if (model == null)
                    {
                        EditableTrancport.Model = new ModelTrancport()
                        {
                            Name = value
                        };
                    }
                    else
                    {
                        EditableTrancport.Model = model;
                    }

                    OnPropertyChanged("ModelTrancport");
                }
            }
        }

        public ObservableCollection<TrancportCategory> Categories { get; set; }

        public string Category
        {
            get
            {
                return EditableTrancport.Category?.Name;
            }
            set
            {
                if (value != null)
                {
                    var category = Categories.FirstOrDefault(x => x.Name == value);

                    if (category == null)
                    {
                        EditableTrancport.Category = new TrancportCategory()
                        {
                            Name = value
                        };
                    }
                    else
                    {
                        EditableTrancport.Category = category;
                    }
                }
            }
        }

        public string Type
        {
            get
            {
                return EditableTrancport.Type?.Name;
            }
            set
            {
                if (value != null)
                {
                    var type = TrancportTypes.FirstOrDefault(x => x.Name == value);

                    if (type == null)
                    {
                        EditableTrancport.Type = new TrancportType()
                        {
                            Name = value
                        };
                    }
                    else
                    {
                        EditableTrancport.Type = type;
                    }
                }
            }
        }

        public ObservableCollection<TrancportType> TrancportTypes { get; set; }

        public ObservableCollection<EngineType> EngineTypes { get; set; }

        public string EngineType
        {
            get
            {
                return EditableTrancport.EngineType?.Name;
            }
            set
            {
                if (value != null)
                {
                    var type = EngineTypes.FirstOrDefault(x => x.Name == value);

                    if (type == null)
                    {
                        EditableTrancport.EngineType = new EngineType()
                        {
                            Name = value
                        };
                    }
                    else
                    {
                        EditableTrancport.EngineType = type;
                    }
                }
            }
        }

        public Trancport EditableTrancport { get; }

        public TrancportInfoViewModel(Trancport trancport, IObjectSetValue window)
        {
            _window = window;

            if (trancport.Id > 0)
            {
                EditableTrancport = TinyMapper.Map<Trancport>(trancport);
            }
            else
            {
                EditableTrancport = new Trancport();
            }
            _trancport = trancport;
            _models = TracnportInformation.Info.Models;

            Makes = new ObservableCollection<MakeTrancport>(TracnportInformation.Info.Makes);

            Categories = new ObservableCollection<TrancportCategory>(TracnportInformation.Info.Categories);

            TrancportTypes = new ObservableCollection<TrancportType>(TracnportInformation.Info.Types);

            EngineTypes = new ObservableCollection<EngineType>(TracnportInformation.Info.EngineTypes);

            MakeTrancport = EditableTrancport?.Make?.Name; // hack crazy 
        }

        public Command SaveChanges
        {
            get
            {
                return new Command((win) =>
                {
                    if (_Validation())
                    {
                        LoadingViewHalper.ShowDialog("Сохранение...", () =>
                        {
                            try
                            {
                                MakeTrancport make = Makes.FirstOrDefault(x => x.Name == EditableTrancport.Make.Name);

                                if (make == null)
                                    make = new MakeTrancport()
                                    {
                                        Name = EditableTrancport.Make.Name
                                    };

                                EditableTrancport.Make = make;

                                ModelTrancport model = _models.FirstOrDefault(x => x.Name == EditableTrancport.Model.Name);

                                if (model == null)
                                    model = new ModelTrancport()
                                    {
                                        Name = EditableTrancport.Model.Name,
                                        Make = EditableTrancport.Make
                                    };

                                EditableTrancport.Model = model;
                                using (var service = new AimpService())
                                {
                                    EditableTrancport.Id = service.SaveTrancport(EditableTrancport);
                                }
                                var window = (win as Window);

                                if (window != null)
                                {
                                    _window.SetValue(EditableTrancport);

                                    window.Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        });
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
                    if (EditableTrancport.Id == 0)
                        _window.SetValue(null);

                    var window = (win as Window);

                    if (!_trancport.CompareProperties(EditableTrancport))
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
