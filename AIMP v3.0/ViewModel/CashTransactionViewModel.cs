using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.View;
using Models.Documents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using AIMP_v3._0.Enums;
using AIMP_v3._0.Extensions;
using AIMP_v3._0.User_Control;
using Models.Entities;
using Nelibur.ObjectMapper;

namespace AIMP_v3._0.ViewModel
{
    public class CashTransactionViewModel : BaseViewModel
    {
        private bool _Validation()
        {
            if (!CheckValue.Check(CashTransaction.Date))
            {
                MessageBox.Show("Поле 'Дата ДКП' не заполнено");
                return false;
            }

            if (!CheckValue.Check(CashTransaction.Seller))
            {
                MessageBox.Show("Не указан продавец");
                return false;
            }

            if (!CheckValue.Check(CashTransaction.Buyer))
            {
                MessageBox.Show("Не указан покупатель");
                return false;
            }

            if (!CheckValue.Check(CashTransaction.Trancport))
            {
                MessageBox.Show("Не указано транспортное средство");
                return false;
            }

            if (!CheckValue.Check(CashTransaction.Price))
            {
                MessageBox.Show("Поле 'стоимость ТС' не заполнено");
                return false;
            }

            if (IsProxy)
            {
                if (!CheckValue.Check(CashTransaction.NumberProxy))
                {
                    MessageBox.Show("Поле 'номер довереность' не заполнено");
                    return false;
                }

                if (!CheckValue.Check(CashTransaction.NumberRegistry))
                {
                    MessageBox.Show("Поле 'номер в реестре' не заполнено");
                    return false;
                }

                if (!CheckValue.Check(CashTransaction.DateProxy))
                {
                    MessageBox.Show("Поле 'дата довереность' не заполнено");
                    return false;
                }

                if (!CheckValue.Check(CashTransaction.Owner))
                {
                    MessageBox.Show("Не указаг собственник");
                    return false;
                }
            }

            return true;
        }
        public bool IsProxy { get; set; }
        public CashTransactionDocument CashTransaction { get; }
        private CashTransactionDocument _transaction;
        public ObservableCollection<PrintItem> PrintedList { get; }
        public CashTransactionViewModel(CashTransactionDocument cashTransaction, IEnumerable<PrintItem> printedList)
        {
            CashTransaction = cashTransaction;
            IsProxy = cashTransaction.Owner != null;
            _transaction = TinyMapper.Map<CashTransactionDocument>(cashTransaction);
            PrintedList = new ObservableCollection<PrintItem>(printedList);
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
                                CashTransaction.Owner = null;
                                CashTransaction.DateProxy = null;
                                CashTransaction.NumberProxy = null;
                                CashTransaction.NumberRegistry = null;
                            }
                            using (AimpService service = new AimpService())
                            {
                                var response = service.SaveCashTransaction(CashTransaction);

                                if (response.Error)
                                    throw new Exception(response.Message);

                                CashTransaction.Id = response.Id;
                                CashTransaction.Number = response.Number;
                                _transaction = TinyMapper.Map<CashTransactionDocument>(CashTransaction);
                                OnPropertyChanged("CashTransaction");
                                MessageBox.Show(response.Message);
                            }
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
                        if (CashTransaction.Id > 0)
                        {
                            if (new QuestClosingView("Удалить документ?").ShowDialog() == true)
                            {
                                using (AimpService service = new AimpService())
                                {
                                    var response = service.DeleteCashTransaction(CashTransaction);

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

                    if (!_transaction.CompareProperties(CashTransaction))
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
