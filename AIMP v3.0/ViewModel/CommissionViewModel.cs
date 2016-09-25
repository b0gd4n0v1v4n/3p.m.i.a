using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Extensions;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.User_Control;
using AIMP_v3._0.View;
using AIMP_v3._0.ViewModel;
using Models.Documents;
using Models.Entities;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AIMP_v3._0
{
    public class CommissionViewModel : BaseViewModel
    {
        private bool _Validation()
        {
            if (!CheckValue.Check(Commission.Date))
            {
                MessageBox.Show("Поле 'Дата ДКП' не заполнено");
                return false;
            }

            if (!CheckValue.Check(Commission.Seller))
            {
                MessageBox.Show("Не указан продавец");
                return false;
            }


            if (!CheckValue.Check(Commission.Trancport))
            {
                MessageBox.Show("Не указано транспортное средство");
                return false;
            }

            if (!CheckValue.Check(Commission.Price))
            {
                MessageBox.Show("Поле 'стоимость ТС' не заполнено");
                return false;
            }
            if (Commission.IsUseCardTrancport && !CheckValue.Check(Commission.SourceTrancport))
            {
                MessageBox.Show("Поле 'условаие постановки' не заполнено");
                return false;
            }
            if (IsProxy)
            {
                if (!CheckValue.Check(Commission.NumberProxy))
                {
                    MessageBox.Show("Поле 'номер довереность' не заполнено");
                    return false;
                }

                if (!CheckValue.Check(Commission.NumberRegistry))
                {
                    MessageBox.Show("Поле 'номер в реестре' не заполнено");
                    return false;
                }

                if (!CheckValue.Check(Commission.DateProxy))
                {
                    MessageBox.Show("Поле 'дата довереность' не заполнено");
                    return false;
                }

                if (!CheckValue.Check(Commission.Owner))
                {
                    MessageBox.Show("Не указаг собственник");
                    return false;
                }
            }

            return true;
        }
        public bool IsProxy { get; set; }
        public CommissionDocument Commission { get; }
        private CommissionDocument _commission;
        public ObservableCollection<PrintItem> PrintedList { get; }
        public ObservableCollection<SourceTrancport> SourcesTrancport { get; }
        public CommissionViewModel(CommissionDocument commission, IEnumerable<PrintItem> printedList,IEnumerable<SourceTrancport> sourcesTrancport)
        {
            Commission = commission;
            IsProxy = commission.Owner != null;
            _commission = TinyMapper.Map<CommissionDocument>(Commission);
            PrintedList = new ObservableCollection<PrintItem>(printedList);
            SourcesTrancport = new ObservableCollection<SourceTrancport>(sourcesTrancport);
            Commission.SourceTrancport = sourcesTrancport.FirstOrDefault(x => x.Id == commission.SourceTrancport?.Id);
        }
        public Command SaveChangesCommand
        {
            get
            {
                return new Command((win) =>
                {
                    if (_Validation())
                    {
                        try
                        {
                            if (!IsProxy)
                            {
                                Commission.Owner = null;
                                Commission.DateProxy = null;
                                Commission.NumberProxy = null;
                                Commission.NumberRegistry = null;
                            }
                            using (AimpService service = new AimpService())
                            {
                                var response = service.SaveCommission(Commission);

                                if (response.Error)
                                    throw new Exception(response.Message);
                                Commission.Id = response.Id;
                                Commission.Number = response.Number;
                                if (Commission.IsUseCardTrancport)
                                {
                                    var responseAdd = service.AddCardTrancport(Commission.Id,Commission.Date);
                                    if (responseAdd.Error)
                                        throw new Exception(responseAdd.Message);
                                }
                                else {
                                    var responseDelete = service.DeleteCardTrancport(Commission.Id);
                                    if (responseDelete.Error)
                                        throw new Exception(responseDelete.Message);
                                }
                                
                            }
                            OnPropertyChanged("Commission");
                            _commission = TinyMapper.Map<CommissionDocument>(Commission);
                            MessageBox.Show("Данные успешно сохранены");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                });
            }
        }
        public Command DeleteCommand
        {
            get
            {
                return new Command((win) =>
                {
                    try
                    {
                        if (Commission.Id > 0)
                        {
                            if (new QuestClosingView("Удалить документ?").ShowDialog() == true)
                            {
                                using (AimpService service = new AimpService())
                                {
                                    var response = service.DeleteCommission(Commission);

                                    if (response.Error)
                                        MessageBox.Show(response.Message);
                                }

                                var window = win as Window;

                                if (window != null)
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
        public Command CloseCommand
        {
            get
            {
                return new Command((win) =>
                {
                    var window = (win as Window);

                    if (!_commission.CompareProperties(Commission))
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


