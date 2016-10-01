using System;

namespace Aimp.Entities
{
     public interface ITrancport : IEntity
    {
         string Number { get; set; }
         int MakeId { get; set; }
         IMakeTrancport Make { get; set; }
         int ModelId { get; set; }
         IModelTrancport Model { get; set; }
         int CategoryId { get; set; }
         ITrancportCategory Category { get; set; }
         int TypeId { get; set; }
         ITrancportType Type { get; set; }
         string Maker { get; set; }
         short Year { get; set; }
         string Color { get; set; }
         string Vin { get; set; }
         string BodyNumber { get; set; }
         string ChassisNumber { get; set; }
         string Mass { get; set; }
         string MaxMass { get; set; }
         string SerialPts { get; set; }
         string NumberPts { get; set; }
         DateTime? DatePts { get; set; }
         string ByPts { get; set; }
         int? CopyPtsId { get; set; }
         IUserFile CopyPts { get; set; }
         string SerialSts { get; set; }
         string NumberSts { get; set; }
         DateTime? DateSts { get; set; }
         string BySts { get; set; }
         int? CopyStsId { get; set; }
         IUserFile CopySts { get; set; }
         int EngineTypeId { get; set; }
         IEngineType EngineType { get; set; }
         string EngineMake { get; set; }
         string Strong { get; set; }
         string Pa { get; set; }
         string Volume { get; set; }

    }
}
