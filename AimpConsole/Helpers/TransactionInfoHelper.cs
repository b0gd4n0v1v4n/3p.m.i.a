using AimpLogic.Logic;
using AimpLogic.Transactions;
using Models.ContractorInfo;
using Models.Entities;
using Models.TrancportInfo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AimpConsole.Helpers
{
    public class TransactionInfoHelper : AimpHelper,IDisposable
    {
        private ITransactionService _logic;
        public TransactionInfoHelper()
        {
            _logic = new TransactionService(User.Login,User.Password);
        }
        public void Dispose()
        {
            _logic?.Dispose();
        }
        public ContractorInfo GetContractorInfo()
        {
            return _logic.GetContractorInfo();
        }
        public TrancportInfo GetTrancportInfo()
        {
            return _logic.GetTrancportInfo();
        }

        public IEnumerable<Contractor> SearchContractors(TypeSearchContractor type,string text)
        {
            switch (type)
            {
                case TypeSearchContractor.LastName:
                    {
                        return _logic.GetContractors().Where(x => x.LastName.Contains(text)).ToList();
                    }
                case TypeSearchContractor.Inn:
                    {
                        return _logic.GetContractors().Where(x => x.LegalPerson.Inn.Contains(text)).ToList();
                    }
                case TypeSearchContractor.Organization:
                    {
                        return _logic.GetContractors().Where(x => x.LegalPerson.Name.Contains(text)).ToList();
                    }
                    case TypeSearchContractor.Empty:
                {
                        return _logic.GetContractors().ToList();
                    }
                default:
                    throw new NotImplementedException($"Not Implemented search for{type}");
            }
        }
        public IEnumerable<Trancport> SearchTrancport(TypeSearchTrancport type, string text)
        {
            switch (type)
            {
                case TypeSearchTrancport.Make:
                    {
                        return _logic.GetTrancports().Where(x => x.Make.Name.Contains(text)).ToList();
                    }
                case TypeSearchTrancport.Model:
                    {
                        return _logic.GetTrancports().Where(x => x.Model.Name.Contains(text)).ToList();
                    }
                case TypeSearchTrancport.Vin:
                    {
                        return _logic.GetTrancports().Where(x => x.Vin.Contains(text)).ToList();
                    }
                case TypeSearchTrancport.Empty:
                    {
                        return _logic.GetTrancports().ToList();
                    }
                default:
                    throw new NotImplementedException($"Not Implemented search for{type}");
            }
        }
        public void SaveContractor(Contractor contractor)
        {
            _logic.SaveContractor(contractor);
        }
        public void SaveTrancport(Trancport trancport)
        {
            _logic.SaveTrancport(trancport);
        }
        public UserFile GetUserFile(int id)
        {
            return _logic.GetUserFile(id);
        }
    }
}
