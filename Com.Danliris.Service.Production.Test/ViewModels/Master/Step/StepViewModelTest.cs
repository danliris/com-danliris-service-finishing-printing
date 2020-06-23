using Com.Danliris.Service.Production.Lib.ViewModels.Master.Step;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Master.Step
{
    public class StepViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            List<StepIndicatorViewModel> stepIndicators = new List<StepIndicatorViewModel>()
            {
                new StepIndicatorViewModel()
            };

            StepViewModel viewModel = new StepViewModel()
            {
                Code = "Code",
                UId = "UId",
                Alias = "Alias",
                Process = "Process",
                ProcessArea = "ProcessArea",
                StepIndicators = stepIndicators,
                LastModifiedBy ="someone",
                CreatedBy ="someone",
                LastModifiedAgent ="someone"
                
            };

            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("UId", viewModel.UId);
            Assert.Equal("Alias", viewModel.Alias);
            Assert.Equal("ProcessArea", viewModel.ProcessArea);
            Assert.Equal(stepIndicators, viewModel.StepIndicators);
        }

        [Fact]
        public void validate_default()
        {

            StepViewModel viewModel = new StepViewModel();
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_StepIndicator()
        {
            List<StepIndicatorViewModel> stepIndicators = new List<StepIndicatorViewModel>()
            {
                new StepIndicatorViewModel()
            };
            StepViewModel viewModel = new StepViewModel()
            {
                StepIndicators = stepIndicators
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }
}
