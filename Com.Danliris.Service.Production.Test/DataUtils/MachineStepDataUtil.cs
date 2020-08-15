using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Production.Lib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils
{
   public class MachineStepDataUtil
    {
        ProductionDbContext _dbContext;
        public MachineStepDataUtil(ProductionDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public MachineStepModel GetNewdata()
        {
            return new MachineStepModel()
            {
                StepId =1,
                Alias = "Alias",
                Machine =new MachineModel(),
                Process = "Process",
                ProcessArea ="Dyieng"
            };
        }

        public MachineStepModel GetTestData()
        {
            MachineStepModel data = GetNewdata();
            _dbContext.MachineSteps.Add(data);
            return data;
        }

    }
}
