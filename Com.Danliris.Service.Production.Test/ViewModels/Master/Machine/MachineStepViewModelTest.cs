using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Master.Machine
{
    public class MachineStepViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            MachineStepViewModel viewModel = new MachineStepViewModel()
            {
                Alias = "Alias",
                Code = "Code",
                Process = "Process",
                ProcessArea = "ProcessArea",
                MachineId =1,
                StepId =1
            };

            Assert.Equal("Alias", viewModel.Alias);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("Process", viewModel.Process);
            Assert.Equal("ProcessArea", viewModel.ProcessArea);
            Assert.Equal(1, viewModel.MachineId);
            Assert.Equal(1, viewModel.StepId);
        }
        }
}
