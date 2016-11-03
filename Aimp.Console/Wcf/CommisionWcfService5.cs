using System;
using System.Collections.Generic;
using System.Linq;
using Aimp.Domain;
using Aimp.Logic.Interfaces;
using Aimp.Model;
using Aimp.Model.Commission;
using Aimp.Model.Documents;
using Aimp.ServiceContract.Services;
using Entities;

namespace Aimp.Console.Wcf
{
    public class CommisionWcfService5 :CreditTransactionWcfService4, ICommisionWcfService
    {
        public IEnumerable<CommissionListItem> GetCommissions()
        {
            EventLog("Get commissions");
            try
            {
                return IoC.Resolve<ICommissionService>()
                    .GetCommissions(CurrentUser)
                    .Select(x => new CommissionListItem()
                {
                    Id = x.Id,
                    SellerFullName = x.Seller.LegalPerson != null ? x.Seller.LegalPerson.Name : x.Seller.LastName + " " + x.Seller.FirstName + " " + x.Seller.MiddleName,
                    Date = x.Date,
                    DocumentSellerId = x.Seller.Document.Id,
                    Number = x.Number.ToString(),
                    NumberProxy = x.NumberProxy,
                    TrancportFullName = x.Trancport.Model.Name + ", " + x.Trancport.Make.Name,
                    PtsId = x.Trancport.CopyPts.Id,
                    Parking = x.Parking.ToString(),
                    Commission = x.Commission.ToString()

                }).OrderByDescending(x => new { x.Date, x.Number }).ToList();
            }
            catch (Exception ex) 
            {
                Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<SourceTrancport> GetSourceTrancport()
        {
            EventLog($"Get source trancports");
            try
            {
                return IoC.Resolve<ICommissionService>().GetSourcesTrancport().ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public CommissionDto GetCommission(int id)
        {
            EventLog($"Get commission id: {id}")
            ;
            
            try
            {
                var service = IoC.Resolve<ICommissionService>();

                var document = service.GetDocument(id);
                var printedDocuments =
                    service.GetPrintedDocumentTemplates()
                        .ToList()
                        .Select(x => new KeyValue<string, string>()
                        {
                            Key = x.Type,
                            Value = x.Name
                        });
                return new CommissionDto()
                {
                    Document = document,
                    PrintedDocuments = printedDocuments
                };
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public int SaveCommission(CommissionDocument document)
        {
            EventLog($"Save commission document id:{document.Id}");
            try
            {
                 IoC.Resolve<ICommissionService>().SaveDocument(document);
                return document.Id;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public void DeleteCommission(CommissionDocument document)
        {
            EventLog($"Delete commission document id: {document.Id}");
            try
            {
                IoC.Resolve<ICommissionService>().DeleteDocument(document);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }
    }
}
