using Aimp.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aimp.Model.Entities
{
    public class Trancport : Entity, ITrancport
    {
        public string Number { get; set; }
        [ForeignKey("Make")]
        public int MakeId { get; set; }
        public virtual IMakeTrancport Make { get; set; }
        [ForeignKey("Model")]
        public int ModelId { get; set; }
        public virtual IModelTrancport Model { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual ITrancportCategory Category { get; set; }
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public virtual ITrancportType Type { get; set; }
        public string Maker { get; set; }
        public short Year { get; set; }
        public string Color { get; set; }
        public string Vin { get; set; }
        public string BodyNumber { get; set; }
        public string ChassisNumber { get; set; }
        public string Mass { get; set; }
        public string MaxMass { get; set; }
        public string SerialPts { get; set; }
        public string NumberPts { get; set; }
        public DateTime? DatePts { get; set; }
        public string ByPts { get; set; }
        [ForeignKey("CopyPts")]
        public int? CopyPtsId { get; set; }
        public virtual IUserFile CopyPts { get; set; }
        public string SerialSts { get; set; }
        public string NumberSts { get; set; }
        public DateTime? DateSts { get; set; }
        public string BySts { get; set; }
        [ForeignKey("CopySts")]
        public int? CopyStsId { get; set; }
        public virtual IUserFile CopySts { get; set; }
        [ForeignKey("EngineType")]
        public int EngineTypeId { get; set; }
        public virtual IEngineType EngineType { get; set; }
        public string EngineMake { get; set; }
        public string Strong { get; set; }
        public string Pa { get; set; }
        public string Volume { get; set; }

    }
}
