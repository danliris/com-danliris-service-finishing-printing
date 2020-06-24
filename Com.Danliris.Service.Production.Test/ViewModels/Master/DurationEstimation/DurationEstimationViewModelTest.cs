using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.DurationEstimation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Master.DurationEstimation
{
    public class DurationEstimationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var processType = new ProcessTypeIntegrationViewModel();
            var areas = new List<DurationEstimationAreaViewModel>()
            {
                new DurationEstimationAreaViewModel()
            };
            DurationEstimationViewModel viewModel = new DurationEstimationViewModel()
            {
                UId = "UId",
                Code = "Code",
                ProcessType = processType,
                Areas =areas
            };

            Assert.Equal("UId", viewModel.UId);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal(processType, viewModel.ProcessType);
            Assert.Equal(areas, viewModel.Areas);

        }

        [Fact]
        public void validate_default()
        {
            DurationEstimationViewModel viewModel = new DurationEstimationViewModel();
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_Areas()
        {
            var areas = new List<DurationEstimationAreaViewModel>()
            {
                new DurationEstimationAreaViewModel()
                {
                    
                }
            };
            DurationEstimationViewModel viewModel = new DurationEstimationViewModel()
            {
                Areas = areas,
                
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_Areas_DuplicateName()
        {
            var areas = new List<DurationEstimationAreaViewModel>()
            {
                new DurationEstimationAreaViewModel()
                {
                    Duration =0,
                    Name ="Name"
                },
                new DurationEstimationAreaViewModel()
                {
                    Name ="Name"
                }
            };
            DurationEstimationViewModel viewModel = new DurationEstimationViewModel()
            {
                Areas = areas,

            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }
}
