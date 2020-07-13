using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.FabricQualityControl;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.FabricQualityControl
{
    public class FabricQCGradeTestsViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            FabricQCGradeTestsViewModel viewModel = new FabricQCGradeTestsViewModel()
            {
                Grade ="A",
                OrderNo ="1",
                OrderQuantity=1
            };

            Assert.Equal(1,viewModel.OrderQuantity);
            Assert.Equal("1", viewModel.OrderNo);
            Assert.Equal("A", viewModel.Grade);
        }
            
    }
}
