using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.FabricQualityControl
{
    public class FabricQualityControlViewModel : BaseViewModel, IValidatableObject
    {
        public string Buyer { get; set; }
        public string CartNo { get; set; }
        public string Code { get; set; }
        public string Color { get; set; }
        public string Construction { get; set; }
        public DateTimeOffset DateIm { get; set; }
        public List<FabricGradeTestViewModel> FabricGradeTests { get; set; }
        public string Group { get; set; }
        public bool? IsUsed { get; set; }
        public string KanbanCode { get; set; }
        public int? KanbanId { get; set; }
        public string MachineNoIm { get; set; }
        public string OperatorIm { get; set; }
        public double? OrderQuantity { get; set; }
        public string PackingInstruction { get; set; }
        public double? PointLimit { get; set; }
        public double? PointSystem { get; set; }
        public string ProductionOrderNo { get; set; }
        public string ProductionOrderType { get; set; }
        public string ShiftIm { get; set; }
        public string Uom { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (KanbanId == null || KanbanId == 0)
                yield return new ValidationResult("Kanban harus diisi", new List<string> { "Kanban" });

            if (PointSystem != 10 && PointSystem != 4)
                yield return new ValidationResult("Point System harus diisi", new List<string> { "PointSystem" });
            else if (PointSystem == 4)
                if (PointLimit == null || PointLimit <= 0)
                    yield return new ValidationResult("Point Limit harus lebih besar dari 0", new List<string> { "PointLimit" });

            if (DateIm == null)
                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "Tanggal IM" });

            if (string.IsNullOrWhiteSpace(ShiftIm))
                yield return new ValidationResult("Shift harus diisi", new List<string> { "ShiftIm" });

            if (string.IsNullOrWhiteSpace(OperatorIm))
                yield return new ValidationResult("Operator harus diisi", new List<string> { "OperatorIm" });

            if (string.IsNullOrWhiteSpace(MachineNoIm))
                yield return new ValidationResult("Nomor Mesin harus diisi", new List<string> { "MachineNoIm" });

            int Count = 0;
            string FabricGradeTestErrors = "[";

            if (FabricGradeTests == null || FabricGradeTests.Count <= 0)
                yield return new ValidationResult("Tabel kain harus diisi", new List<string> { "FabricGradeTest" });
            else
            {
                foreach (var fabricGradeTest in FabricGradeTests)
                {
                    FabricGradeTestErrors += "{";

                    if (string.IsNullOrWhiteSpace(fabricGradeTest.PcsNo))
                        yield return new ValidationResult("Nomor Pcs harus diisi", new List<string> { "PcsNo" });

                    if (fabricGradeTest.InitLength <= 0)
                        yield return new ValidationResult("Panjang harus diisi", new List<string> { "InitLength" });


                    if (fabricGradeTest.AvalLength > fabricGradeTest.InitLength)
                        yield return new ValidationResult("Panjang Aval tidak boleh lebih dari panjang kain", new List<string> { "AvalLength" });
                    else if (fabricGradeTest.SampleLength > fabricGradeTest.InitLength)
                        yield return new ValidationResult("Panjang Sampel tidak boleh lebih dari panjang kain", new List<string> { "SampleLength" });
                    else if (fabricGradeTest.AvalLength + fabricGradeTest.SampleLength > fabricGradeTest.InitLength)
                    {
                        yield return new ValidationResult("Jumlah Panjang Aval dan Panjang Sampel tidak boleh lebih dari panjang kain", new List<string> { "AvalLength" });
                        yield return new ValidationResult("Jumlah Panjang Aval dan Panjang Sampel tidak boleh lebih dari panjang kain", new List<string> { "SampleLength" });
                    }

                    if (fabricGradeTest.Width <= 0)
                        yield return new ValidationResult("Lebar kain harus lebih besar dari 0", new List<string> { "Width" });

                    FabricGradeTestErrors += "}, ";
                }
            }
            FabricGradeTestErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(FabricGradeTestErrors, new List<string> { "FabricGradeTests" });
        }
    }
}
