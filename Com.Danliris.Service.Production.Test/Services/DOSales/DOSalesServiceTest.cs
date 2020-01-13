using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DOSales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Services.DOSales
{
    public class DOSalesServiceTest
    {
        private const string ENTITY = "DOSales";

        [MethodImpl(MethodImplOptions.NoInlining)]
        
        [Fact]
        public void Should_Success_Validate_All_Null_Data()
        {
            var vm = new DOSalesViewModel();

            Assert.True(vm.Validate(null).Count() > 0);
        }

        [Fact]
        public void Should_Success_Validate_Invalid_Data()
        {
            var viewModel = new DOSalesViewModel()
            {
                UId = "",
                Code = "",
                AutoIncreament = -1,
                DOSalesNo = "",
                DOSalesType = "",
                DOSalesDate = DateTimeOffset.UtcNow.AddDays(5),
                StorageId = 0,
                StorageName = "",
                StorageDivision = "",
                HeadOfStorage = "",
                ProductionOrderId = 0,
                ProductionOrderNo = "",
                MaterialId = 0,
                Material = "",
                MaterialWidthFinish = "",
                MaterialConstructionFinishId = 0,
                MaterialConstructionFinishName = "",
                BuyerAddress = "",
                BuyerCode = "",
                BuyerId = 0,
                BuyerName = "",
                BuyerNPWP = "",
                BuyerType = "",
                DestinationBuyerAddress = "",
                DestinationBuyerCode = "",
                DestinationBuyerId = 0,
                DestinationBuyerName = "",
                DestinationBuyerNPWP = "",
                DestinationBuyerType = "",
                PackingUom = "",
                LengthUom = "",
                Disp = -1,
                Op = -1,
                Sc = -1,
                Construction = "",
                Remark = "",
                Status = "",
                DOSalesDetails = new List<DOSalesDetailViewModel>()
                {
                    new DOSalesDetailViewModel()
                    {
                        DOSalesId = 0,
                        TotalPacking = -1,
                        TotalLength = -1,
                        TotalLengthConversion = -1,
                        UnitCode = "",
                        UnitName = "",
                        UnitRemark = ""
                    }
                }
            };

            Assert.True(viewModel.Validate(null).Count() > 0);
        }
    }
}
