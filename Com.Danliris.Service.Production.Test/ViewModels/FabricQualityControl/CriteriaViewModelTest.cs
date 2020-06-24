using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.FabricQualityControl;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.FabricQualityControl
{
    public class CriteriaViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var score = new Score();
            CriteriaViewModel viewModel = new CriteriaViewModel()
            {
                Id = 1,
                Code = "Code",
                Group = "Group",
                Index = 1,
                Name = "Name",
               Score=score
            };

            Assert.Equal(1, viewModel.Id);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("Group", viewModel.Group);
            Assert.Equal("Name", viewModel.Name);
            Assert.Equal(1, viewModel.Index);
            Assert.Equal(score, viewModel.Score);
        }
            
    }
}
