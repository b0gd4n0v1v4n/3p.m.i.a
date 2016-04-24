using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Extensions;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.User_Control;
using AIMP_v3._0.View;
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

namespace AIMP_v3._0.ViewModel
{
    public class CreditTransactionViewModel : BaseViewModel
    {
        private bool _Validation()
        {
            if (!CheckValue.Check(CreditTransaction.Date))
            {
                MessageBox.Show("Поле 'Дата ДКП' не заполнено");
                return false;
            }

            if (!CheckValue.Check(CreditTransaction.Seller))
            {
                MessageBox.Show("Не указан продавец");
                return false;
            }

            if (!CheckValue.Check(CreditTransaction.Buyer))
            {
                MessageBox.Show("Не указан покупатель");
                return false;
            }

            if (!CheckValue.Check(CreditTransaction.Trancport))
            {
                MessageBox.Show("Не указано транспортное средство");
                return false;
            }

            if (!CheckValue.Check(CreditTransaction.Price))
            {
                MessageBox.Show("Поле 'стоимость ТС' не заполнено");
                return false;
            }
            if (!CheckValue.Check(CreditTransaction.Creditor))
            {
                MessageBox.Show("Поле ' кредитор' не заполнено");
                return false;
            }
            if (!CheckValue.Check(CreditTransaction.Requisit))
            {
                MessageBox.Show("Поле 'реквезиты' не заполнено");
                return false;
            }
            if (IsProxy)
            {
                if (!CheckValue.Check(CreditTransaction.NumberProxy))
                {
                    MessageBox.Show("Поле 'номер довереность' не заполнено");
                    return false;
                }

                if (!CheckValue.Check(CreditTransaction.NumberRegistry))
                {
                    MessageBox.Show("Поле 'номер в реестре' не заполнено");
                    return false;
                }

                if (!CheckValue.Check(CreditTransaction.DateProxy))
                {
                    MessageBox.Show("Поле 'дата довереность' не заполнено");
                    return false;
                }

                if (!CheckValue.Check(CreditTransaction.Owner))
                {
                    MessageBox.Show("Не указаг собственник");
                    return false;
                }
            }

            return true;
        }
        public bool IsProxy { get; set; }
        public CreditTransactionDocument CreditTransaction { get; }
        private CreditTransactionDocument _transaction;
        public ObservableCollection<PrintItem> PrintedList { get; }

        public CreditTransactionViewModel(CreditTransactionDocument creditTransaction, IEnumerable<PrintItem> printedList, IEnumerable<Creditor> creditors, IEnumerable<Requisit> requisits)
        {
            IsProxy = creditTransaction.Owner != null;
            PrintedList = new ObservableCollection<PrintItem>(printedList);
            creditTransaction.Creditor = creditors.FirstOrDefault(x => x.Id == creditTransaction.Creditor?.Id);
            creditTransaction.Requisit = requisits.FirstOrDefault(x => x.Id == creditTransaction.Requisit?.Id);
            Creditors = new ObservableCollection<Creditor>(creditors);
            Requisits = new ObservableCollection<Requisit>(requisits);

            CreditTransaction = creditTransaction;
            _transaction = TinyMapper.Map<CreditTransactionDocument>(CreditTransaction);
        }
        public ObservableCollection<Creditor> Creditors { get; private set; }
        public ObservableCollection<Requisit> Requisits { get; private set; }

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
                                CreditTransaction.Owner = null;
                                CreditTransaction.DateProxy = null;
                                CreditTransaction.NumberProxy = null;
                                CreditTransaction.NumberRegistry = null;
                            }
                            using (AimpService service = new AimpService())
                            {
                                var response = service.SaveCreditTransaction(CreditTransaction);

                                if (response.Error)
                                    throw new Exception(response.Message);
                                CreditTransaction.Id = response.Id;
                                CreditTransaction.Number = response.Number;
                                _transaction = TinyMapper.Map<CreditTransactionDocument>(CreditTransaction);
                                OnPropertyChanged("CreditTransaction");
                            }
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
                        if (CreditTransaction.Id > 0)
                        {
                            if (new QuestClosingView("Удалить документ?").ShowDialog() == true)
                            {
                                using (AimpService service = new AimpService())
                                {
                                    var response = service.DeleteCreditTransaction(CreditTransaction);

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

                    if (!_transaction.CompareProperties(CreditTransaction))
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