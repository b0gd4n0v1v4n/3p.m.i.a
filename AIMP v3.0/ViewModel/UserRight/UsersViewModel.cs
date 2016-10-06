using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using AIMP_v3._0.View;
using AIMP_v3._0.Aimp.Services;
using Aimp.Model.Rights;
using Aimp.Model.Entities;

namespace AIMP_v3._0.ViewModel.UserRight
{
    public class UsersViewModel
    {
        public ObservableCollection<UserListItem> UserRightList { get; set; }
        public UserListItem CurrentItem { get; set; }
        public UsersViewModel()
        {
            using (var service = ServiceClientProvider.GetAimpInfo())
            {
                var response = service.GetUsers();
                var lst = response
                    .Select(x => new UserListItem()
                    {
                        User = x
                    });

                UserRightList = new ObservableCollection<UserListItem>(lst);
            }
        }

        public Command OpenCurrentItemCommand
        {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        IEnumerable<UserRightViewModel> rigthsDb = null;
                        using (var service = ServiceClientProvider.GetAimpInfo())
                        {
                            var response = service.GetUserRights(CurrentItem.User.Id);
                            rigthsDb = response.Select(IRight => new UserRightViewModel()
                            {
                                BaseId = IRight.Id,
                                Id = IRight.RightId
                            });
                        }
                        var rights = from rStatic in RightsCollection.Items
                                     join rDb in rigthsDb
                                         on rStatic.Id equals rDb.Id
                                         into rDbDefault
                                     from rDb in rDbDefault.DefaultIfEmpty()
                                     select new UserRightViewModel()
                                     {
                                         Id = rStatic.Id,
                                         Name = rStatic.Name,
                                         BaseId = rDb?.BaseId,
                                         Enable = rDb != null
                                     };
                        UserView view = new UserView(new UserEditViewModel()
                        {
                            User = CurrentItem.User,
                            Rights = new ObservableCollection<UserRightViewModel>(rights)
                        });
                        view.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });
            }
        }
        public Command NewCommand
        {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        var rights = RightsCollection.Items.Select(y => new UserRightViewModel()
                        {
                            Id = y.Id,
                            Name = y.Name
                        });
                        UserView view = new UserView(new UserEditViewModel()
                        {
                            User = new User(),
                            Rights = new ObservableCollection<UserRightViewModel>(rights)
                        });
                        view.ShowDialog();
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
    }
}
