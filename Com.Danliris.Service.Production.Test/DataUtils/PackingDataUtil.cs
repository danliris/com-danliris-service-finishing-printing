using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils
{
    public class PackingDataUtil : BaseDataUtil<PackingFacade, PackingModel>
    {
        public PackingDataUtil(PackingFacade facade) : base(facade)
        {
        }

        public override PackingModel GetNewData()
        {
            PackingModel model = new PackingModel
            {
                PackingDetails = new List<PackingDetailModel>
                {
                    new PackingDetailModel()
                }
            };
            return model;
        }
    }
}
