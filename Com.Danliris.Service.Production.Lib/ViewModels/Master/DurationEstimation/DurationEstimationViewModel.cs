using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Production.Lib.ViewModels.Master.DurationEstimation
{
    public class DurationEstimationViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public ProcessTypeIntegrationViewModel ProcessType { get; set; }
        public List<DurationEstimationAreaViewModel> Areas { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ProcessType == null)
                yield return new ValidationResult("Jenis Proses harus diisi", new List<string> { "ProcessType" });

            int Count = 0;
            string AreaErrors = "[";

            if (Areas == null || Areas.Count <= 0)
                yield return new ValidationResult("Tabel Area harus diisi", new List<string> { "Area" });
            else
            {
                foreach (var Area in Areas)
                {
                    AreaErrors += "{";
                    if (string.IsNullOrWhiteSpace(Area.Name))
                    {
                        Count++;
                        AreaErrors += "Name: 'Nama Area harus diisi', ";
                    }
                    else if (Areas.Where(w => w.Name == Area.Name).Count() > 1)
                    {
                        Count++;
                        AreaErrors += "Name: 'Nama Area tidak boleh duplikat', ";
                    }

                    if (Area.Duration <= 0)
                    {
                        Count++;
                        AreaErrors += "Duration: 'Durasi harus lebih besar dari 0', ";
                    }
                    AreaErrors += "}, ";
                }
            }
            AreaErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(AreaErrors, new List<string> { "Areas" });
        }
    }
}
