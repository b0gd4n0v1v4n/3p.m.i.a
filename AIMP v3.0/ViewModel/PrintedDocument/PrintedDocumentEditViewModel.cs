﻿using Aimp.Entities;
using Aimp.Model.Entities;
using Aimp.Model.PrintedDocument;
using AIMP_v3._0.Aimp.Services;
using AIMP_v3._0.DataAccess;
using AIMP_v3._0.Helpers;
using AIMP_v3._0.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace AIMP_v3._0.ViewModel.PrintedDocument
{
    public class PrintedDocumentEditViewModel: BaseViewModel
    {
        public IPrintedDocumentTemplate Template { get; }
        public ObservableCollection<string> TypeList { get; }
        public PrintedDocumentEditViewModel(IPrintedDocumentTemplate template)
        {
            Template = template;
            TypeList = new ObservableCollection<string>()
            {
                PrintedDocumentTemplateType.Акт.ToString(),
                PrintedDocumentTemplateType.Дкп.ToString(),
                PrintedDocumentTemplateType.Комиссия.ToString(),
                PrintedDocumentTemplateType.Сделка.ToString(),
                PrintedDocumentTemplateType.Кредит.ToString()

            };
        }
        public Command AddFileCommand
        {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        using(OpenFileDialog dialg = new OpenFileDialog())
                        {
                            dialg.Filter = "(*.doc ; *.docs ; *.docx) | *.doc; *.docs; *.docx";
                            if(dialg.ShowDialog() == DialogResult.OK)
                            {
                                Template.FileName = dialg.FileName;
                                Template.File = File.ReadAllBytes(dialg.FileName);
                                OnPropertyChanged("Template.FileName");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message);
                    }
                });
            }
        }
        public Command OpenFileCommand
        {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        if (Template?.File != null)
                            OpenUserFile.Open(Template.FileName, Template.File);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message);
                    }
                });
            }
        }
        public Command SaveCommand
        {
            get
            {
                return new Command(x =>
                {
                    try
                    {
                        if (Template.File == null)
                            throw new Exception("Шаблон не выбран");
                        using (var service = ServiceClientProvider.GetPrintedDocument())
                        {
                            service.SavePrintedDocTemplate(Template);
                        }
                        System.Windows.MessageBox.Show("Данные сохранены");
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message);
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
                            using (var service = ServiceClientProvider.GetPrintedDocument())
                            {
                                service.DeletePrintedDocTemplate(Template);
                                    System.Windows.MessageBox.Show("ОК");
                                    (win as Window)?.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(ex.Message);
                        }
                    }
                });
            }
        }
    }
}
