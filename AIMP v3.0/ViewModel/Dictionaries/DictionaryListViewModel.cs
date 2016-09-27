using AIMP_v3._0.DataAccess;
using AIMP_v3._0.View;
using AIMP_v3._0.ViewModel.Dictionaries;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace AIMP_v3._0.ViewModel
{
    public class DictionaryListViewModel
    {
        private string _tableName;
        private IEnumerable<ColumnViewModel> _columnView;
        public ObservableCollection<EntityViewModel> Rows { get; }
        public DictionaryListViewModel(string tableName,IEnumerable<ColumnViewModel> columnView)
        {
            _columnView = columnView;
            using(var service = new AimpService())
            {
                _tableName = tableName;
                var response = service.GetDictionary(_tableName,columnView.Select(x => x.DbName));
                if (response.Error)
                    throw new Exception(response.Message);
                var rows = new List<EntityViewModel>();
                foreach(var iRow in response.Rows)
                {
                    var cells = new List<CellViewModel>();
                    foreach(var iColumn in columnView)
                    {
                        var cell = new CellViewModel() { ColumnName = iColumn.DbName, Name = iColumn.Name, Value = iRow.Cells[iColumn.Name] };
                    }
                    int id = int.Parse(iRow.Cells["Id"]);
                    var entity = new EntityViewModel(_tableName, cells, id);
                }
                Rows = new ObservableCollection<EntityViewModel>(rows);
            }
        }
        public EntityViewModel CurrentItem { get; set; }

        public Command OpenCurrentItemCommand
        {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        var entitEditVm = new EntityEditViewModel(CurrentItem);
                        var view = new EntityEditView(entitEditVm);
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
                        var vm = new EntityEditViewModel(new EntityViewModel(_tableName,_columnView.Select(c=>new CellViewModel() { ColumnName = c.DbName,Name = c.Name })));
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
