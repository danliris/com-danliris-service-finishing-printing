using Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ReturToQC;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.ReturToQC
{
    public interface IReturToQCFacade : IBaseFacade<ReturToQCModel>
    {
        ReadResponse<ReturToQCViewModel> GetReport(int page, int size, DateTime? dateFrom , DateTime? dateTo , string productionOrderNo , string returNo , string destination , string deliveryOrderNo, int offSet);

        List<ReturToQCViewModel> GetReport(DateTime? dateFrom, DateTime? dateTo, string productionOrderNo, string returNo, string destination, string deliveryOrderNo, int offSet);

        MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, string productionOrderNo, string returNo, string destination, string deliveryOrderNo, int offSet);
    }
}
