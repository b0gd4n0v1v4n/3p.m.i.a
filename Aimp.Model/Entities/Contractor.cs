using Aimp.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aimp.Model.Entities
{
    public class Contractor : Entity, IContractor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FirstNameGenitive { get; set; }
        public string LastNameGenitive { get; set; }
        public string MiddleNameGenitive { get; set; }
        public DateTime DateBirth { get; set; }
        [ForeignKey("Region")]
        public int RegionId { get; set; }
        public virtual IRegion Region { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }
        public virtual ICity City { get; set; }
        public string Raion { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Housing { get; set; }
        public string Apartment { get; set; }
        public string Telefon { get; set; }
        public string NumberDocument { get; set; }
        public string SerialDocument { get; set; }
        public string ByDocument { get; set; }
        public DateTime? DateDocument { get; set; }
        [ForeignKey("Document")]
        public int? DocumentId { get; set; }
        public virtual IUserFile Document { get; set; }
        [ForeignKey("Photo")]
        public int? PhotoId { get; set; }
        public virtual IUserFile Photo { get; set; }
        [ForeignKey("LegalPerson")]
        public int? LegalPersonId { get; set; }
        public virtual ILegalPerson LegalPerson { get; set; }
    }
}
