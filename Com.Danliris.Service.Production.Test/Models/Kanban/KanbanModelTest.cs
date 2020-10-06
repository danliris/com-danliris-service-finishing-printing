using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Models.Kanban
{
  public  class KanbanModelTest
    {

        [Fact]
        public void Should_Throws_NotImplementedException_When_Validate()
        {
            KanbanModel model = new KanbanModel();

            Assert.Throws<NotImplementedException>(() => model.Validate(null));

        }
    }
}
