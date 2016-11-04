using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using AIMP_v3._0.DataAccess;
using AIMP_v3._0.View;

namespace AIMP_v3._0.ViewModel.Dictionaries
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

                var rows = new List<EntityViewModel>();
                foreach(var iRow in response)
                {
                    var cells = new List<CellViewModel>();
                    var name = string.Empty;
                    foreach(var iColumn in columnView)
                    {
                        var value = iRow.Cells.First(x => x.Key == iColumn.DbName).Value;
                        if (iColumn.DbName == "Name")
                            name = value;
                        var cell = new CellViewModel() { ColumnName = iColumn.DbName, Name = iColumn.Name, Value = value};
                        cells.Add(cell);
                    }
                    int id = int.Parse(iRow.Cells.First(x=>x.Key == "Id").Value);
                    var entity = new EntityViewModel(name, cells, id);
                    rows.Add(entity);
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
                        var entitEditVm = new EntityEditViewModel(CurrentItem, _tableName);
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
                        var vm = new EntityEditViewModel(new EntityViewModel(_tableName,_columnView.Select(c=>new CellViewModel() { ColumnName = c.DbName,Name = c.Name })), _tableName);
                        var view = new EntityEditView(vm);
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
