using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.CostCalculation;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.CostCalculation;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.CostCalculation;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.CostCalculation;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class CostCalculationControllerTest
    {
        private int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        private ServiceValidationException GetServiceValidationExeption()
        {
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            List<ValidationResult> validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(new CostCalculationViewModel(), serviceProvider.Object, null);
            return new ServiceValidationException(validationContext, validationResults);
        }

        private CostCalculationController GetController(IIdentityService identityService, IValidateService validateService, ICostCalculationService service)
        {

            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);
            CostCalculationController controller = (CostCalculationController)Activator.CreateInstance(typeof(CostCalculationController), identityService, validateService, service);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = user.Object
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            return controller;
        }

        [Fact]
        public async Task Should_Succes_Get_List()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var validateServiceMock = new Mock<IValidateService>();
            var serviceMock = new Mock<ICostCalculationService>();
            serviceMock
                .Setup(service => service.GetPaged(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new CostCalculationPagedListWrapper());

            var controller = new CostCalculationController(identityServiceMock.Object, validateServiceMock.Object, serviceMock.Object);
            var response = await controller.Get(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            controller.Dispose();
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Internal_Server_Error_Get_List_Throw_Exception()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var validateServiceMock = new Mock<IValidateService>();
            var serviceMock = new Mock<ICostCalculationService>();

            var controller = new CostCalculationController(identityServiceMock.Object, validateServiceMock.Object, serviceMock.Object);
            var response = await controller.Get(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            controller.Dispose();
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_Get_Single_By_Id()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var validateServiceMock = new Mock<IValidateService>();
            var serviceMock = new Mock<ICostCalculationService>();
            serviceMock
                .Setup(service => service.GetSingleById(It.IsAny<int>()))
                .ReturnsAsync(new CostCalculationViewModel());

            var controller = new CostCalculationController(identityServiceMock.Object, validateServiceMock.Object, serviceMock.Object);
            var response = await controller.Get(It.IsAny<int>());
            controller.Dispose();
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Not_Found_Get_Single_By_Id_Get_Null()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var validateServiceMock = new Mock<IValidateService>();
            var serviceMock = new Mock<ICostCalculationService>();
            serviceMock
                .Setup(service => service.GetSingleById(It.IsAny<int>()))
                .ReturnsAsync((CostCalculationViewModel)null);

            var controller = new CostCalculationController(identityServiceMock.Object, validateServiceMock.Object, serviceMock.Object);
            var response = await controller.Get(It.IsAny<int>());
            controller.Dispose();
            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Internal_Server_Error_Get_Single_By_Id_With_Exception()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var validateServiceMock = new Mock<IValidateService>();
            var serviceMock = new Mock<ICostCalculationService>();
            serviceMock
                .Setup(service => service.GetSingleById(It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            var controller = new CostCalculationController(identityServiceMock.Object, validateServiceMock.Object, serviceMock.Object);
            var response = await controller.Get(It.IsAny<int>());
            controller.Dispose();
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_Post_Data()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(service => service.Validate(It.IsAny<CostCalculationViewModel>()))
                .Verifiable();

            var serviceMock = new Mock<ICostCalculationService>();
            serviceMock
                .Setup(service => service.InsertSingle(It.IsAny<CostCalculationModel>()))
                .ReturnsAsync(1);

            var controller = GetController(identityServiceMock.Object, validateServiceMock.Object, serviceMock.Object);
            var response = await controller.Post(new CostCalculationViewModel() { Machines = new List<CostCalculationMachineViewModel>() { new CostCalculationMachineViewModel() { Chemicals = new List<CostCalculationChemicalViewModel>() { new CostCalculationChemicalViewModel() } } } });
            controller.Dispose();
            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Bad_Request_Post_Data_Throws_Validation_Exception()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(service => service.Validate(It.IsAny<CostCalculationViewModel>()))
                .Throws(GetServiceValidationExeption());

            var serviceMock = new Mock<ICostCalculationService>();

            var controller = GetController(identityServiceMock.Object, validateServiceMock.Object, serviceMock.Object);
            var response = await controller.Post(It.IsAny<CostCalculationViewModel>());
            controller.Dispose();
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Internal_Server_Error_Post_Data_Throws_Exception()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(service => service.Validate(It.IsAny<CostCalculationViewModel>()))
                .Verifiable();

            var serviceMock = new Mock<ICostCalculationService>();
            serviceMock
                .Setup(service => service.InsertSingle(It.IsAny<CostCalculationModel>()))
                .ThrowsAsync(new Exception());

            var controller = GetController(identityServiceMock.Object, validateServiceMock.Object, serviceMock.Object);
            var response = await controller.Post(It.IsAny<CostCalculationViewModel>());
            controller.Dispose();
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_Put_Data()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(service => service.Validate(It.IsAny<CostCalculationViewModel>()))
                .Verifiable();

            var serviceMock = new Mock<ICostCalculationService>();
            serviceMock
                .Setup(service => service.UpdateSingle(It.IsAny<int>(), It.IsAny<CostCalculationViewModel>()))
                .ReturnsAsync(1);

            serviceMock
                .Setup(service => service.IsDataExistsById(It.IsAny<int>()))
                .ReturnsAsync(true);

            var controller = GetController(identityServiceMock.Object, validateServiceMock.Object, serviceMock.Object);
            var response = await controller.Put(1, new CostCalculationViewModel() { Id = 1 });
            controller.Dispose();
            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Not_Found_Put_Data_Not_Exist()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var validateServiceMock = new Mock<IValidateService>();
            var serviceMock = new Mock<ICostCalculationService>();

            serviceMock
                .Setup(service => service.IsDataExistsById(It.IsAny<int>()))
                .ReturnsAsync(false);

            var controller = GetController(identityServiceMock.Object, validateServiceMock.Object, serviceMock.Object);
            var response = await controller.Put(1, new CostCalculationViewModel() { Id = 1 });
            controller.Dispose();
            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Bad_Request_Put_Data_Throws_Validation_Exception()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(service => service.Validate(It.IsAny<CostCalculationViewModel>()))
                .Throws(GetServiceValidationExeption());

            var serviceMock = new Mock<ICostCalculationService>();
            serviceMock
                .Setup(service => service.IsDataExistsById(It.IsAny<int>()))
                .ReturnsAsync(true);

            var controller = GetController(identityServiceMock.Object, validateServiceMock.Object, serviceMock.Object);
            var response = await controller.Put(1, new CostCalculationViewModel() { Id = 1 });
            controller.Dispose();
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Internal_Server_Error_Put_Data_Throws_Exception()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock
                .Setup(service => service.Validate(It.IsAny<CostCalculationViewModel>()))
                .Verifiable();

            var serviceMock = new Mock<ICostCalculationService>();
            serviceMock
                .Setup(service => service.IsDataExistsById(It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            var controller = GetController(identityServiceMock.Object, validateServiceMock.Object, serviceMock.Object);
            var response = await controller.Put(1, new CostCalculationViewModel() { Id = 1 });
            controller.Dispose();
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Success_Delete_Data()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var validateServiceMock = new Mock<IValidateService>();
            var serviceMock = new Mock<ICostCalculationService>();
            serviceMock
                .Setup(service => service.DeleteSingle(It.IsAny<int>()))
                .ReturnsAsync(1);

            serviceMock
                .Setup(service => service.IsDataExistsById(It.IsAny<int>()))
                .ReturnsAsync(true);

            var controller = GetController(identityServiceMock.Object, validateServiceMock.Object, serviceMock.Object);
            var response = await controller.Delete(It.IsAny<int>());
            controller.Dispose();
            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Throws_InternalServerError_Delete_Data()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var validateServiceMock = new Mock<IValidateService>();
            var serviceMock = new Mock<ICostCalculationService>();
            serviceMock
                .Setup(service => service.DeleteSingle(It.IsAny<int>()))
                .Throws(new Exception());

            serviceMock
                .Setup(service => service.IsDataExistsById(It.IsAny<int>()))
                .ReturnsAsync(true);

            var controller = GetController(identityServiceMock.Object, validateServiceMock.Object, serviceMock.Object);
            var response = await controller.Delete(It.IsAny<int>());
            controller.Dispose();
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Not_Found_Delete_Data_Not_Exist()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var validateServiceMock = new Mock<IValidateService>();
            var serviceMock = new Mock<ICostCalculationService>();

            serviceMock
                .Setup(service => service.IsDataExistsById(It.IsAny<int>()))
                .ReturnsAsync(false);

            var controller = GetController(identityServiceMock.Object, validateServiceMock.Object, serviceMock.Object);
            var response = await controller.Delete(It.IsAny<int>());
            controller.Dispose();
            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async Task Should_Internal_Server_Error_Delete_Data_Throws_Exception()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var validateServiceMock = new Mock<IValidateService>();

            var serviceMock = new Mock<ICostCalculationService>();
            serviceMock
                .Setup(service => service.IsDataExistsById(It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            var controller = GetController(identityServiceMock.Object, validateServiceMock.Object, serviceMock.Object);
            var response = await controller.Put(1, new CostCalculationViewModel() { Id = 1 });
            controller.Dispose();
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
