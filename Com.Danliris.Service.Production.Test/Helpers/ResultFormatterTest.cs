using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ColorReceipt;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Helpers
{
    public class ResultFormatterTest
    {
        [Fact]
        public void ResultFormatterSuccess()
        {
            ResultFormatter resultFormatter = new ResultFormatter("1", 200, "ok");
            var mapper = new Mock<IMapper>();
            List<TechnicianModel> data = new List<TechnicianModel>();
            Dictionary<string, string> orderDictionary = new Dictionary<string, string>();
            orderDictionary.Add("Name", "desc");
           var result =  resultFormatter.Ok<TechnicianModel>(mapper.Object, data, 1, 1, 1, 1, orderDictionary, new List<string>() { "Name"});
            Assert.NotNull(result);
        }
    }
}
