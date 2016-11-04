using System;
using System.Linq.Expressions;
using Aimp.DataAccess.Interfaces;
using Aimp.Domain;
using Aimp.Logic.Extensions;
using Aimp.Logic.Interfaces;
using Aimp.Model.ContractorInfo;
using Aimp.Model.TrancportInfo;
using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Aimp.Logic.Services
{
    public class TransactionService : ITransactionService
    {
        public void SaveContractor(Contractor contractor)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                if (contractor.Region.Id == 0)
                {
                    var values = new Dictionary<string, string>() {
                        {"Name",contractor.Region.Name }
                    };
                    contractor.RegionId = context.Regions.GetOrAdd(values).Id;
                    contractor.Region = null;
                }
                if (contractor.City.Id == 0)
                {
                    var values = new Dictionary<string, string>() {
                        {"Name",contractor.City.Name }, {"RegionId",contractor.RegionId.ToString() }
                    };
                    contractor.CityId = context.Cities.GetOrAdd(values).Id;
                    contractor.City = null;
                }

                if (contractor.Id != 0)
                {
                    var dbContractor = context.Contractors.Get(contractor.Id, x => x.Document, x => x.Photo);
                    context.UserFileUpdate(contractor.DocumentId, contractor.Document, dbContractor.Document);
                    context.UserFileUpdate(contractor.PhotoId, contractor.Photo, dbContractor.Photo);
                    context.SaveChanges();
                }

                if (contractor.LegalPerson != null)
                    context.LegalPersons.AddOrUpdate(contractor.LegalPerson);

                context.Contractors.AddOrUpdate(contractor);
                context.SaveChanges();
            }
        }
        public void SaveTrancport(Trancport trancport)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                if (trancport.Category?.Id == 0)
                {
                    trancport.Category = context.TrancportCategories.GetOrAdd(new Dictionary<string, string>() { { "Name", trancport.Category.Name } });
                }
                if (trancport.EngineType?.Id == 0)
                {
                    trancport.EngineType = context.EngineTypes.GetOrAdd(new Dictionary<string, string>() { { "Name", trancport.EngineType.Name } });
                }

                if (trancport.Type?.Id == 0)
                {
                    trancport.Type = context.TrancportTypes.GetOrAdd(new Dictionary<string, string>() { { "Name", trancport.Type.Name } });
                }
                if (trancport.Make.Id == 0)
                {
                    var values = new Dictionary<string, string>() {
                        {"Name",trancport.Make.Name }
                    };
                    trancport.MakeId = context.MakesTrancport.GetOrAdd(values, "MakeTrancports").Id;
                    trancport.Make = null;
                }
                if (trancport.Model.Id == 0)
                {
                    var values = new Dictionary<string, string>() {
                        {"Name",trancport.Model.Name }, {"MakeId",trancport.MakeId.ToString() }
                    };
                    trancport.ModelId = context.ModelsTrancport.GetOrAdd(values, "ModelTrancports").Id;
                    trancport.Model = null;
                }

                if (trancport.Id != 0)
                {
                    var dbTrancport = context.Trancports.Get(trancport.Id, x => x.CopyPts);
                    context.UserFileUpdate(trancport.CopyPtsId, trancport.CopyPts, dbTrancport.CopyPts);
                    context.SaveChanges();
                }

                context.Trancports.AddOrUpdate(trancport);

                context.SaveChanges();
            }
        }
        public TrancportInfo GetTrancportInfo()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                return new TrancportInfo()
                {
                    Categories = context.TrancportCategories.All().ToList(),
                    EngineTypes = context.EngineTypes.All().ToList(),
                    Models = context.ModelsTrancport.All().ToList(),
                    Makes = context.MakesTrancport.All().ToList(),
                    Types = context.TrancportTypes.All().ToList()
                };
            }
        }
        public ContractorInfo GetContractorInfo()
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                return new ContractorInfo()
                {
                    Cities = context.Cities.All().ToList(),
                    Regions = context.Regions.All().ToList()
                };
            }
        }
        public IEnumerable<Contractor> GetContractors(Expression<Func<Contractor, bool>> predicate = null)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                if (predicate == null)
                    return context.Contractors
                        .All(x => x.LegalPerson, x => x.City, x => x.Region)
                        .ToList();

                return context.Contractors
                        .All(x => x.LegalPerson, x => x.City, x => x.Region)
                        .Where(predicate)
                        .ToList();
            }
        }
        public IEnumerable<Trancport> GetTrancports(Expression<Func<Trancport, bool>> predicate = null)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                if (predicate == null)
                    return context.Trancports
                        .All(x => x.Make, x => x.Model, x => x.Category, x => x.EngineType, x => x.Type)
                        .ToList();

                return context.Trancports
                        .All(x => x.Make, x => x.Model, x => x.Category, x => x.EngineType, x => x.Type)
                        .Where(predicate)
                        .ToList();
            }
        }
        public UserFile GetUserFile(int id)
        {
            using (var context = IoC.Resolve<IDataContext>())
            {
                return context.UserFiles.Get(id);
            }
        }
    }
}
