using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Event;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Monitoring_Event
{
    public class MonitoringEventReportViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            MonitoringEventReportViewModel viewModel = new MonitoringEventReportViewModel()
            {
                code = "code",
                timeInMilisStart =1,
                timeInMilisEnd =1,
                machineId =1,
                productionOrderId =1,
                productionOrderDeliveryDate = "productionOrderDeliveryDate",
                productionOrderDetailCode = "productionOrderDetailCode",
                productionOrderDetailColorRequest = "productionOrderDetailColorRequest",
                productionOrderDetailColorTemplate = "productionOrderDetailColorTemplate",
                productionOrderDetailColorTypeId = "productionOrderDetailColorTypeId",
                productionOrderDetailColorType = "productionOrderDetailColorType",
                productionOrderDetailQuantity =1,
                machineEventId =1,
                machineEventCode = "machineEventCode",
                machineEventCategory = "machineEventCategory"
            };
            Assert.Equal("code", viewModel.code);
            Assert.Equal(1, viewModel.timeInMilisStart);
            Assert.Equal(1, viewModel.machineId);
            Assert.Equal(1, viewModel.timeInMilisEnd);
            Assert.Equal(1, viewModel.productionOrderId);
            Assert.Equal("productionOrderDeliveryDate", viewModel.productionOrderDeliveryDate);
            Assert.Equal("productionOrderDetailCode", viewModel.productionOrderDetailCode);
            Assert.Equal("productionOrderDetailColorRequest", viewModel.productionOrderDetailColorRequest);
            Assert.Equal("productionOrderDetailColorTemplate", viewModel.productionOrderDetailColorTemplate);
            Assert.Equal("productionOrderDetailColorTypeId", viewModel.productionOrderDetailColorTypeId);
            Assert.Equal("productionOrderDetailColorType", viewModel.productionOrderDetailColorType);
            Assert.Equal(1, viewModel.productionOrderDetailQuantity);
            Assert.Equal(1, viewModel.machineEventId);
            Assert.Equal("machineEventCode", viewModel.machineEventCode);
            Assert.Equal("machineEventCategory", viewModel.machineEventCategory);
        }
        }
}
