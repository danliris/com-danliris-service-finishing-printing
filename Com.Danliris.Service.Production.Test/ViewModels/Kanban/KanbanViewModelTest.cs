using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Kanban
{
    public class KanbanViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            KanbanViewModel viewModel = new KanbanViewModel()
            {
                UId = "UId",
                BadOutput =1,
                Code = "Code",
                FinishWidth = "FinishWidth",
                IsFulfilledOutput=true
            };
        }

        [Fact]
        public void validate_default()
        {

            KanbanViewModel viewModel = new KanbanViewModel();
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_Instruction_Step_empty()
        {

            KanbanViewModel viewModel = new KanbanViewModel()
            {
                Instruction = new KanbanInstructionViewModel()
                {
                    
                    Id =1,
                    Steps =new List<KanbanStepViewModel>()
                    {
                        
                    }
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_Instruction()
        {

            KanbanViewModel viewModel = new KanbanViewModel()
            {
                Instruction = new KanbanInstructionViewModel()
                {

                    Id = 1,
                    Steps = new List<KanbanStepViewModel>()
                    {
                        new KanbanStepViewModel()
                        {

                        }
                    }
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_CartNumber_Empty()
        {

            KanbanViewModel viewModel = new KanbanViewModel()
            {
                Cart =new CartViewModel()
                {
                    CartNumber =""
                }
                
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }


    }
}
