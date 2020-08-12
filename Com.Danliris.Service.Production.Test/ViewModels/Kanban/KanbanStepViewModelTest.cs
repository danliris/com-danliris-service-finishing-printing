using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Step;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Kanban
{
    public class KanbanStepViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            KanbanStepViewModel viewModel = new KanbanStepViewModel()
            {
                UId = "UId",
                Alias = "Alias",
                Code = "Code",
                Deadline = DateTimeOffset.Now,
                Machine = new MachineViewModel(),
                MachineId =1,
                Process = "Process",
                ProcessArea = "ProcessArea",
                SelectedIndex =1,
                StepIndex =1,
                StepIndicators =new List<StepIndicatorViewModel>()
                {
                    new StepIndicatorViewModel()
                }
            };

            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("Alias", viewModel.Alias);
            Assert.Equal("Code", viewModel.Code);
            Assert.True(DateTimeOffset.MinValue < viewModel.Deadline);
            Assert.NotNull(viewModel.Machine);
            Assert.Equal(1, viewModel.MachineId);
            Assert.Equal("Process", viewModel.Process);
            Assert.Equal("ProcessArea", viewModel.ProcessArea);
            Assert.Equal(1, viewModel.SelectedIndex);
            Assert.Equal(1, viewModel.StepIndex);
            Assert.NotNull(viewModel.StepIndicators);
        }
    }
}
