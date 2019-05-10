using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades
{
    //public class ReturToQCFacadeTest : BaseFacadeTest<ProductionDbContext, ReturToQCFacade, ReturToQCLogic, ReturToQCModel, ReturToQCDataUtil>
    //{
    //    private const string ENTITY = "ReturToQC";

    //    public ReturToQCFacadeTest() : base(ENTITY)
    //    {
    //    }

    //    protected override Mock<IServiceProvider> GetServiceProviderMock(ProductionDbContext dbContext)
    //    {
    //        var serviceProviderMock = new Mock<IServiceProvider>();

    //        IIdentityService identityService = new IdentityService { Username = "Username" };

    //        serviceProviderMock
    //            .Setup(x => x.GetService(typeof(IdentityService)))
    //            .Returns(identityService);

    //        serviceProviderMock
    //            .Setup(x => x.GetService(typeof(ReturToQCLogic)))
    //            .Returns(new ReturToQCLogic(identityService, dbContext));

    //        return serviceProviderMock;
    //    }
    //}
}
