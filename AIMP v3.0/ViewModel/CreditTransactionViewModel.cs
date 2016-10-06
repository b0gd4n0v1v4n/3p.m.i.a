using Aimp.Entities;
using AIMP_v3._0.Aimp.Services;
using AIMP_v3._0.Extensions;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.User_Control;
using AIMP_v3._0.View;
using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public ICreditTransaction CreditTransaction { get; }
        private ICreditTransaction _transaction;
        public ObservableCollection<PrintItem> PrintedList { get; }

        public CreditTransactionViewModel(ICreditTransaction creditTransaction, IEnumerable<PrintItem> printedList, IEnumerable<ICreditor> creditors, IEnumerable<IRequisit> requisits)
        {
            IsProxy = creditTransaction.Owner != null;
            PrintedList = new ObservableCollection<PrintItem>(printedList);
            creditTransaction.Creditor = creditors.FirstOrDefault(x => x.Id == creditTransaction.Creditor?.Id);
            creditTransaction.Requisit = requisits.FirstOrDefault(x => x.Id == creditTransaction.Requisit?.Id);
            Creditors = new ObservableCollection<ICreditor>(creditors);
            Requisits = new ObservableCollection<IRequisit>(requisits);

            CreditTransaction = creditTransaction;
            _transaction = TinyMapper.Map<ICreditTransaction>(CreditTransaction);
        }
        public ObservableCollection<ICreditor> Creditors { get; private set; }
        public ObservableCollection<IRequisit> Requisits { get; private set; }

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
                            using (var service = ServiceClientProvider.GetCreditTransaction())
                            {
                                var response = service.SaveCreditTransaction(CreditTransaction);
                                
                                CreditTransaction.Id = response.Id;
                                CreditTransaction.Number = response.Number;
                                _transaction = TinyMapper.Map<ICreditTransaction>(CreditTransaction);
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
                                using (var service = ServiceClientProvider.GetCreditTransaction())
                                {
                                    service.DeleteCreditTransaction(CreditTransaction);
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