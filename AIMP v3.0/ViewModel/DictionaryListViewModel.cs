using AIMP_v3._0.DataAccess;
using AIMP_v3._0.View;
using Models;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AIMP_v3._0.ViewModel
{
    public class DictionaryListViewModel
    {
        private string _tableName;
        public ObservableCollection<EntityName> Rows { get; }
        public DictionaryListViewModel(string tableName)
        {
            using(var service = new AimpService())
            {
                _tableName = tableName;
                var response = service.GetDictionary(_tableName);
                if (response.Error)
                    throw new Exception(response.Message);
                Rows = new ObservableCollection<EntityName>(response.Items);
            }
        }
        public EntityName CurrentItem { get; set; }

        public Command OpenCurrentItemCommand
        {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        var vm = new EntityNameEditViewModel(CurrentItem,_tableName);
                        var view = new EntityNameEditView(vm);
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
                        var vm = new EntityNameEditViewModel(new EntityName(),_tableName);
                        var view = new EntityNameEditView(vm);
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
