using Com.Danliris.Service.Finishing.Printing.Lib.Models.NewShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.NewShipmentDocument;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.NewShipmentDocument
{
    public interface INewShipmentDocumentService : IBaseFacade<NewShipmentDocumentModel>
    {
        Task<List<NewShipmentDocumentPackingReceiptItemModel>> GetShipmentProducts(int productionOrderId, int buyerId);
        Task<List<NewShipmentDocumentPackingReceiptItemProductViewModel>> GetProductNames(int shipmentDocumentId);
    }
}
