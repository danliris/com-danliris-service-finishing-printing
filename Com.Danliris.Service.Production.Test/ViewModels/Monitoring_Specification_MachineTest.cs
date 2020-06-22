using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Specification_Machine;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels
{
    public class Monitoring_Specification_MachineTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var time = DateTimeOffset.Now;
            var details = new List<MonitoringSpecificationMachineDetailsViewModel>()
            {
                new MonitoringSpecificationMachineDetailsViewModel()
            };
            var machine = new MachineViewModel();
            var productionOrder = new ProductionOrderIntegrationViewModel();
            MonitoringSpecificationMachineViewModel viewModel = new MonitoringSpecificationMachineViewModel()
            {
                UId = "UId",
                Code = "Code",
                CartNumber = "CartNumber",
                DateTimeInput = time,
                Machine = machine,
                Details = details,
                ProductionOrder = productionOrder,
            };

            Assert.Equal("UId", viewModel.UId);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("CartNumber", viewModel.CartNumber);
            Assert.Equal(machine, viewModel.Machine);
            Assert.Equal(details, viewModel.Details);
            Assert.Equal(time, viewModel.DateTimeInput);
            Assert.Equal(productionOrder, viewModel.ProductionOrder);

        }

        [Fact]
        public void validate_default()
        {

            MonitoringSpecificationMachineViewModel viewModel = new MonitoringSpecificationMachineViewModel()
            {
               
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_DetailsCount_Empty()
        {

            MonitoringSpecificationMachineViewModel viewModel = new MonitoringSpecificationMachineViewModel()
            {
                Details = new List<MonitoringSpecificationMachineDetailsViewModel>()
                {

                },
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
        [Fact]
        public void validate_Details_when_DataType_skala_angka()
        {

            MonitoringSpecificationMachineViewModel viewModel = new MonitoringSpecificationMachineViewModel()
            {
                Details = new List<MonitoringSpecificationMachineDetailsViewModel>()
                {
                    new MonitoringSpecificationMachineDetailsViewModel()
                    {
                        DataType ="input skala angka",
                        Value = "4",
                        DefaultValue ="4-1"
                    }
                },
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_Details_when_DataType_input_teks()
        {

            MonitoringSpecificationMachineViewModel viewModel = new MonitoringSpecificationMachineViewModel()
            {
                Details = new List<MonitoringSpecificationMachineDetailsViewModel>()
                {
                    new MonitoringSpecificationMachineDetailsViewModel()
                    {
                        DataType ="input teks",
                        Value = "",
                        DefaultValue ="1-4"
                    }
                },
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }


}
