using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils
{
    public class ReturToQCDataUtil : BaseDataUtil<ReturToQCFacade, ReturToQCModel>
    {
        public ReturToQCDataUtil(ReturToQCFacade facade) : base(facade)
        {
        }

        public override ReturToQCModel GetNewData()
        {
            ReturToQCModel model = new ReturToQCModel
            {
                ReturToQCItems = new List<ReturToQCItemModel>
                {
                    new ReturToQCItemModel
                    {
                        ReturToQCItemDetails = new List<ReturToQCItemDetailModel>
                        {
                            new ReturToQCItemDetailModel()
                        }
                    }
                }
            };
            return model;
        }
    }
}
