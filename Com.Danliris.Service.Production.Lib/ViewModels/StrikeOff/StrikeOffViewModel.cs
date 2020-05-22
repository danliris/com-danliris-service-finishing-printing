
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.StrikeOff;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.StrikeOff
{
    public class StrikeOffViewModel : BaseViewModel, IValidatableObject
    {
        public StrikeOffViewModel()
        {
            StrikeOffItems = new HashSet<StrikeOffItemViewModel>();
        }

        public string Code { get; set; }
        public string Remark { get; set; }
        public string Type { get; set; }
        public string Cloth { get; set; }

        public ICollection<StrikeOffItemViewModel> StrikeOffItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            StrikeOffLogic service = (StrikeOffLogic)validationContext.GetService(typeof(StrikeOffLogic));

            if (string.IsNullOrEmpty(Code))
            {
                yield return new ValidationResult("Kode harus diisi", new List<string> { "Code" });
            }
            else
            {
                if (service.CheckDuplicateCode(Code, Id))
                {
                    yield return new ValidationResult("Kode Tidak boleh Duplikat", new List<string> { "Code" });
                }
            }

            if (string.IsNullOrEmpty(Type))
                yield return new ValidationResult("Jenis Printing harus diisi", new List<string> { "Type" });

            if (StrikeOffItems == null || StrikeOffItems.Count == 0)
            {
                yield return new ValidationResult("Tabel Kode Warna Harus Diisi", new List<string> { "StrikeOffItem" });
            }
            else
            {
                var anyError = false;
                var strikeOffItemsErrors = "[";


                foreach (var item in StrikeOffItems)
                {
                    strikeOffItemsErrors += "{";

                    if (string.IsNullOrEmpty(item.ColorCode))
                    {
                        anyError = true;
                        strikeOffItemsErrors += "ColorCode: 'Kode Warna Harus Diisi', ";
                    }
                    else
                    {
                        if (item.DyeStuffItems != null && item.DyeStuffItems.Count > 0 && item.ChemicalItems != null && item.ChemicalItems.Count > 0)
                        {
                            if (item.DyeStuffItems.Sum(s => s.Quantity) + item.ChemicalItems.Where(s => s.Name != null && s.Name.ToLower() != "vicositas").Sum(s => s.Quantity) != 1000)
                            {
                                anyError = true;
                                strikeOffItemsErrors += "ColorCode: 'Total Quantity dari Dye Stuff dan Chemical (Tanpa Vicositas) harus sama dengan 1000', ";
                            }
                        }
                    }

                    if (item.ChemicalItems == null || item.ChemicalItems.Count == 0)
                    {
                        anyError = true;
                        strikeOffItemsErrors += "ChemicalItem: 'Tabel Chemical Harus Diisi', ";
                    }
                    else
                    {
                        var waterChemical = item.ChemicalItems.FirstOrDefault(s => s.Name != null && s.Name.ToLower() == "air");
                        if (waterChemical != null && waterChemical.Quantity < 0)
                        {
                            anyError = true;
                            strikeOffItemsErrors += "ChemicalItem: 'Quantity Air tidak boleh kurang dari 0', ";
                        }
                        else
                        {
                            strikeOffItemsErrors += "ChemicalItems : [ ";
                            foreach (var chemical in item.ChemicalItems)
                            {
                                strikeOffItemsErrors += "{";
                                if (string.IsNullOrEmpty(chemical.Name))
                                {
                                    anyError = true;
                                    strikeOffItemsErrors += "Name: 'Nama Harus Diisi', ";
                                }

                                strikeOffItemsErrors += "}, ";
                            }
                            strikeOffItemsErrors += "], ";
                        }
                    }

                    if (item.DyeStuffItems == null || item.DyeStuffItems.Count == 0)
                    {
                        anyError = true;
                        strikeOffItemsErrors += "DyeStuffItem: 'Table Dye Stuff Harus Diisi', ";
                    }
                    else
                    {
                        if (item.DyeStuffItems.Sum(s => s.Quantity) > 1000)
                        {
                            anyError = true;
                            strikeOffItemsErrors += "DyeStuffItem: 'Total Quantity tidak boleh melebihi 1000', ";
                        }
                        else
                        {
                            strikeOffItemsErrors += "DyeStuffItems: [ ";
                            foreach (var dyeStuff in item.DyeStuffItems)
                            {
                                strikeOffItemsErrors += "{";
                                if (dyeStuff.Product == null)
                                {
                                    anyError = true;
                                    strikeOffItemsErrors += "Product: 'Dye Stuff Harus Diisi', ";
                                }
                                strikeOffItemsErrors += "}, ";
                            }
                            strikeOffItemsErrors += "], ";
                        }
                    }

                   
                    strikeOffItemsErrors += "}, ";
                }

                strikeOffItemsErrors += "]";
                if (anyError)
                {
                    yield return new ValidationResult(strikeOffItemsErrors, new List<string> { "StrikeOffItems" });
                }
            }
        }
    }
}
