using System;

namespace Aimp.Entities
{
     public interface IContractor : IEntity
    {
         string FirstName { get; set; }
         string LastName { get; set; }
         string MiddleName { get; set; }
         string FirstNameGenitive { get; set; }
         string LastNameGenitive { get; set; }
         string MiddleNameGenitive { get; set; }
         DateTime DateBirth { get; set; }
         int RegionId { get; set; }
         IRegion Region { get; set; }
         int CityId { get; set; }
         ICity City { get; set; }
         string Raion { get; set; }
         string Street { get; set; }
         string House { get; set; }
         string Housing { get; set; }
         string Apartment { get; set; }
         string Telefon { get; set; }
         string NumberDocument { get; set; }
         string SerialDocument { get; set; }
         string ByDocument { get; set; }
         DateTime? DateDocument { get; set; }
         int? DocumentId { get; set; }
         IUserFile Document { get; set; }
         int? PhotoId { get; set; }
         IUserFile Photo { get; set; }
         int? LegalPersonId { get; set; }
         ILegalPerson LegalPerson { get; set; }
    }
}
