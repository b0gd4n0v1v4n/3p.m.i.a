using AimpLogic.Extensions;
using AimpLogic.Logging;
using AimpLogic.Logic;
using AimpLogic.UserRights;
using Models.ContractorInfo;
using Models.Entities;
using Models.TrancportInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AimpLogic.Transactions
{
    public class TransactionService : Aimp, ITransactionService
    {
        public TransactionService(string login, string password)
            : base(login, password)
        { }
        public void SaveContractor(Contractor contractor)
        {
            try
            {
                CheckAddRight();

                if(contractor.Region.Id == 0)
                {
                    var values = new Dictionary<string, string>() {
                        {"Name",contractor.Region.Name }
                    };
                    contractor.RegionId = Context.Regions.GetOrAdd(values).Id;
                    contractor.Region = null;
                }
                if(contractor.City.Id == 0)
                {
                    var values = new Dictionary<string, string>() {
                        {"Name",contractor.City.Name }, {"RegionId",contractor.RegionId.ToString() }
                    };
                    contractor.CityId = Context.Cities.GetOrAdd(values).Id;
                    contractor.City = null;
                }

                if(contractor.Id != 0)
                {
                    var dbContractor = Context.Contractors.Get(contractor.Id, x => x.Document, x => x.Photo);
                    Context.UserFileUpdate(contractor.DocumentId, contractor.Document, dbContractor.Document);
                    Context.UserFileUpdate(contractor.PhotoId, contractor.Photo, dbContractor.Photo);
                    Context.SaveChanges();
                }

                if (contractor.LegalPerson != null)
                    Context.LegalPersons.AddOrUpdate(contractor.LegalPerson);
                
                Context.Contractors.AddOrUpdate(contractor);
                Context.SaveChanges();
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось сохранить контрагента, обратитесь к администратору");
            }
        }
        public void SaveTrancport(Trancport trancport)
        {
            try
            {
                CheckAddRight();

                if (trancport.Category?.Id == 0)
                {
                    trancport.Category = Context.TrancportCategories.GetOrAdd(new Dictionary<string, string>() { { "Name", trancport.Category.Name } });
                }
                if(trancport.EngineType?.Id == 0)
                {
                    trancport.EngineType = Context.EngineTypes.GetOrAdd(new Dictionary<string, string>() { { "Name", trancport.EngineType.Name } });
                }

                if (trancport.Type?.Id == 0)
                {
                    trancport.Type = Context.TrancportTypes.GetOrAdd(new Dictionary<string, string>() { { "Name", trancport.Type.Name } });
                }
                if (trancport.Make.Id == 0)
                {
                    var values = new Dictionary<string, string>() {
                        {"Name",trancport.Make.Name }
                    };
                    trancport.MakeId = Context.Regions.GetOrAdd(values).Id;
                    trancport.Make = null;
                }
                if (trancport.Model.Id == 0)
                {
                    var values = new Dictionary<string, string>() {
                        {"Name",trancport.Model.Name }, {"MakeId",trancport.MakeId.ToString() }
                    };
                    trancport.ModelId = Context.Cities.GetOrAdd(values).Id;
                    trancport.Model = null;
                }

                if (trancport.Id != 0)
                {
                    var dbTrancport = Context.Trancports.Get(trancport.Id, x => x.CopyPts);
                    Context.UserFileUpdate(trancport.CopyPtsId,trancport.CopyPts, dbTrancport.CopyPts);
                    Context.SaveChanges();
                }
                
                Context.Trancports.AddOrUpdate(trancport);
   
                Context.SaveChanges();
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log("SaveTrancport", ex);

                throw new Exception("Неудалось сохранить транспорт, обратитесь к администратору");
            }
        }
        public TrancportInfo GetTrancportInfo()
        {
            try
            {
                return new TrancportInfo()
                {
                    Categories = Context.TrancportCategories.All().ToList(),
                    EngineTypes = Context.EngineTypes.All().ToList(),
                    Models = Context.ModelsTrancport.All().ToList(),
                    Makes = Context.MakesTrancport.All().ToList(),
                    Types = Context.TrancportTypes.All().ToList()
                };
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось получить информацию по ТС, обратитесь к администратору");
            }
        }
        public ContractorInfo GetContractorInfo()
        {
            try
            {
                return new ContractorInfo()
                {
                    Cities = Context.Cities.All().ToList(),
                    Regions = Context.Regions.All().ToList()
                };
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось получить информацию по контрагенту, обратитесь к администратору");
            }
        }
        public IQueryable<Contractor> GetContractors()
        {
            try
            {
                CheckViewRight();

                return Context.Contractors
                    .All(x => x.LegalPerson, x => x.City, x => x.Region);
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log("GetContractors", ex);

                throw new Exception("Не удалось получить список контрагентов, обратитесь к администратору");
            }
        }
        public IQueryable<Trancport> GetTrancports()
        {
            try
            {
                CheckViewRight();

                return Context.Trancports.All(x => x.Make, x => x.Model, x => x.Category, x => x.EngineType, x => x.Type);
            }
            catch (AccessDeniedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Не удалось получить список ТС, обратитесь к администратору");
            }
        }
        public UserFile GetUserFile(int id)
        {
            try
            {
                return Context.UserFiles.Get(id);
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);

                throw new Exception("Неудалось получить искомый файл, обратитесь к администратору");
            }
        }
    }
}
