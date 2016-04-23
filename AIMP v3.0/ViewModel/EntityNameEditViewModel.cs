
using AIMP_v3._0.DataAccess;
using AIMP_v3._0.View;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AIMP_v3._0.ViewModel
{
   public class EntityNameEditViewModel
    {
        private string _tableName;
        public EntityName EntityName { get; }
        public EntityNameEditViewModel(EntityName entityName,string tableName)
        {
            EntityName = entityName;
            _tableName = tableName;
        }
        public Command SaveCommand
        {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        using (var service = new AimpService())
                        {
                            var response = service.SaveRowDictionary(_tableName,EntityName.Name,EntityName.Id);
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
                                var response = service.DeleteRowDictionary(_tableName,EntityName.Id);
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
