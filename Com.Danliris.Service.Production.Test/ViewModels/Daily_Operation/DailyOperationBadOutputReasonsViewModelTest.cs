using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.BadOutput;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Daily_Operation
{
    public class DailyOperationBadOutputReasonsViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var badOutput = new BadOutputViewModel();
            var machine = new MachineViewModel();
            DailyOperationBadOutputReasonsViewModel viewModel = new DailyOperationBadOutputReasonsViewModel()
            {
                Description = "Description",
                Action = "Action",
                BadOutput = badOutput,
                Machine = machine,
                DailyOperationId =1

            };
            Assert.Equal("Description", viewModel.Description);
            Assert.Equal("Action", viewModel.Action);
            Assert.Equal(badOutput, viewModel.BadOutput);
            Assert.Equal(machine, viewModel.Machine);
            Assert.Equal(1, viewModel.DailyOperationId);
        }
        }
}
