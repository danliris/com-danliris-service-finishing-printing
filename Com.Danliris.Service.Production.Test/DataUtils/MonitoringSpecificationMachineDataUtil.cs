using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.MonitoringSpecificationMachine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils
{
    public class MonitoringSpecificationMachineDataUtil : BaseDataUtil<MonitoringSpecificationMachineFacade, MonitoringSpecificationMachineModel>
    {
        public MonitoringSpecificationMachineDataUtil(MonitoringSpecificationMachineFacade facade) : base(facade)
        {
        }

        public override MonitoringSpecificationMachineModel GetNewData()
        {
            MonitoringSpecificationMachineModel model = new MonitoringSpecificationMachineModel
            {
                Details = new List<MonitoringSpecificationMachineDetailsModel>
                {
                    new MonitoringSpecificationMachineDetailsModel()
                }
            };
            return model;
        }
    }
}
