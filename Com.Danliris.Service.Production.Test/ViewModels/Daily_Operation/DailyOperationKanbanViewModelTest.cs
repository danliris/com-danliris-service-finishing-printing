using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Daily_Operation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Daily_Operation
{
    public class DailyOperationKanbanViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {

            DailyOperationKanbanViewModel viewModel = new DailyOperationKanbanViewModel()
            {
                OrderNo = "OrderNo",
                OrderQuantity =1,
                Color = "Color",
                Area = "Area",
                Machine = "Machine",
                Step = "Step"
            };

            Assert.Equal("OrderNo", viewModel.OrderNo);
            Assert.Equal(1, viewModel.OrderQuantity);
            Assert.Equal("Color", viewModel.Color);
            Assert.Equal("Area", viewModel.Area);
            Assert.Equal("Machine", viewModel.Machine);
            Assert.Equal("Step", viewModel.Step);
        }
        }
    }
