using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.NewShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.NewShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.NewShipmentDocument;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils
{
    public class NewShipmentDocumentDataUtil
    {
        private readonly NewShipmentDocumentService Service;
        private readonly ProductionDbContext _dbContext;

        public NewShipmentDocumentDataUtil(NewShipmentDocumentService service, ProductionDbContext dbContext)
        {
            Service = service;
            _dbContext = dbContext;
        }

        private PackingModel GetPacking()
        {
            return new PackingModel()
            {
                PackingDetails = new List<PackingDetailModel>()
                {
                    new PackingDetailModel()
                    {

                    }
                }
            };
        }

        private PackingReceiptModel GetPackingReceipt(PackingModel packingModel)
        {
            return new PackingReceiptModel()
            {
                PackingId = packingModel.Id,
                Items = new List<PackingReceiptItem>()
                {
                    new PackingReceiptItem()
                }
            };
        }

        public async Task<NewShipmentDocumentModel> GetNewData()
        {
            var packingService = new PackingFacade(Service._ServiceProvider, _dbContext);
            var packingReceiptService = new PackingReceiptFacade(Service._ServiceProvider, _dbContext);

            var packingModel = GetPacking();
            await packingService.CreateAsync(packingModel);

            var packingReceiptModel = GetPackingReceipt(packingModel);
            await packingReceiptService.CreateAsync(packingReceiptModel);

            var TestData = new NewShipmentDocumentModel()
            {
                BuyerAddress = "BuyerAddress",
                BuyerCity = "BuyerCity",
                BuyerCode = "BuyerCode",
                BuyerContact = "BuyerContact",
                BuyerCountry = "BuyerCountry",
                BuyerId = 1,
                BuyerName = "BuyerName",
                BuyerNPWP = " BuyerNPWP",
                BuyerTempo = "BuyerTempo",
                BuyerType = "BuyerType",
                DOSalesId = 1,
                DOSalesNo = "DOSalesNo",
                DeliveryDate = DateTimeOffset.Now,
                DeliveryReference = "Reference",
                Details = new List<NewShipmentDocumentDetailModel>()
                {
                    new NewShipmentDocumentDetailModel()
                    {
                        Items = new List<NewShipmentDocumentItemModel>()
                        {
                            new NewShipmentDocumentItemModel()
                            {
                                PackingReceiptId = packingReceiptModel.Id,
                                PackingReceiptItems = new List<NewShipmentDocumentPackingReceiptItemModel>()
                                {
                                    new NewShipmentDocumentPackingReceiptItemModel()
                                    {
                                        PackingReceiptItemIndex = packingReceiptModel.Items.FirstOrDefault().Id
                                    }
                                }
                            }
                        }
                    }
                }
            };

            return TestData;
        }

        public NewShipmentDocumentViewModel GetDataToValidate()
        {
            NewShipmentDocumentViewModel TestData = new NewShipmentDocumentViewModel()
            {
                Buyer = new BuyerIntegrationViewModel()
                {
                    Id = 1
                },
                DeliveryDate = DateTimeOffset.Now,
                DOSales = new Lib.ViewModels.Integration.Sales.DOSales.DOSalesIntegrationViewModel()
                {
                    Id = 1,
                    DOSalesNo = "DOSalesNo",
                },
                ProductIdentity = "ProductIdentity",
                ShipmentNumber = "ShipmentNumber",
                Details = new List<NewShipmentDocumentDetailViewModel>()
                {
                    new NewShipmentDocumentDetailViewModel()
                    {
                        Items = new List<NewShipmentDocumentItemViewModel>()
                        {
                            new NewShipmentDocumentItemViewModel()
                            {
                                PackingReceiptItems = new List<NewShipmentDocumentPackingReceiptItemViewModel>()
                                {
                                    new NewShipmentDocumentPackingReceiptItemViewModel()
                                }
                            }
                        }
                    }
                }
            };

            return TestData;
        }

        public async Task<NewShipmentDocumentModel> GetTestData()
        {
            var model = await GetNewData();
            await Service.CreateAsync(model);
            return await Service.ReadByIdAsync(model.Id);
        }
    }
}
