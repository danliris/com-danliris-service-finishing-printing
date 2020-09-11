using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Kanban
{
    public class KanbanCreateViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var carts = new List<CartViewModel>() {  
            
                new CartViewModel()
                {
                    Code ="Code",
                    CartNumber ="CartNumber",
                    Pcs=1,
                    Qty =1,
                    Uom =new UOMIntegrationViewModel()
                    {
                        Unit ="Unit"
                    }
                }
            };

         

            var instruction = new KanbanInstructionViewModel();
            var productionOrder = new ProductionOrderIntegrationViewModel();
            var selectedProductionOrderDetail = new ProductionOrderDetailIntegrationViewModel();
            KanbanCreateViewModel viewModel = new KanbanCreateViewModel()
            {
                GoodOutput =1,
                BadOutput=1,
                Code = "Code",
                Carts = carts,
                CurrentQty =1,
                Instruction = instruction,
                IsBadOutput =true,
                Grade = "Grade",
                IsComplete =true,
                IsInactive =false,
                IsReprocess =true,
                OldKanbanId =1,
                ProductionOrder = productionOrder,
                SelectedProductionOrderDetail =selectedProductionOrderDetail,
                CurrentStepIndex =1
            };
            Assert.Equal(1, viewModel.BadOutput);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal(carts, viewModel.Carts);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal(1, viewModel.CurrentQty);
            Assert.Equal(1, viewModel.CurrentStepIndex);
            Assert.Equal(1, viewModel.GoodOutput);
            Assert.Equal("Grade", viewModel.Grade);
            Assert.Equal(instruction, viewModel.Instruction);
            Assert.True(viewModel.IsBadOutput);
            Assert.True(viewModel.IsComplete);
            Assert.False(viewModel.IsInactive);
            Assert.True(viewModel.IsReprocess);
            Assert.Equal("Grade", viewModel.Grade);
            Assert.Equal(selectedProductionOrderDetail, viewModel.SelectedProductionOrderDetail);
        }

        [Fact]
        public void validate_default()
        {

            KanbanCreateViewModel viewModel = new KanbanCreateViewModel();
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }


        [Fact]
        public void validate_when_Carts_moreThan_1()
        {
            var carts = new List<CartViewModel>() {

                new CartViewModel()
                {
                    Code ="Code",
                    CartNumber ="CartNumber",
                    Pcs=1,
                    Qty =1,

                    Uom =new UOMIntegrationViewModel()
                    {
                        Unit ="Unit"
                    }
                }
            };
            KanbanCreateViewModel viewModel = new KanbanCreateViewModel()
            {
                Instruction =new KanbanInstructionViewModel()
                {
                  
                },
                Carts = carts
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_Instruction_notNull()
        {
            var carts = new List<CartViewModel>() {

                new CartViewModel()
                {
                    Code ="Code",
                    CartNumber ="CartNumber",
                    Pcs=1,
                    Qty =1,

                    Uom =new UOMIntegrationViewModel()
                    {
                        Unit ="Unit"
                    }
                }
            };
            KanbanCreateViewModel viewModel = new KanbanCreateViewModel()
            {
                Instruction = new KanbanInstructionViewModel()
                {
                    Name = "Name"
                },
                Carts = carts
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_InstructionSteps_notNull()
        {
            var carts = new List<CartViewModel>() {

                new CartViewModel()
                {
                    Code ="Code",
                    CartNumber ="",
                    Pcs=1,
                    Qty =1,

                    Uom =new UOMIntegrationViewModel()
                    {
                        Unit ="Unit"
                    },
                 
                }
            };
            KanbanCreateViewModel viewModel = new KanbanCreateViewModel()
            {
                Instruction = new KanbanInstructionViewModel()
                {
                    Code = "Code",
                    Name = "Name",
                    Steps = new List<KanbanStepViewModel>()
                    {
                        new KanbanStepViewModel()
                        {
                            
                        }
                    }
                },
                Carts = carts
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }
}
