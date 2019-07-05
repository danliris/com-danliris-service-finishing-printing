using Com.Danliris.Service.Finishing.Printing.Lib.Models.ShipmentDocument;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.ShipmentDocument
{
    public interface IShipmentDocumentService : IBaseFacade<ShipmentDocumentModel>
    {
        Task<List<ShipmentDocumentPackingReceiptItemModel>> GetShipmentProducts(int productionOrderId, int buyerId);
    }
}
