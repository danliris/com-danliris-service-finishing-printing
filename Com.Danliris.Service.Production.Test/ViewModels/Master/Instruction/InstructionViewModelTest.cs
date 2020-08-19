using Com.Danliris.Service.Production.Lib.ViewModels.Master.Instruction;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Step;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Master.Instruction
{
    public class InstructionViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var steps = new List<StepViewModel>()
            {
                new StepViewModel()
            };
            InstructionViewModel viewModel = new InstructionViewModel()
            {
                UId = "UId",
                Code = "Code",
                Name = "Name",
                Steps = steps
            };
            Assert.Equal("UId", viewModel.UId);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("Name", viewModel.Name);
            Assert.Equal(steps, viewModel.Steps);

        }

        [Fact]
        public void validate_default()
        {
            InstructionViewModel viewModel = new InstructionViewModel();
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_Steps()
        {
            var steps = new List<StepViewModel>()
            {
                new StepViewModel()
            };
            InstructionViewModel viewModel = new InstructionViewModel()
            {
                Steps = steps
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }
}
