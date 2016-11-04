using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.View;

namespace AIMP_v3._0.ViewModel.Dictionaries
{
   public class EntityEditViewModel
    {
        private string _tableName;
        private int _id;
        public ObservableCollection<CellViewModel> Cells { get; }
        public EntityEditViewModel(EntityViewModel entity,string tableName)
        {
            Cells = new ObservableCollection<CellViewModel>(entity.Cells);
            _tableName = tableName;
            _id = entity.Id;
        }
        public Command SaveCommand
        {
            get
            {
                return new Command(x =>
                {
                    LoadingViewHalper.ShowDialog("Сохранение...", () =>
                    {
                        try
                        {
                            Dictionary<string, string> dictionary = Cells
                            .Select(c => new { c.ColumnName, c.Value })
                            .ToDictionary(d => d.ColumnName, d => d.Value);
                            using (var service = new AimpService())
                            {
                                service.SaveRowValuesDictionary(_tableName, dictionary, _id);
                            }
                            MessageBox.Show("Данные сохранены");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    });
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
                        LoadingViewHalper.ShowDialog("Удаление...", () =>
                        {
                            try
                            {
                                using (var service = new AimpService())
                                {
                                    service.DeleteRowDictionary(_tableName, _id);
                                        (win as Window)?.Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        });
                    }
                });
            }
        }
    }
}
