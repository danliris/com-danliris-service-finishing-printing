using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.MachineType;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System.Collections.Generic;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils
{
    public class MachineTypeDataUtil : BaseDataUtil<MachineTypeFacade, MachineTypeModel>
    {
        public MachineTypeDataUtil(MachineTypeFacade facade) : base(facade)
        {
        }

        public override MachineTypeModel GetNewData()
        {
            MachineTypeModel model = new MachineTypeModel
            {
                Indicators = new List<MachineTypeIndicatorsModel>
                {
                    new MachineTypeIndicatorsModel()
                }
            };
            return model;
        }
    }
}
