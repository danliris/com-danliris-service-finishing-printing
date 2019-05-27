using Com.Danliris.Service.Finishing.Printing.Lib.Models.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.FabricQualityControl;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.FabricQualityControl
{
    public interface IFabricQualityControlFacade : IBaseFacade<FabricQualityControlModel>
    {
        ReadResponse<FabricQualityControlViewModel> GetReport(int page, int size, string code, int kanbanId, string productionOrderType, string productionOrderNo, string shiftIm, DateTime? dateFrom, DateTime? dateTo, int offSet);

        MemoryStream GenerateExcel(string code, int kanbanId, string productionOrderType, string productionOrderNo, string shiftIm, DateTime? dateFrom, DateTime? dateTo, int offSet);

        Task<List<FabricQCGradeTestsViewModel>> GetForSPP(string no);

        List<FabricQualityControlViewModel> GetReport(string code, int kanbanId, string productionOrderType, string productionOrderNo, string shiftIm, DateTime? dateFrom, DateTime? dateTo, int offSet);
    }
}
