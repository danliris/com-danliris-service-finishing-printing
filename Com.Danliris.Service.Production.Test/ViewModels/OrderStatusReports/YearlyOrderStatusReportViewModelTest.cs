using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.OrderStatusReports;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.OrderStatusReports
{
   public class YearlyOrderStatusReportViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {

            ProductionOrderStatusReportViewModel viewModel = new ProductionOrderStatusReportViewModel()
            {
                cartNumber = "cartNumber",
                process = "process",
                processArea = "processArea",
                quantity =1,
                status = "status"
            };

            Assert.Equal("cartNumber", viewModel.cartNumber);
            Assert.Equal("process", viewModel.process);
            Assert.Equal("processArea", viewModel.processArea);
            Assert.Equal(1, viewModel.quantity);
            Assert.Equal("status", viewModel.status);
        }
    }
}
