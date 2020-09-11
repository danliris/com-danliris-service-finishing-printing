using System.ComponentModel.DataAnnotations;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ColorReceipt;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Com.Danliris.Service.Production.Lib.Utilities;
using Xunit;
using AutoMapper;
using System.Linq;

namespace Com.Danliris.Service.Finishing.Printing.Test.Helpers
{
    public class ResultFormatterTest
    {
        [Fact]
        public void ResultFormatterSuccess()
        {
            //Settup
            ResultFormatter resultFormatter = new ResultFormatter("1", 200, "ok");
            var mapper = new Mock<IMapper>();
            List<TechnicianModel> data = new List<TechnicianModel>();
            Dictionary<string, string> orderDictionary = new Dictionary<string, string>();
            orderDictionary.Add("Name", "desc");

            //Act
            var result = resultFormatter.Ok<TechnicianModel>(mapper.Object, data, 1, 1, 1, 1, orderDictionary, new List<string>() { "Name" });

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Fail_Return_Success()
        {
            //Setup
            string ApiVersion = "V1";
            int StatusCode = 200;
            string Message = "OK";

            TechnicianViewModel viewModel = new TechnicianViewModel();
            ResultFormatter formatter = new ResultFormatter(ApiVersion, StatusCode, Message);
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(viewModel);

            var errorData = new
            {
                WarningError = "Format Not Match"
            };

            string error = JsonConvert.SerializeObject(errorData);
            var exception = new ServiceValidationException(validationContext, new List<ValidationResult>() { new ValidationResult(error, new List<string>() { "WarningError" }) });

            //Act
            var result = formatter.Fail(exception);

            //Assert
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void Fail_Throws_Exception()
        {
            //Setup
            string ApiVersion = "V1";
            int StatusCode = 200;
            string Message = "OK";

            TechnicianViewModel viewModel = new TechnicianViewModel();
            ResultFormatter formatter = new ResultFormatter(ApiVersion, StatusCode, Message);
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(viewModel);
            var exception = new ServiceValidationException(validationContext, new List<ValidationResult>() { new ValidationResult("errorMessaage", new List<string>() { "WarningError" }) });

            //Act
            var result = formatter.Fail(exception);

            //Assert
            Assert.True(0 < result.Count());
        }
    }
}
