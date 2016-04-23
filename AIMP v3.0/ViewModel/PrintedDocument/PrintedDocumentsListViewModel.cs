using AIMP_v3._0.DataAccess;
using AIMP_v3._0.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AIMP_v3._0.ViewModel.PrintedDocument
{
    public class PrintedDocumentsListViewModel : BaseViewModel
    {
        public ObservableCollection<PrintedDocumentListItemViewModel> ListItems { get; set; }
        public PrintedDocumentListItemViewModel CurrentItem { get; set; }
        public PrintedDocumentsListViewModel()
        {
            using (var service = new AimpService())
            {
                var response = service.GetListPrintDocTemplateDto();
                if (response.Error)
                    throw new Exception(response.Message);
                var lst = response.Items
                    .Select(x => new PrintedDocumentListItemViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Type = x.Type
                    })
                    .OrderBy(x=>x.Type);

                ListItems = new ObservableCollection<PrintedDocumentListItemViewModel>(lst);
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
                        using (var service = new AimpService())
                        {
                            var response = service.GetPrintDocTemplate(CurrentItem.Id);
                            if (response.Error)
                                throw new Exception(response.Message);
                            var vm = new PrintedDocumentEditViewModel(response.Template);
                            var view = new EditPrintedDocumentView(vm);
                            view.ShowDialog();
                        }
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
                        var vm = new PrintedDocumentEditViewModel(new Models.Entities.PrintedDocumentTemplate());
                        var view = new EditPrintedDocumentView(vm);
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
