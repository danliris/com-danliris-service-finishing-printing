using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Packing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Packing
{
    public class PackingViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var date = DateTimeOffset.Now;
            var packingDetails = new List<PackingDetailViewModel>()
                {
                    new PackingDetailViewModel()
                    {
                        PackingId =1
                    }
                };
            PackingViewModel viewModel = new PackingViewModel()
            {
                UId = "UId",
                DeliveryType = "DeliveryType",
                BuyerAddress = "BuyerAddress",
                BuyerCode = "BuyerCode",
                BuyerName = "BuyerName",
                BuyerId = 1,
                BuyerType = "BuyerType",
                ColorCode = "ColorCode",
                Code = "Code",
                ColorName = "ColorName",
                ColorType = "ColorType",
                Construction = "Construction",
                Date = date,
                Declined = true,
                DesignCode = "DesignCode",
                FinishedProductType = "FinishedProductType",
                DesignNumber = "DesignNumber",
                Material = "Material",
                MaterialConstructionFinishId = 1,
                MaterialConstructionFinishName = "MaterialConstructionFinishName",
                MaterialId = 1,
                MaterialWidthFinish = "MaterialWidthFinish",
                Motif = "Motif",
                Accepted = true,
                OrderTypeCode = "OrderTypeCode",
                OrderTypeId = 1,
                OrderTypeName = "OrderTypeName",
                ProductionOrderId = 1,
                PackingUom = "PackingUom",
                PackingDetails = packingDetails,
                Status = "Status",
                SalesContractNo = "SalesContractNo"
            };
        } 
        
        [Fact]
        public void validate_default()
        {

            PackingViewModel viewModel = new PackingViewModel();
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_PackingDetails_moreThan_1()
        {

            PackingViewModel viewModel = new PackingViewModel()
            {
                PackingDetails = new List<PackingDetailViewModel>()
                {
                    new PackingDetailViewModel()
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_rowErrorCount_moreThan_1()
        {

            PackingViewModel viewModel = new PackingViewModel()
            {
                PackingDetails = new List<PackingDetailViewModel>()
                {
                    new PackingDetailViewModel()
                    {
                        Lot ="Lot",
                        Quantity =1,
                        Length =1,
                        Grade ="A"
                    },
                    new PackingDetailViewModel()
                    {
                        Lot ="Lot",
                        Quantity =1,
                        Length =1,
                        Grade ="A"
                    }
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }
    }


