using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Master.Machine
{
    public class MachineViewModelTest
    {
        [Fact]
        public void validate_default()
        {
            MachineViewModel viewModel = new MachineViewModel();
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }
}
