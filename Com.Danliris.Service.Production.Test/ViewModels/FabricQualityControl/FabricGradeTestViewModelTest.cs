using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.FabricQualityControl;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.FabricQualityControl
{
    public class FabricGradeTestViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var criteria = new List<CriteriaViewModel>()
            {
                new CriteriaViewModel()
            };
            FabricGradeTestViewModel viewModel = new FabricGradeTestViewModel()
            {
                Criteria = criteria,
                FabricGradeTest = 1,
                FinalArea =1,
                FinalGradeTest =1,
                FinalLength =1,
                FinalScore =1,
                Grade ="A",
                PointLimit =1,
                PointSystem =1,
                Score =1,
                Type = "Type"
            };
            Assert.Equal(criteria, viewModel.Criteria);
            Assert.Equal(1, viewModel.FabricGradeTest);
            Assert.Equal(1, viewModel.FinalArea);
            Assert.Equal(1, viewModel.FinalGradeTest);
            Assert.Equal(1, viewModel.FinalLength);
            Assert.Equal(1, viewModel.FinalScore);
            Assert.Equal("A", viewModel.Grade);
            Assert.Equal(1, viewModel.PointLimit);
            Assert.Equal(1, viewModel.PointSystem);
            Assert.Equal(1, viewModel.Score);
            Assert.Equal("Type", viewModel.Type);
        }
        }
}
