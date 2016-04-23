﻿using AIMP_v3._0.View;
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
    public class DictionaryMenuItemViewModel
    {
        public string TableName { get; set; }
        public string Name { get; set; }

        public Command OpenCommand
        {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        var vm = new DictionaryListViewModel(TableName);
                        var view = new DictionaryListView(vm);
                        view.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });
            }
        }
    }
}
