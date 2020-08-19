using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.BadOutput;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Master.BadOutput
{
    public class BadOutputMachineViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            BadOutputMachineViewModel viewModel = new BadOutputMachineViewModel()
            {
                MachineId =1,
                Name = "Name",
                Code = "Code",
                BadOutputId =1,
            };

            Assert.Equal(1, viewModel.MachineId);
            Assert.Equal("Name", viewModel.Name);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal(1, viewModel.BadOutputId);
        }
        }
}
