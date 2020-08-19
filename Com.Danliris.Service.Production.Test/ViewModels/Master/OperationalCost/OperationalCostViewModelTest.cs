using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.OperationalCost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Master.OperationalCost
{
    public class OperationalCostViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            OperationalCostViewModel viewModel = new OperationalCostViewModel()
            {
                Code = "Code",
                Month = 1,
                Year =2020
            };

            Assert.Equal("Code", viewModel.Code);
            Assert.Equal(1, viewModel.Month);
            Assert.Equal(2020, viewModel.Year);
        }

        [Fact]
        public void validate_default()
        {
            OperationalCostViewModel viewModel = new OperationalCostViewModel();
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }
}
