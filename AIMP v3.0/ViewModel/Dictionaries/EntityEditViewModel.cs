
using AIMP_v3._0.DataAccess;
using AIMP_v3._0.View;
using AIMP_v3._0.ViewModel.Dictionaries;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AIMP_v3._0.ViewModel
{
   public class EntityEditViewModel
    {
        private string _tableName;
        private int _id;
        public ObservableCollection<CellViewModel> Cells { get; }
        public EntityEditViewModel(EntityViewModel entity)
        {
            Cells = new ObservableCollection<CellViewModel>(entity.Cells);
            _tableName = entity.Name;
            _id = entity.Id;
        }
        public Command SaveCommand
        {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        Dictionary<string, string> dictionary = Cells
                        .Select(c => new { c.ColumnName,c.Value })
                        .ToDictionary(d=>d.ColumnName,d=>d.Value);
                        using (var service = new AimpService())
                        {
                            var response = service.SaveRowValuesDictionary(_tableName,dictionary,_id);
                            if (response.Error)
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
                    if (new QuestClosingView("Удалить?").ShowDialog() == true)
                    {
                        try
                        {
                            using (var service = new AimpService())
                            {
                                var response = service.DeleteRowDictionary(_tableName,_id);
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
