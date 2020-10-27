using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.EventOrganizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Master.EventOrganizer
{
  public  class EventOrganizerViewModelTest
    {
        [Fact]
        public void Should_Have_No_Error()
        {
            EventOrganizerViewModel viewModel = new EventOrganizerViewModel()
            {
                Code= "Code",
                Kasie= "Kasie",
                Kasubsie= "Kasubsie",
                ProcessArea= "ProcessArea",
                Group= "Group"

            };
            var result= viewModel.Validate(null);
            Assert.True(0 == result.Count());
           
        }

        [Fact]
        public void Should_Have_Error()
        {
            EventOrganizerViewModel viewModel = new EventOrganizerViewModel();
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());

        }
    }
}
