using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.ShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ShipmentDocument;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils
{
    public class ShipmentDocumentDataUtil
    {
        private readonly ShipmentDocumentService Service;

        public ShipmentDocumentDataUtil(ShipmentDocumentService service)
        {
            Service = service;
        }

        public ShipmentDocumentModel GetNewData()
        {
            var TestData = new ShipmentDocumentModel()
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
                DeliveryCode = "DeliveryCode",
                DeliveryDate = DateTimeOffset.Now,
                DeliveryReference = "Reference",
                Details = new List<ShipmentDocumentDetailModel>()
                {
                    new ShipmentDocumentDetailModel()
                    {
                        Items = new List<ShipmentDocumentItemModel>()
                        {
                            new ShipmentDocumentItemModel()
                            {
                                PackingReceiptItems = new List<ShipmentDocumentPackingReceiptItemModel>()
                                {
                                    new ShipmentDocumentPackingReceiptItemModel()
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

        public ShipmentDocumentViewModel GetDataToValidate()
        {
            ShipmentDocumentViewModel TestData = new ShipmentDocumentViewModel()
            {
                Buyer = new BuyerIntegrationViewModel()
                {
                    Id = 1
                },
                DeliveryDate = DateTimeOffset.Now,
                DeliveryCode = "DeliveryCode",
                ProductIdentity = "ProductIdentity",
                ShipmentNumber = "ShipmentNumber",
                Details = new List<ShipmentDocumentDetailViewModel>()
                {
                    new ShipmentDocumentDetailViewModel()
                    {
                        Items = new List<ShipmentDocumentItemViewModel>()
                        {
                            new ShipmentDocumentItemViewModel()
                            {
                                PackingReceiptItems = new List<ShipmentDocumentPackingReceiptItemViewModel>()
                                {
                                    new ShipmentDocumentPackingReceiptItemViewModel()
                                }
                            }
                        }
                    }
                }
            };

            return TestData;
        }

        public async Task<ShipmentDocumentModel> GetTestData()
        {
            var model = GetNewData();
            await Service.CreateAsync(model);
            return await Service.ReadByIdAsync(model.Id);
        }
    }
}
