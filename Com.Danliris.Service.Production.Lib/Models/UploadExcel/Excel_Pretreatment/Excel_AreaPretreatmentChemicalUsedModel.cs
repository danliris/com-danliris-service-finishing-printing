using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.UploadExcel.Excel_Pretreatment
{
    public class Excel_AreaPretreatmentChemicalUsedModel
    {
        public int Id { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public string MachineName { get; set; }
        public string OrderNo { get; set; }
        public string Material { get; set; }
        public double? ChemicalPrimalase { get; set; }
        public double? ChemicalStabironAT2 { get; set; }
        public double? ChemicalSizolTX { get; set; }
        public double? ChemicalNaoh { get; set; }
        public double? ChemicalNeoratePH { get; set; }
        public double? ChemicalH2o2 { get; set; }
        public double? ChemicalDsm60 { get; set; }
        public double? ChemicalDjetpt { get; set; }
        public double? ChemicalGamasol { get; set; }
        public string Remark { get; set; }

    }
}
