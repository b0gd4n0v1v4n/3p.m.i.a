using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using AIMP_v3._0.View;
using Aimp.Model.Entities;
using AIMP_v3._0.Aimp.Services;

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
                        using (var service = ServiceClientProvider.GetAimpInfo())
                        {
                            var response = service.SaveUser(User, rightIds);
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
                            using (var service = ServiceClientProvider.GetAimpInfo())
                            {
                                service.DeleteUser(User);
                                MessageBox.Show("ОК.");
                                (win as Window)?.Close();
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
