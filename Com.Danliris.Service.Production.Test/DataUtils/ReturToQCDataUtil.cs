using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
                Destination = "Destination",
                DeliveryOrderNo ="1",
                ReturToQCItems = new List<ReturToQCItemModel>
                {
                    
                    new ReturToQCItemModel
                    {
                        
                        ProductionOrderNo ="1",
                        
                        ReturToQCItemDetails = new List<ReturToQCItemDetailModel>
                        {
                            new ReturToQCItemDetailModel()
                            {
                               Weight=-1
                            }
                        }
                    }
                }
            };
            return model;
        }

       
    }
}
