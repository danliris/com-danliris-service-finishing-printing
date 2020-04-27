using Com.Danliris.Service.Finishing.Printing.Lib.Models.ColorReceipt;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.ColorReceipt
{
    public interface IColorReceiptFacade : IBaseFacade<ColorReceiptModel>
    {
        Task<TechnicianModel> CreateTechnician(string name);
    }
}
