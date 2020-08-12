using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ColorReceipt;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.Lib.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Helpers
{
    public class ValidateServiceTest
    {
        [Fact]
        public void Validate_Throws_ServiceValidationExeption()
        {
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            TechnicianViewModel viewModel = new TechnicianViewModel();

            ValidateService service = new ValidateService(serviceProvider.Object);
            Assert.Throws<ServiceValidationException>(() => service.Validate(viewModel));

        }
    }
}
