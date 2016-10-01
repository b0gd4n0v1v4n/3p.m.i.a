namespace Aimp.Entities
{
    public interface ILegalPerson : IEntity
    {
         string Name { get; set; }
         string Inn { get; set; }
         string Ogrn { get; set; }
         string Kpp { get; set; }
         string Ras_schet { get; set; }
         string Kor_schet { get; set; }
         string Bik { get; set; }
         string Bank { get; set; }
    }
}
