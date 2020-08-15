using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Specification_Machine;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Monitoring_Specification_Machine
{
  public  class MonitoringSpecificationMachineDetailsViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            
            MonitoringSpecificationMachineDetailsViewModel viewModel = new MonitoringSpecificationMachineDetailsViewModel()
            {
               Indicator = "Indicator",
                Uom = "Uom",
                MonitoringSpecificationMachineId =1
            };

            Assert.Equal("Indicator", viewModel.Indicator);
            Assert.Equal("Uom", viewModel.Uom);
            Assert.Equal(1, viewModel.MonitoringSpecificationMachineId);
        }

        [Fact]
        public void validate_Throws_NotImplementedException()
        {
            MonitoringSpecificationMachineDetailsViewModel viewModel = new MonitoringSpecificationMachineDetailsViewModel();

            Assert.Throws<NotImplementedException>(() => viewModel.Validate(null));
        }
    }
}
