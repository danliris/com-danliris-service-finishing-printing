using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.StrikeOff;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.StrikeOff
{
    public class StrikeOffConsumptionViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var strikeOffItems = new List<StrikeOffConsumptionItemViewModel>()
                {
                    new StrikeOffConsumptionItemViewModel()
                };
            StrikeOffConsumptionViewModel viewModel = new StrikeOffConsumptionViewModel()
            {
                Code = "Code",
                Remark = "Remark",
                Type = "Type",
                Cloth = "Cloth",
                StrikeOffItems = strikeOffItems
            };

            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("Remark", viewModel.Remark);
            Assert.Equal("Type", viewModel.Type);
            Assert.Equal("Cloth", viewModel.Cloth);
            Assert.Equal(strikeOffItems, viewModel.StrikeOffItems);
        }

        [Fact]
        public void Should_Success_StrikeOffConsumptionDetailViewModel()
        {
            StrikeOffConsumptionDetailViewModel viewModel = new StrikeOffConsumptionDetailViewModel()
            {
                Name = "Name",
                Quantity = 1
            };
            Assert.Equal("Name", viewModel.Name);
            Assert.Equal(1, viewModel.Quantity);
        }

        [Fact]
        public void Should_Success_StrikeOffConsumptionItemViewModel()
        {
            var strikeOffItemDetails = new List<StrikeOffConsumptionDetailViewModel>();
            StrikeOffConsumptionItemViewModel viewModel = new StrikeOffConsumptionItemViewModel()
            {
                ColorCode = "ColorCode",
                StrikeOffItemDetails = strikeOffItemDetails
            };

            Assert.Equal("ColorCode", viewModel.ColorCode);
            Assert.Equal(strikeOffItemDetails, viewModel.StrikeOffItemDetails);
        }

            
    }
}
