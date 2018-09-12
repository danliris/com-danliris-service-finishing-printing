using Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Packing;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Packing
{
    public interface IPackingFacade : IBaseFacade<PackingModel>
    {
        ReadResponse<PackingViewModel> GetReport(int page, int size, string code, string productionOrderNo, DateTime? dateFrom, DateTime? dateTo, int offSet);
    }
}
