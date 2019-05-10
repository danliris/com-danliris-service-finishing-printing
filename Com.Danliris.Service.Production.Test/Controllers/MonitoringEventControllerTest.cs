using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.MonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Event;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Event;
using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.MonitoringEvent;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class MonitoringEventControllerTest : BaseControllerTest<MonitoringEventController, MonitoringEventModel, MonitoringEventViewModel, IMonitoringEventFacade>
    {
    }
}
