using AIMP_v3._0.Helpers;
using AIMP_v3._0.View;
using AIMP_v3._0.ViewModel.Dictionaries;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AIMP_v3._0.ViewModel
{
    public class DictionaryMenuItemViewModel
    {
        public string TableName { get; set; }
        public string Name { get; set; }
        public IEnumerable<ColumnViewModel> Columns { get; set; }
        public Command OpenCommand
        {
            get
            {
                return new Command(x =>
                {
                    LoadingViewHalper.ShowDialog("Загрузка...", () =>
                    {
                        try
                        {
                            var vm = new DictionaryListViewModel(TableName, Columns);
                            var view = new DictionaryListView(vm);
                            view.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    });
                });
            }
        }
    }
}
