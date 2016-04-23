using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Trancport : Entity
    {
        public string Number { get; set; }
        [ForeignKey("Make")]
        public int MakeId { get; set; }
        public virtual MakeTrancport Make { get; set; }
        [ForeignKey("Model")]
        public int ModelId { get; set; }
        public virtual ModelTrancport Model { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual TrancportCategory Category { get; set; }
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public virtual TrancportType Type { get; set; }
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
        public virtual UserFile CopyPts { get; set; }
        public string SerialSts { get; set; }
        public string NumberSts { get; set; }
        public DateTime? DateSts { get; set; }
        public string BySts { get; set; }
        [ForeignKey("CopySts")]
        public int? CopyStsId { get; set; }
        public virtual UserFile CopySts { get; set; }
        [ForeignKey("EngineType")]
        public int EngineTypeId { get; set; }
        public virtual EngineType EngineType { get; set; }
        public string EngineMake { get; set; }
        public string Strong { get; set; }
        public string Pa { get; set; }
        public string Volume { get; set; }

    }
}
