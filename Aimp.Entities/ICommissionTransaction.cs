namespace Aimp.Entities
{
     public interface ICommissionTransaction : ITransactionProxy
    {
         decimal Commission { get; set; }
         decimal Parking { get; set; }
         bool IsTwoMounth { get; set; }
         bool IsUseCardTrancport { get; set; }
         string Manager { get; set; }
         int? SourceTrancportId { get; set; }
         ISourceTrancport SourceTrancport { get; set; }
         string CountKey { get; set; }
         string ComplectDoc { get; set; }
         decimal PriceDkp { get; set; }
         string Mileage { get; set; } //пробег
    }
}
