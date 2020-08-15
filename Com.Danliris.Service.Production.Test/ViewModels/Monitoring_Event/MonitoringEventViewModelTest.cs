using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Event;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Monitoring_Event
{
    public class MonitoringEventViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var time = DateTimeOffset.Now;
            var machineViewModel = new MachineViewModel()
            {
                UId= "UId",
               Code = "Code"
            };
            var production = new ProductionOrderIntegrationViewModel()
            {

            };
            var productionOrderDetail = new ProductionOrderDetailIntegrationViewModel();
            var machineEvent = new MachineEventViewModel();
            MonitoringEventViewModel viewModel = new MonitoringEventViewModel()
            {
                UId = "UId",
                CartNumber = "CartNumber",
                Code = "Code",
                DateStart = time,
                DateEnd =time,
                TimeInMilisStart =1,
                TimeInMilisEnd =1,
                Remark = "Remark",
                Machine = machineViewModel,
                ProductionOrder = production,
                ProductionOrderDetail = productionOrderDetail,
                MachineEvent = machineEvent
            };

            Assert.Equal("UId", viewModel.UId);
            Assert.Equal("CartNumber", viewModel.CartNumber);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal(time, viewModel.DateStart);
            Assert.Equal(time, viewModel.DateEnd);
            Assert.Equal(1, viewModel.TimeInMilisStart);
            Assert.Equal(1, viewModel.TimeInMilisEnd);
            Assert.Equal("Remark", viewModel.Remark);
            Assert.Equal("Remark", viewModel.Remark);
            Assert.Equal(machineViewModel, viewModel.Machine);
            Assert.Equal(production, viewModel.ProductionOrder);
            Assert.Equal(productionOrderDetail, viewModel.ProductionOrderDetail);
            Assert.Equal(machineEvent, viewModel.MachineEvent);
        }

        [Fact]
        public void validate_default()
        {

            MonitoringEventViewModel viewModel = new MonitoringEventViewModel()
            {
                Machine = new MachineViewModel()
                {
                    Id=0,
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_DateStart_greaterThan_DateEnd()
        {

            MonitoringEventViewModel viewModel = new MonitoringEventViewModel()
            {
                DateStart = DateTimeOffset.Now.AddDays(2),
                DateEnd = DateTimeOffset.Now
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_DateEnd_greaterThan_dateNow()
        {

            MonitoringEventViewModel viewModel = new MonitoringEventViewModel()
            {
                DateEnd = DateTimeOffset.Now.AddDays(1),
            };

            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }
}
