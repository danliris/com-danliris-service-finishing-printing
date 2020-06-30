using Com.Danliris.Service.Finishing.Printing.Lib.Models.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DyestuffChemicalUsageReceipt
{
    public interface IDyestuffChemicalUsageReceiptFacade : IBaseFacade<DyestuffChemicalUsageReceiptModel>
    {
        Task<DyestuffChemicalUsageReceiptModel> GetDataByStrikeOff(int strikeOffId);
    }
}
