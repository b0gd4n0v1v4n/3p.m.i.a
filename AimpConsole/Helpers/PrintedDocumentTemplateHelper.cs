using AimpLogic.PrintedDocument;
using Models.Entities;
using Models.PrintDocumentTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimpConsole.Helpers
{
    public class PrintedDocumentTemplateHelper : AimpHelper, IDisposable
    {
        private IPrintedDocumentService _logic;
        public PrintedDocumentTemplateHelper()
        {
            _logic = new PrintedDocumentService(User.Login,User.Password);
        }
        public PrintedDocumentTemplate GetTemplate(int id)
        {
            return _logic.GetTemplate(id);
        }
        public int SaveTemplate(PrintedDocumentTemplate template)
        {
            return _logic.SaveTemplate(template);
        }
        public void DeleteTemplate(PrintedDocumentTemplate template)
        {
            _logic.DeleteTemplate(template);
        }
        public IEnumerable<PrinDocTempListItem> GetList()
        {
            return _logic.GetTemplates()
                .Select(x => new PrinDocTempListItem()
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type
            })
            .ToList();
        }
        public void Dispose()
        {
            _logic?.Dispose();
        }
    }
}
