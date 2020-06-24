using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.BadOutput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Master.BadOutput
{
    public class BadOutputViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var machineDetails = new List<BadOutputMachineViewModel>()
            {
                new BadOutputMachineViewModel()
            };
            BadOutputViewModel viewModel = new BadOutputViewModel()
            {
                UId = "UId",
                Code = "Code",
                Reason = "Reason",
                MachineDetails = machineDetails
            };

            Assert.Equal("UId", viewModel.UId);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("Reason", viewModel.Reason);
            Assert.Equal(machineDetails, viewModel.MachineDetails);
        }

        [Fact]
        public void validate_default()
        {

            BadOutputViewModel viewModel = new BadOutputViewModel();
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }
}
