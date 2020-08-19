using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ReturToQC;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.ReturToQC
{
    public class ReturToQCItemViewIModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {

        }

        [Fact]
        public void validate_default()
        {

            ReturToQCItemViewModel viewModel = new ReturToQCItemViewModel()
            {
                ProductionOrder = null,
                Details = new List<ReturToQCItemDetailViewModel>()
                {
                 
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_ProductionOrder()
        {

            ReturToQCItemViewModel viewModel = new ReturToQCItemViewModel()
            {
                
                ProductionOrder = new ProductionOrderIntegrationViewModel()
                {
                    Id =0,
                    
                },
                Details = new List<ReturToQCItemDetailViewModel>()
                {
                    new ReturToQCItemDetailViewModel()
                    {
                        QuantityBefore =1,
                        ReturQuantity =0
                    }
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }
}
