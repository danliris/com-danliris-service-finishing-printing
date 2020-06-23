using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.DirectLaborCost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Master.DirectLaborCost
{
    public class DirectLaborCostViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            DirectLaborCostViewModel viewModel = new DirectLaborCostViewModel()
            {
                Code = "Code",
                Month = 1,
                Year = 2020,
                LaborTotal =1,
                WageTotal =1
            };

            Assert.Equal("Code", viewModel.Code);
            Assert.Equal(1, viewModel.Month);
            Assert.Equal(1, viewModel.Year);
            Assert.Equal(1, viewModel.LaborTotal);
            Assert.Equal(1, viewModel.WageTotal);
        }

        [Fact]
        public void validate_default()
        {

            DirectLaborCostViewModel viewModel = new DirectLaborCostViewModel()
            {
                WageTotal = -1,
                LaborTotal = -1,
                Month = 0,
                Year = 0,
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }
}
