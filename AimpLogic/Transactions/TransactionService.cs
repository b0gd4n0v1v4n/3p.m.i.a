using AimpLogic.Logging;
using AimpLogic.Logic;
using AimpLogic.UserRights;
using Models.ContractorInfo;
using Models.Entities;
using Models.TrancportInfo;
using System;
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

                var sRegion = Context.Regions.All().FirstOrDefault(x => x.Name == contractor.Region.Name);
                if (sRegion == null)
                {
                    Context.Regions.AddOrUpdate(contractor.Region);
                    Context.SaveChanges();
                    contractor.City.RegionId = contractor.Region.Id;
                    Context.Cities.AddOrUpdate(contractor.City);
                    Context.SaveChanges();
                }
                else
                {
                    contractor.Region = sRegion;
                    var sCity = Context.Cities.All().FirstOrDefault(x => x.RegionId == sRegion.Id && x.Name == contractor.City.Name);                    
                    if(sCity == null)
                    {
                        contractor.City.RegionId = sRegion.Id;
                        Context.Cities.AddOrUpdate(contractor.City);
                        Context.SaveChanges();
                    }
                    else
                    {
                        contractor.City = sCity;
                    }
                }
                //if (contractor.Region.Id == 0)
                //{
                //    Context.Cities.AddOrUpdate(contractor.City);
                //    contractor.Region = contractor.City.Region;
                //}
                //if (contractor.City.Id == 0 && contractor.Region.Id > 0)
                //{
                //    Context.Cities.AddOrUpdate(contractor.City);
                //}
                //if (contractor.Photo?.Id == 0)
                //{
                //    Context.UserFiles.AddOrUpdate(contractor.Photo);
                //}
                //if (contractor.Document?.Id == 0)
                //{
                //    Context.UserFiles.AddOrUpdate(contractor.Document);
                //}
                if (contractor.LegalPerson != null)
                    Context.LegalPersons.AddOrUpdate(contractor.LegalPerson);
                //Context.SaveChanges();
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
                    Context.TrancportCategories.AddOrUpdate(trancport.Category);
                if (trancport.EngineType?.Id == 0)
                    Context.EngineTypes.AddOrUpdate(trancport.EngineType);
                if (trancport.Make?.Id == 0)
                    Context.MakesTrancport.AddOrUpdate(trancport.Make);
                if (trancport.Model?.Id == 0 && trancport.Make?.Id != 0)
                {
                    Context.ModelsTrancport.AddOrUpdate(trancport.Model);
                    trancport.Make = trancport.Model.Make;
                }
                if (trancport.Make?.Id == null)
                {
                    Context.ModelsTrancport.AddOrUpdate(trancport.Model);
                    trancport.Make = trancport.Model.Make;
                }
                if (trancport.Type?.Id == null)
                    Context.TrancportTypes.AddOrUpdate(trancport.Type);
                if (trancport.CopyPts?.Id == 0)
                {
                    Context.UserFiles.AddOrUpdate(trancport.CopyPts);
                }
                Context.SaveChanges();
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
