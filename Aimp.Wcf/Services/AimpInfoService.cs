using System;
using System.Collections.Generic;
using Aimp.Entities;
using Aimp.ServiceContracts;
using Aimp.ServiceContracts.AimpInfo;
using Aimp.Infrastructure.IoC;
using Aimp.DataContext;
using System.Linq;

namespace Aimp.Wcf.Services
{
    public class AimpInfoService : IAimpInfoContract
    {
        public void DeleteUser(IUser user)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    var rigths = context.UserRights.All().Where(x => x.UserId == user.Id).Select(x => x.Id).ToArray();
                    context.UserRights.DeleteRange(rigths);
                    context.Users.Delete(user);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public ContractorInfo GetContractorInfo()
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    return new ContractorInfo()
                    {
                        Cities = context.Cities.All().ToList(),
                        Regions = context.Regions.All().ToList()
                    };
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public ITrancport GetTrancport(int id)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public TrancportInfo GetTrancportInfo()
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
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
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public IUserFile GetUserFile(int id)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    return context.UserFiles.Get(id);
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<IUserRight> GetUserRights(int idUser)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    return context.UserRights.All().Where(x => x.UserId == idUser).ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<IUser> GetUsers()
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    return context.Users.All().ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public SaveEntityResult SaveContractor(IContractor contractor)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
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
                    IContractor dbContractor = null;

                    if (contractor.Id != 0)
                        dbContractor = context.Contractors.Get(contractor.Id, x => x.Document, x => x.Photo);

                    UserFileCheck.AddOrUpdate(context, contractor, contractor.Document, dbContractor?.Document);
                    UserFileCheck.AddOrUpdate(context, contractor, contractor.Photo, dbContractor?.Photo);
                    if (contractor.LegalPerson != null)
                        context.LegalPersons.AddOrUpdate(contractor.LegalPerson);

                    context.Contractors.AddOrUpdate(contractor);
                    context.SaveChanges();
                }
                return new SaveEntityResult()
                {
                    Id = contractor.Id
                };
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public SaveEntityResult SaveTrancport(ITrancport trancport)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
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
                        trancport.MakeId = context.Regions.GetOrAdd(values).Id;
                        trancport.Make = null;
                    }
                    if (trancport.Model.Id == 0)
                    {
                        var values = new Dictionary<string, string>() {
                        {"Name",trancport.Model.Name }, {"MakeId",trancport.MakeId.ToString() }
                    };
                        trancport.ModelId = context.Cities.GetOrAdd(values).Id;
                        trancport.Model = null;
                    }
                    ITrancport dbTrancport = null;

                    if (trancport.Id != 0)
                        dbTrancport = context.Trancports.Get(trancport.Id, x => x.CopyPts);

                    UserFileCheck.AddOrUpdate(context, trancport, trancport.CopyPts, dbTrancport?.CopyPts);
                    context.Trancports.AddOrUpdate(trancport);

                    context.SaveChanges();
                }
                return new SaveEntityResult()
                {
                    Id = trancport.Id
                };
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public SaveEntityResult SaveUser(IUser user, IEnumerable<string> rightIds)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    var oldRights = context.UserRights.All().Where(x => x.UserId == user.Id).ToList();
                    if (user.Id != 0)
                    {
                        var deleteIds = oldRights.Where(x => !rightIds.Contains(x.RightId)).Select(x => x.Id).ToArray();
                        context.UserRights.DeleteRange(deleteIds);
                    }

                    context.Users.AddOrUpdate(user);
                    if (user.Id == 0)
                        context.SaveChanges();

                    foreach (string iRight in rightIds)
                    {
                        if (!oldRights.Any(x => x.RightId == iRight))
                        {
                            var userRight = context.UserRights.Create();
                            userRight.UserId = user.Id;
                            userRight.RightId = iRight;
                            context.UserRights.AddOrUpdate(userRight);
                        }
                    }
                    context.SaveChanges();
                }
                return new SaveEntityResult()
                {
                    Id = user.Id
                };
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<IContractor> SearchContractors(TypeSearchContractor type, string text)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    switch (type)
                    {
                        case TypeSearchContractor.LastName:
                            {
                                return context.Contractors
                                              .All(x => x.LegalPerson, x => x.City, x => x.Region)
                                              .Where(x => x.LastName.Contains(text)).ToList();
                            }
                        case TypeSearchContractor.Inn:
                            {
                                return context.Contractors
                                              .All(x => x.LegalPerson, x => x.City, x => x.Region)
                                    .Where(x => x.LegalPerson.Inn.Contains(text)).ToList();
                            }
                        case TypeSearchContractor.Organization:
                            {
                                return context.Contractors
                                              .All(x => x.LegalPerson, x => x.City, x => x.Region)
                                    .Where(x => x.LegalPerson.Name.Contains(text)).ToList();
                            }
                        case TypeSearchContractor.Empty:
                            {
                                return context.Contractors
                                              .All(x => x.LegalPerson, x => x.City, x => x.Region).ToList();
                            }
                        default:
                            throw new NotImplementedException($"Not Implemented search for type: {type}");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }

        public IEnumerable<ITrancport> SearchTrancports(TypeSearchTrancport type, string text)
        {
            try
            {
                using (var context = IoC.Resolve<IAimpContext>())
                {
                    switch (type)
                    {
                        case TypeSearchTrancport.Make:
                            {
                                return context.Trancports.All(x => x.Make, x => x.Model, x => x.Category, x => x.EngineType, x => x.Type)
                                    .Where(x => x.Make.Name.Contains(text)).ToList();
                            }
                        case TypeSearchTrancport.Model:
                            {
                                return context.Trancports.All(x => x.Make, x => x.Model, x => x.Category, x => x.EngineType, x => x.Type)
                                    .Where(x => x.Model.Name.Contains(text)).ToList();
                            }
                        case TypeSearchTrancport.Vin:
                            {
                                return context.Trancports.All(x => x.Make, x => x.Model, x => x.Category, x => x.EngineType, x => x.Type)
                                    .Where(x => x.Vin.Contains(text)).ToList();
                            }
                        case TypeSearchTrancport.Empty:
                            {
                                return context.Trancports.All(x => x.Make, x => x.Model, x => x.Category, x => x.EngineType, x => x.Type)
                                    .ToList();
                            }
                        default:
                            throw new NotImplementedException($"Not Implemented search for type: {type}");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.Logger.Log(ex);
                throw;
            }
        }
    }
}
