using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.FabricQualityControl
{
    public class FabricGradeTestViewModel : BaseViewModel, IValidatableObject
    {
        public double? AvalLength { get; set; }
        public List<CriteriaViewModel> Criteria { get; set; }
        public double? FabricGradeTest { get; set; }
        public double? FinalArea { get; set; }
        public double? FinalGradeTest { get; set; }
        public double? FinalLength { get; set; }
        public double? FinalScore { get; set; }
        public string Grade { get; set; }
        public double? InitLength { get; set; }
        public string PcsNo { get; set; }
        public double? PointLimit { get; set; }
        public double? PointSystem { get; set; }
        public double? SampleLength { get; set; }
        public double? Score { get; set; }
        public string Type { get; set; }
        public double? Width { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}