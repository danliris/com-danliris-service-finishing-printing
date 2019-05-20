using Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Packing;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Packing
{
    public interface IPackingFacade : IBaseFacade<PackingModel>
    {
        Task<PackingDetailModel> GetPackingDetail(string productName);

        ReadResponse<PackingViewModel> GetReport(int page, int size, string code, int productionOrderId, DateTime? dateFrom, DateTime? dateTo, int offSet);

        MemoryStream GenerateExcel(string code, int productionOrderId, DateTime? dateFrom, DateTime? dateTo, int offSet);

        List<PackingViewModel> GetReport(string code, int productionOrderId, DateTime? dateFrom, DateTime? dateTo, int offSet);
    }
}
