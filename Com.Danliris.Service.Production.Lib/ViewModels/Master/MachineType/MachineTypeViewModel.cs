using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.MachineType
{
    public class MachineTypeViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<MachineTypeIndicatorsViewModel> Indicators { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name))
                yield return new ValidationResult("Nama harus diisi", new List<string> { "Name" });

            int Count = 0;
            string Indicators = "[";

            foreach (MachineTypeIndicatorsViewModel data in this.Indicators)
            {
                if (string.IsNullOrWhiteSpace(data.Indicator))
                {
                    Count++;
                    Indicators += "{ 'harus di isi' }, ";
                }

                if (data.DataType == "input pilihan")
                {
                    if (!string.IsNullOrWhiteSpace(data.DefaultValue))
                    {
                        string[] optionValue = data.DefaultValue.Split(",");

                        if (optionValue.Length <= 1 || Array.FindAll(optionValue, x => String.IsNullOrWhiteSpace(x)).Length > 0)
                        {
                            Count++;
                            Indicators += "{ 'input pilihan harus lebih dari 1' }, ";
                        }
                    }
                }
                else if (data.DataType == "input skala angka")
                {
                    if (!string.IsNullOrWhiteSpace(data.DefaultValue))
                    {
                        var rangeValue = data.DefaultValue.Split("-");
                        double output;
                        {
                            if (rangeValue.Length <= 1 || rangeValue.Length > 2 || !double.TryParse(rangeValue[0], out output) || !double.TryParse(rangeValue[1], out output))
                            {
                                Count++;
                                Indicators += "{ 'input tidak tepat,contoh:1-2' }, ";
                            }
                            else if (Convert.ToDouble(rangeValue[0]) >= Convert.ToDouble(rangeValue[1]) || Convert.ToDouble(rangeValue[1]) <= Convert.ToDouble(rangeValue[0]))
                            {
                                Count++;
                                Indicators += "{ 'input tidak tepat, angka pertama harus > kedua' }, ";
                            }
                        }
                    }
                }            
            }
            Indicators += "]";

            if (Count > 0)
            {
                yield return new ValidationResult(Indicators, new List<string> { "Indicators" });
            }

        }
    }
}
