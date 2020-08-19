using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.FabricQualityControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.FabricQualityControl
{
    public class FabricQualityControlViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var time = DateTimeOffset.Now;
            var fabricGradeTests = new List<FabricGradeTestViewModel>()
            {
                new FabricGradeTestViewModel()
                {
                
                }
            };
            FabricQualityControlViewModel viewModel = new FabricQualityControlViewModel()
            {
                Active =true,
                Buyer = "Buyer",
                CartNo = "CartNo",
                Code = "Code",
                Color = "Color",
                Construction = "Construction",
                DateIm = time,
                IsUsed =true,
                Group = "Group",
                KanbanCode = "KanbanCode",
                KanbanId =1,
                MachineNoIm = "MachineNoIm",
                OperatorIm = "OperatorIm",
                OrderQuantity =1,
                PackingInstruction = "PackingInstruction",
                PointLimit =1,
                ProductionOrderNo = "ProductionOrderNo",
                ShiftIm = "ShiftIm",
                PointSystem =1,
                UId = "UId",
                ProductionOrderType = "ProductionOrderType",
                Uom = "Uom",
                FabricGradeTests = fabricGradeTests
            };

            Assert.True(viewModel.Active);
            Assert.Equal("Buyer", viewModel.Buyer);
            Assert.Equal("CartNo", viewModel.CartNo);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("Color", viewModel.Color);
            Assert.Equal("Construction", viewModel.Construction);
            Assert.Equal(time, viewModel.DateIm);
            Assert.True(viewModel.IsUsed);
            Assert.Equal("Group", viewModel.Group);
            Assert.Equal(1, viewModel.KanbanId);
            Assert.Equal("MachineNoIm", viewModel.MachineNoIm);
            Assert.Equal("OperatorIm", viewModel.OperatorIm);
            Assert.Equal(1, viewModel.OrderQuantity);
            Assert.Equal("PackingInstruction", viewModel.PackingInstruction);
            Assert.Equal(1, viewModel.PointLimit);
            Assert.Equal("ProductionOrderNo", viewModel.ProductionOrderNo);
            Assert.Equal("ShiftIm", viewModel.ShiftIm);
            Assert.Equal(1, viewModel.PointSystem);
            Assert.Equal("UId", viewModel.UId);
            Assert.Equal("ProductionOrderType", viewModel.ProductionOrderType);
            Assert.Equal("Uom", viewModel.Uom);
            Assert.Equal(fabricGradeTests, viewModel.FabricGradeTests);
        }

        [Fact]
        public void validate_default()
        {

            FabricQualityControlViewModel viewModel = new FabricQualityControlViewModel();
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_PointSystem_Equal_4()
        {
            FabricQualityControlViewModel viewModel = new FabricQualityControlViewModel()
            {
                PointSystem = 4
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_FabricGradeTests_moreThan_1()
        {
            var fabricGradeTests = new List<FabricGradeTestViewModel>()
            {
                new FabricGradeTestViewModel()
                {
                    InitLength= 0,
                    AvalLength =2,
                    SampleLength =2,
                    Width =0,
                    PcsNo ="PcsNo",
                    PointLimit =1,
                    PointSystem =1,
                    Criteria = new List<CriteriaViewModel>()
                    {
                        new CriteriaViewModel()
                        {
                            Code ="Code",
                            Group ="Group",
                            Name ="Name"
                        }
                    }
                }
            };
            FabricQualityControlViewModel viewModel = new FabricQualityControlViewModel()
            {
                FabricGradeTests = fabricGradeTests
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }


        [Fact]
        public void validate_when_SampleLength_moreThan_InitLength()
        {
            var fabricGradeTests = new List<FabricGradeTestViewModel>()
            {
                new FabricGradeTestViewModel()
                {
                    InitLength= 2,
                    AvalLength =2,
                    SampleLength =3,
                    Width =0,
                }
            };
            FabricQualityControlViewModel viewModel = new FabricQualityControlViewModel()
            {
                FabricGradeTests = fabricGradeTests
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_SampleLength_and_avalength_moreThan_InitLength()
        {
            var fabricGradeTests = new List<FabricGradeTestViewModel>()
            {
                new FabricGradeTestViewModel()
                {
                    InitLength= 2,
                    AvalLength =2,
                    SampleLength =1,
                    Width =0,
                }
            };
            FabricQualityControlViewModel viewModel = new FabricQualityControlViewModel()
            {
                FabricGradeTests = fabricGradeTests
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

    }
}
