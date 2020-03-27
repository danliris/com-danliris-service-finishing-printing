using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.NewShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.NewShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.NewShipmentDocument;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils
{
    public class NewShipmentDocumentDataUtil
    {
        private readonly NewShipmentDocumentService Service;

        public NewShipmentDocumentDataUtil(NewShipmentDocumentService service)
        {
            Service = service;
        }

        public NewShipmentDocumentModel GetNewData()
        {
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
                                PackingReceiptItems = new List<NewShipmentDocumentPackingReceiptItemModel>()
                                {
                                    new NewShipmentDocumentPackingReceiptItemModel()
                                    {

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
            var model = GetNewData();
            await Service.CreateAsync(model);
            return await Service.ReadByIdAsync(model.Id);
        }
    }
}
