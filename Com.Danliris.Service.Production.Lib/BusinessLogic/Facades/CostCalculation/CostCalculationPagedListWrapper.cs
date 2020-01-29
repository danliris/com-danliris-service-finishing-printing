using System;
using System.Collections.Generic;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.CostCalculation;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.CostCalculation
{
    public class CostCalculationPagedListWrapper
    {
        public CostCalculationPagedListWrapper()
        {
            Data = new List<CostCalculationPagedListData>();
        }

        public int TotalData { get; set; }
        public List<CostCalculationPagedListData> Data { get; set; }
    }

    public class CostCalculationPagedListData
    {
        public CostCalculationPagedListData(CostCalculationModel entity)
        {
            Id = entity.Id;
            ProductionOrderNo = entity.ProductionOrderNo;
            LastModifiedUtc = entity.LastModifiedUtc;
            InstructionName = entity.InstructionName;
            Date = entity.Date;
            GreigeName = entity.GreigeName;
            BuyerName = entity.BuyerName;
        }

        public int Id { get; set; }
        public string ProductionOrderNo { get; set; }
        public DateTimeOffset LastModifiedUtc { get; set; }
        public string InstructionName { get; set; }
        public DateTimeOffset Date { get; set; }
        public string GreigeName { get; set; }
        public string BuyerName { get; set; }
    }
}