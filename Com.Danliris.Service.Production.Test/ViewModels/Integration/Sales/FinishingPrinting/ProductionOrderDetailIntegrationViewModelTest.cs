using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Sales.FinishingPrinting
{
    public class ProductionOrderDetailIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string Code = "Code test";
            string ColorRequest = "ColorRequest test";
            string ColorTemplate = "ColorTemplate test";
            double Quantity = 100;
            ColorTypeIntegrationViewModel ctivm = new ColorTypeIntegrationViewModel();
            UOMIntegrationViewModel uvm = new UOMIntegrationViewModel();

            ProductionOrderDetailIntegrationViewModel podivm = new ProductionOrderDetailIntegrationViewModel();
            podivm.Id = 1;
            podivm.Code = Code;
            podivm.ColorRequest = ColorRequest;
            podivm.ColorTemplate = ColorTemplate;
            podivm.Quantity = 100;
            podivm.ColorType = ctivm;
            podivm.Uom = uvm;

            Assert.Equal(Id, podivm.Id);
            Assert.Equal(Code, podivm.Code);
            Assert.Equal(ColorRequest, podivm.ColorRequest);
            Assert.Equal(ColorTemplate, podivm.ColorTemplate);
            Assert.Equal(Quantity, podivm.Quantity);
            Assert.Equal(ctivm, podivm.ColorType);
            Assert.Equal(uvm, podivm.Uom);
        }
    }
}
