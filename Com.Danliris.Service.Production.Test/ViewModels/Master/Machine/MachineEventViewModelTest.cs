using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Master.Machine
{
    public class MachineEventViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            MachineEventViewModel viewModel = new MachineEventViewModel()
            {
                Code = "Code",
                Name = "Name",
                No = "No",
                Category = "Category"

            };

            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("Name", viewModel.Name);
            Assert.Equal("No", viewModel.No);
            Assert.Equal("Category", viewModel.Category);
        }

        [Fact]
        public void validate_Throws_NotImplementedException()
        {
            MachineEventViewModel viewModel = new MachineEventViewModel();
            Assert.Throws<NotImplementedException>(() => viewModel.Validate(null));
        }
    }
}
