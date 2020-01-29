using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DOSales;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Utilities
{
    public class AutoMapperTest
    {
        public AutoMapperTest()
        {
        }

        [Fact]
        public void Should_Success_Map_DOSales()
        {
            var mapper = new MapperConfiguration(configuration => configuration.AddProfile<DOSalesProfile>()).CreateMapper();
            var model = new DOSalesModel();
            var vm = mapper.Map<DOSalesViewModel>(model);
            Assert.True(true);
        }
    }
}
