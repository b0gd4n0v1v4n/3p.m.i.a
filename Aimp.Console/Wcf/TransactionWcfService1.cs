
using System;
using System.Collections.Generic;
using System.Linq;
using Aimp.Domain;
using Aimp.Logic.Interfaces;
using Aimp.Model.ContractorInfo;
using Aimp.Model.TrancportInfo;
using Aimp.ServiceContract.Services;
using Entities;

namespace Aimp.Console.Wcf
{
    public class TransactionWcfService1 : WcfServiceBase,ITransactionWcfService
    {
        public TrancportInfo GetTrancportInfo()
        {
            EventLog("Get trancport info");

            try
            {
                return IoC.Resolve<ITransactionService>().GetTrancportInfo();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public ContractorInfo GetContractorInfo()
        {
            EventLog("Get contractor info");

            try
            {
                return IoC.Resolve<ITransactionService>().GetContractorInfo();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<Contractor> SearchContractors(TypeSearchContractor type, string text)
        {
            EventLog($"Search contractors type: {type}, search text: {text}");
            try
            {
                switch (type)
                {
                    case TypeSearchContractor.LastName:
                        {
                            return IoC.Resolve<ITransactionService>().GetContractors(x => x.LastName.Contains(text));
                        }
                    case TypeSearchContractor.Inn:
                        {
                            return IoC.Resolve<ITransactionService>().GetContractors(x => x.LegalPerson != null && x.LegalPerson.Inn.Contains(text));
                        }
                    case TypeSearchContractor.Organization:
                        {
                            return IoC.Resolve<ITransactionService>().GetContractors(x => x.LegalPerson != null && x.LegalPerson.Name.Contains(text));
                        }
                    case TypeSearchContractor.Empty:
                        {
                            return IoC.Resolve<ITransactionService>().GetContractors();
                        }
                    default:
                        throw new NotImplementedException("Not Implemented search for{type}");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<Trancport> SearchTrancports(TypeSearchTrancport type, string text)
        {
            EventLog($"Search trancports type: {type}, search text: {text}");
            try
            {
                switch (type)
            {
                case TypeSearchTrancport.Make:
                    {
                        return IoC.Resolve<ITransactionService>().GetTrancports(x => x.Make.Name.Contains(text));
                    }
                case TypeSearchTrancport.Model:
                    {
                        return IoC.Resolve<ITransactionService>().GetTrancports(x => x.Model.Name.Contains(text));
                    }
                case TypeSearchTrancport.Vin:
                    {
                        return IoC.Resolve<ITransactionService>().GetTrancports(x => x.Vin.Contains(text));
                    }
                case TypeSearchTrancport.Empty:
                    {
                        return IoC.Resolve<ITransactionService>().GetTrancports();
                    }
                default:
                    throw new NotImplementedException("Not Implemented search for{type}");
            }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public int SaveContractor(Contractor contractor)
        {
            EventLog($"Save contractor id: {contractor.Id}");
            try
            {
                IoC.Resolve<ITransactionService>().SaveContractor(contractor);
                return contractor.Id;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public int SaveTrancport(Trancport trancport)
        {
           EventLog($"Save trancport id: {trancport.Id}");
            try
            {
                IoC.Resolve<ITransactionService>().SaveTrancport(trancport);
                return trancport.Id;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }

        public UserFile GetUserFile(int id)
        {
            EventLog($"Get user file id: {id}");
            try
            {
                return IoC.Resolve<ITransactionService>().GetUserFile(id);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                throw;
            }
        }
    }
}
