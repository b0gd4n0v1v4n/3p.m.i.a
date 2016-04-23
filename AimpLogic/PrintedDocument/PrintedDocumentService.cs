using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;
using AimpLogic.Logic;
using AimpLogic.UserRights;
using AimpLogic.Logging;

namespace AimpLogic.PrintedDocument
{
    public class PrintedDocumentService : Aimp,IPrintedDocumentService
    {
        public PrintedDocumentService(string login, string password) 
            :base(login,password)
        {

        }
        public void DeleteTemplate(PrintedDocumentTemplate template)
        {
            
            try
            {
                CheckDeleteRight();
                Context.PrintedDocumentTemplates.Delete(template);
                Context.SaveChanges();
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);
                throw new Exception("Не удалось удалить печатную форму, обратитесь к администратору");
            }
        }

        public PrintedDocumentTemplate GetTemplate(int id)
        {
             try
            {
                CheckViewRight();
                return Context.PrintedDocumentTemplates.Get(id);
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);
                throw new Exception("Не удалось получить печатную форму, обратитесь к администратору");
            }
        }

        public IQueryable<PrintedDocumentTemplate> GetTemplates()
        {
            try
            {
                CheckViewRight();
                return Context.PrintedDocumentTemplates.All();
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);
                throw new Exception("Не удалось получить список, обратитесь к администратору");
            }
        }

        public int SaveTemplate(PrintedDocumentTemplate template)
        {
            try
            {
                CheckAddRight();
                Context.PrintedDocumentTemplates.AddOrUpdate(template);
                Context.SaveChanges();
                return template.Id;
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);
                throw new Exception("Не удалось сохранить печатную форму, обратитесь к администратору");
            }
        }
    }
}
