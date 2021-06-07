using Com.Danliris.Service.Production.Lib.Models.Master.DurationEstimation;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;

namespace Com.Danliris.Service.Production.Lib.BusinessLogic.Interfaces.Master
{
    public interface IDurationEstimationFacade : IBaseFacade<DurationEstimationModel>
    {
        DurationEstimationModel ReadByProcessType(string processTypeCode);
    }
}
