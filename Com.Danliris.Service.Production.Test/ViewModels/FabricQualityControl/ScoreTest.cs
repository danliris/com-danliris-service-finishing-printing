using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.FabricQualityControl;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.FabricQualityControl
{
    public class ScoreTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            Score score = new Score()
            {
                A=1,
                B =1,
                C=1,
                D =1
            };
            Assert.Equal(1, score.A);
            Assert.Equal(1, score.B);
            Assert.Equal(1, score.C);
            Assert.Equal(1, score.D);
        }
        }
}
