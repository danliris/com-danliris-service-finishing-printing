using Com.Moonlay.Models;
using System.Collections.Generic;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.FabricQualityControl
{
    public class FabricGradeTestModel : StandardEntity
    {
        public double AvalLength { get; set; }
        public ICollection<CriteriaModel> Criteria { get; set; }
        public double FabricGradeTest { get; set; }
        public double FinalArea { get; set; }
        public double FinalGradeTest { get; set; }
        public double FinalLength { get; set; }
        public double FinalScore { get; set; }
        public string Grade { get; set; }
        public double InitLength { get; set; }
        public string PcsNo { get; set; }
        public double PointLimit { get; set; }
        public double PointSystem { get; set; }
        public double SampleLength { get; set; }
        public double Score { get; set; }
        public string Type { get; set; }
        public double Width { get; set; }

        public int FabricQualityControlId { get; set; }
        public virtual FabricQualityControlModel FabricQualityControl { get; set; }
    }
}