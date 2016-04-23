﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using AIMP_v3._0.DataAccess;
using AIMP_v3._0.View;
using Models.Entities;

namespace AIMP_v3._0.ViewModel.UserRight
{
    public class UserEditViewModel
    {
        public User User { get; set; }
        public ObservableCollection<UserRightViewModel> Rights { get; set; }

        public Command SaveCommand
        {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        var rightIds = Rights.Where(e => e.Enable).Select(y => y.Id);
                        using (var service = new AimpService())
                        {
                            var response = service.SaveUser(User, rightIds);
                            if(response.Error)
                                throw new Exception(response.Message);
                        }
                        MessageBox.Show("Данные сохранены");
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
                    (win as Window).Close();
                });
            }
        }

        public Command DeleteCommand
        {
            get
            {
                return new Command((win) =>
                {
                    if (new QuestClosingView("Удалить пользователя?").ShowDialog() == true)
                    {
                        try
                        {
                            using (var service = new AimpService())
                            {
                                var response = service.DeleteUser(User);
                                if (response.Error)
                                    throw new Exception(response.Message);
                                else
                                {
                                    MessageBox.Show(response.Message);
                                    (win as Window)?.Close();
                                }
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
    }
}
