using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.MachineType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Master.MachineType
{
    public class MachineTypeViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var indicators = new List<MachineTypeIndicatorsViewModel>()
            {
                new MachineTypeIndicatorsViewModel()
            };
            MachineTypeViewModel viewModel = new MachineTypeViewModel()
            {
                UId = "UId",
                Code = "Code",
                Name = "Name",
                Description = "Description",
                Indicators = indicators
            };

            Assert.Equal("UId", viewModel.UId);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("Name", viewModel.Name);
            Assert.Equal("Description", viewModel.Description);
            Assert.Equal(indicators, viewModel.Indicators);
        }

        [Fact]
        public void validate_default()
        {
            var indicators = new List<MachineTypeIndicatorsViewModel>()
            {
                new MachineTypeIndicatorsViewModel()
            };
            MachineTypeViewModel viewModel = new MachineTypeViewModel()
            {
                Indicators = indicators
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_DataType_input_pilihan()
        {
            var indicators = new List<MachineTypeIndicatorsViewModel>()
            {
                new MachineTypeIndicatorsViewModel()
                {
                    DataType ="input pilihan",
                    DefaultValue ="0"
                }
            };
            MachineTypeViewModel viewModel = new MachineTypeViewModel()
            {
                Indicators = indicators
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_DataType_input_skala_angka()
        {
            var indicators = new List<MachineTypeIndicatorsViewModel>()
            {
                new MachineTypeIndicatorsViewModel()
                {
                    DataType ="input skala angka",
                    DefaultValue ="0-2"
                }
            };
            MachineTypeViewModel viewModel = new MachineTypeViewModel()
            {
                Indicators = indicators
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_DataType_input_skala_angka_with_firstInput_greater_than_output()
        {
            var indicators = new List<MachineTypeIndicatorsViewModel>()
            {
                new MachineTypeIndicatorsViewModel()
                {
                    DataType ="input skala angka",
                    DefaultValue ="4-2"
                }
            };
            MachineTypeViewModel viewModel = new MachineTypeViewModel()
            {
                Indicators = indicators
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_DataType_input_skala_angka_with_invalidData()
        {
            var indicators = new List<MachineTypeIndicatorsViewModel>()
            {
                new MachineTypeIndicatorsViewModel()
                {
                    DataType ="input skala angka",
                    DefaultValue ="A-B"
                }
            };
            MachineTypeViewModel viewModel = new MachineTypeViewModel()
            {
                Indicators = indicators
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }
}
