
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ColorReceipt;
using Com.Danliris.Service.Production.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Helpers
{
   public class QueryHelperTest
    {
        [Fact]
        public void Filter_Success()
        {
            var query = new List<TechnicianModel>()
            {
                new TechnicianModel()
                {
                    Name ="Name"
                }
            }.AsQueryable();

            Dictionary<string, object> filterDictionary = new Dictionary<string, object>();
            filterDictionary.Add("Name", "Name");

            var result = QueryHelper<TechnicianModel>.Filter(query, filterDictionary);
            Assert.NotNull(result);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void Order_Success()
        {
            var query = new List<TechnicianModel>()
            {
                new TechnicianModel()
                {
                    Name ="Name"
                }
            }.AsQueryable();

            Dictionary<string, string> orderDictionary = new Dictionary<string, string>();
            orderDictionary.Add("Name", "desc");
            var result = QueryHelper<TechnicianModel>.Order(query, orderDictionary);
            Assert.True(0 < result.Count());
            Assert.NotNull(result);

        }

        [Fact]
        public void Search_Success()
        {
            var query = new List<TechnicianModel>()
            {
                new TechnicianModel()
                {
                    Name ="name",

                }
            }.AsQueryable();

            List<string> searchAttributes = new List<string>()
            {
                "Name"
            };
            Dictionary<string, string> orderDictionary = new Dictionary<string, string>();
           
            var result = QueryHelper<TechnicianModel>.Search(query, searchAttributes, "", true);
            Assert.NotNull(result);
            Assert.True(0 < result.Count());
        }




    }
}
