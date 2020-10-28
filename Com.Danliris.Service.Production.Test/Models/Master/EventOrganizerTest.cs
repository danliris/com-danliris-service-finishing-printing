using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.EventOrganizer;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Models.Master
{
   public class EventOrganizerTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            EventOrganizer model = new EventOrganizer()
            {
                Code= "Code",
                Group= "Group",
                ProcessArea= "ProcessArea",
                Kasie= "Kasie",
                Kasubsie= "Kasubsie"
            };
        }

        [Fact]
        public void Should_Throws_NotImplemetedException()
        {
            EventOrganizer model = new EventOrganizer();
            Assert.Throws<NotImplementedException>(() => model.Validate(null));
        }
    }
}
