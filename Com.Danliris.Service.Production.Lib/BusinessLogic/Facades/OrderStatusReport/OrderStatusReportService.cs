using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.OrderStatusReport;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService;
using Com.Danliris.Service.Finishing.Printing.Lib.Utilities;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.OrderStatusReports;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.OrderStatusReport
{
    public class OrderStatusReportService : IOrderStatusReportService
    {
        private readonly ProductionDbContext _dbContext;
        private readonly DbSet<KanbanModel> _kanbanDbSet;
        private readonly DbSet<DailyOperationModel> _dailyOperationDbSet;
        private readonly DbSet<PackingModel> _packingDbSet;
        private readonly DbSet<PackingReceiptModel> _packingReceiptDbSet;
        private readonly DbSet<PackingReceiptItem> _packingReceiptItemDbSet;
        private readonly DbSet<ShipmentDocumentModel> _shipmentDbSet;
        private readonly DbSet<ShipmentDocumentDetailModel> _shipmentDetailDbSet;
        private readonly DbSet<ShipmentDocumentItemModel> _shipmentItemDbSet;
        private readonly DbSet<ShipmentDocumentPackingReceiptItemModel> _shipmentPackingReceiptItem;
        //private readonly IIdentityService _identityService;
        private readonly IServiceProvider _serviceProvider;

        public OrderStatusReportService(ProductionDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;

            _kanbanDbSet = dbContext.Set<KanbanModel>();
            _dailyOperationDbSet = dbContext.Set<DailyOperationModel>();

            _packingDbSet = dbContext.Set<PackingModel>();
            _packingReceiptDbSet = dbContext.Set<PackingReceiptModel>();
            _packingReceiptItemDbSet = dbContext.Set<PackingReceiptItem>();

            _shipmentDbSet = dbContext.Set<ShipmentDocumentModel>();
            _shipmentDetailDbSet = dbContext.Set<ShipmentDocumentDetailModel>();
            _shipmentItemDbSet = dbContext.Set<ShipmentDocumentItemModel>();
            _shipmentPackingReceiptItem = dbContext.Set<ShipmentDocumentPackingReceiptItemModel>();

            _serviceProvider = serviceProvider;
        }

        public async Task<List<MonthlyOrderStatusReportViewModel>> GetMonthlyOrderStatusReport(int year, int month, int orderTypeId)
        {
            var monthlyOrders = await GetMonthlyOrderQuantity(year, month, orderTypeId);

            var result = new List<MonthlyOrderStatusReportViewModel>();

            var tasks = monthlyOrders.Select(async monthlyOrder =>
            {
                var monthlyOrderStatus = new MonthlyOrderStatusReportViewModel()
                {
                    accountName = monthlyOrder.accountName,
                    afterProductionQuantity = await GetAfterProductionQuantity(new List<int>() { monthlyOrder.orderId }),
                    buyerName = monthlyOrder.buyerName,
                    colorRequest = monthlyOrder.colorRequest,
                    constructionComposite = monthlyOrder.constructionComposite,
                    deliveryDate = monthlyOrder.deliveryDate,
                    designCode = monthlyOrder.designCode,
                    inspectingQuantity = await GetInspectingQuantity(new List<int>() { monthlyOrder.orderId }),
                    onProductionQuantity = await GetOnProductionQuantity(new List<int>() { monthlyOrder.orderId }),
                    orderId = monthlyOrder.orderId,
                    orderNo = monthlyOrder.orderNo,
                    orderQuantity = monthlyOrder.orderQuantity,
                    processType = monthlyOrder.processType,
                    shipmentQuantity = await GetShipmentQuantity(new List<int>() { monthlyOrder.orderId }),
                    storageQuantity = await GetInventoryQuantity(new List<int>() { monthlyOrder.orderId }),
                    _createdDate = monthlyOrder._createdDate
                };

                monthlyOrderStatus.notInKanbanQuantity = monthlyOrder.orderQuantity - (await GetPreProductionQuantity(new List<int>() { monthlyOrder.orderId }) + monthlyOrderStatus.onProductionQuantity + monthlyOrderStatus.inspectingQuantity + monthlyOrderStatus.afterProductionQuantity);
                monthlyOrderStatus.diffOrderShipmentQuantity = monthlyOrder.orderQuantity - monthlyOrderStatus.shipmentQuantity;

                result.Add(monthlyOrderStatus);
            });

            Task.WaitAll(tasks.ToArray());

            //Parallel.ForEach(monthlyOrders, async monthlyOrder =>
            //{
            //    var monthlyOrderStatus = new MonthlyOrderStatusReportViewModel()
            //    {
            //        accountName = monthlyOrder.accountName,
            //        afterProductionQuantity = await GetAfterProductionQuantity(new List<int>() { monthlyOrder.orderId }),
            //        buyerName = monthlyOrder.buyerName,
            //        colorRequest = monthlyOrder.colorRequest,
            //        constructionComposite = monthlyOrder.constructionComposite,
            //        deliveryDate = monthlyOrder.deliveryDate,
            //        designCode = monthlyOrder.designCode,
            //        inspectingQuantity = await GetInspectingQuantity(new List<int>() { monthlyOrder.orderId }),
            //        onProductionQuantity = await GetOnProductionQuantity(new List<int>() { monthlyOrder.orderId }),
            //        orderId = monthlyOrder.orderId,
            //        orderNo = monthlyOrder.orderNo,
            //        orderQuantity = monthlyOrder.orderQuantity,
            //        processType = monthlyOrder.processType,
            //        shipmentQuantity = await GetShipmentQuantity(new List<int>() { monthlyOrder.orderId }),
            //        storageQuantity = await GetInventoryQuantity(new List<int>() { monthlyOrder.orderId }),
            //        _createdDate = monthlyOrder._createdDate
            //    };

            //    monthlyOrderStatus.notInKanbanQuantity = monthlyOrder.orderQuantity - (await GetPreProductionQuantity(new List<int>() { monthlyOrder.orderId }) + monthlyOrderStatus.onProductionQuantity + monthlyOrderStatus.inspectingQuantity + monthlyOrderStatus.afterProductionQuantity);
            //    monthlyOrderStatus.diffOrderShipmentQuantity = monthlyOrder.orderQuantity - monthlyOrderStatus.shipmentQuantity;

            //    result.Add(monthlyOrderStatus);
            //});

            //foreach (var monthlyOrder in monthlyOrders)
            //{
            //    var monthlyOrderStatus = new MonthlyOrderStatusReportViewModel()
            //    {
            //        accountName = monthlyOrder.accountName,
            //        afterProductionQuantity = await GetAfterProductionQuantity(new List<int>() { monthlyOrder.orderId }),
            //        buyerName = monthlyOrder.buyerName,
            //        colorRequest = monthlyOrder.colorRequest,
            //        constructionComposite = monthlyOrder.constructionComposite,
            //        deliveryDate = monthlyOrder.deliveryDate,
            //        designCode = monthlyOrder.designCode,
            //        inspectingQuantity = await GetInspectingQuantity(new List<int>() { monthlyOrder.orderId }),
            //        onProductionQuantity = await GetOnProductionQuantity(new List<int>() { monthlyOrder.orderId }),
            //        orderId = monthlyOrder.orderId,
            //        orderNo = monthlyOrder.orderNo,
            //        orderQuantity = monthlyOrder.orderQuantity,
            //        processType = monthlyOrder.processType,
            //        shipmentQuantity = await GetShipmentQuantity(new List<int>() { monthlyOrder.orderId }),
            //        storageQuantity = await GetInventoryQuantity(new List<int>() { monthlyOrder.orderId }),
            //        _createdDate = monthlyOrder._createdDate
            //    };

            //    monthlyOrderStatus.notInKanbanQuantity = monthlyOrder.orderQuantity - (await GetPreProductionQuantity(new List<int>() { monthlyOrder.orderId }) + monthlyOrderStatus.onProductionQuantity + monthlyOrderStatus.inspectingQuantity + monthlyOrderStatus.afterProductionQuantity);
            //    monthlyOrderStatus.diffOrderShipmentQuantity = monthlyOrder.orderQuantity - monthlyOrderStatus.shipmentQuantity;

            //    result.Add(monthlyOrderStatus);
            //}

            return result;
        }

        private async Task<List<MonthlyOrderQuantity>> GetMonthlyOrderQuantity(int year, int month, int orderTypeId)
        {
            var http = _serviceProvider.GetService<IHttpClientService>();
            var getMonthlyQueryUrl = $"{APIEndpoint.Sales}/sales/production-orders/monthly-by-order-type?year={year}&month={month}&orderTypeId={orderTypeId}";
            var requestResponse = await http.GetAsync(getMonthlyQueryUrl);

            var requestResult = new HttpDefaultResponse<List<MonthlyOrderQuantity>>();
            if (requestResponse.IsSuccessStatusCode)
            {
                requestResult = JsonConvert.DeserializeObject<HttpDefaultResponse<List<MonthlyOrderQuantity>>>(await requestResponse.Content.ReadAsStringAsync());
            }

            return requestResult.data;
        }

        public async Task<List<ProductionOrderStatusReportViewModel>> GetProductionOrderStatusReport(int productionOrderId)
        {
            //var kanbans = _kanbanDbSet.Include(kanban => kanban.Instruction).ThenInclude(instruction => instruction.Steps).Where(kanban => kanban.ProductionOrderId.Equals(productionOrderId)).Select(s => new ProductionOrderStatusReportViewModel()
            //{
            //    cartNumber = s.CartCartNumber,
            //    quantity = s.CurrentQty,
            //    status = s.IsInactive ? "Inactive" : s.IsComplete ? "Complete" : s.CurrentStepIndex > 0 && s.CurrentStepIndex <= s.Instruction.Steps.Count ? "Incomplete" : "Not yet started",
            //    processArea = !s.IsComplete ? s.CurrentStepIndex != s.Instruction.Steps.Count && s.CurrentStepIndex != 0 ? s.Instruction.Steps.ToList()[s.CurrentStepIndex - 1].ProcessArea : s.CurrentStepIndex == 0 ? "PPIC" : s.Instruction.Steps.LastOrDefault().ProcessArea : ""
            //});

            var kanbans = await _kanbanDbSet.Include(kanban => kanban.Instruction).ThenInclude(instruction => instruction.Steps).Where(kanban => kanban.ProductionOrderId.Equals(productionOrderId)).ToListAsync();

            var result = new List<ProductionOrderStatusReportViewModel>();
            foreach (var kanban in kanbans)
            {
                result.Add(new ProductionOrderStatusReportViewModel()
                {
                    cartNumber = kanban.CartCartNumber,
                    processArea = !kanban.IsComplete ? kanban.CurrentStepIndex != kanban.Instruction.Steps.Count && kanban.CurrentStepIndex != 0 ? kanban.Instruction.Steps.ToList()[kanban.CurrentStepIndex - 1].ProcessArea : kanban.CurrentStepIndex == 0 ? "PPIC" : kanban.Instruction.Steps.LastOrDefault().ProcessArea : "",
                    quantity = kanban.CurrentQty,
                    status = kanban.IsInactive ? "Inactive" : kanban.IsComplete ? "Complete" : kanban.CurrentStepIndex > 0 && kanban.CurrentStepIndex <= kanban.Instruction.Steps.Count ? "Incomplete" : "Not yet started"
                });
            }

            return result;
        }

        public async Task<List<YearlyOrderStatusReportViewModel>> GetYearlyOrderStatusReport(int year, int orderTypeId)
        {
            var yearlyOrderQuantity = await GetYearlyOrderQuantity(year, orderTypeId);

            var result = new List<YearlyOrderStatusReportViewModel>();
            foreach (var order in yearlyOrderQuantity)
            {
                var yearlyOrderStatus = new YearlyOrderStatusReportViewModel()
                {
                    afterProductionQuantity = await GetAfterProductionQuantity(order.OrderIds),
                    inspectingQuantity = await GetInspectingQuantity(order.OrderIds),
                    onProductionQuantity = await GetOnProductionQuantity(order.OrderIds),
                    orderQuantity = order.OrderQuantity,
                    preProductionQuantity = await GetPreProductionQuantity(order.OrderIds),
                    shipmentQuantity = await GetShipmentQuantity(order.OrderIds),
                    storageQuantity = await GetInventoryQuantity(order.OrderIds),
                    diffOrderKanbanQuantity = 0,
                    diffOrderShipmentQuantity = 0
                };

                yearlyOrderStatus.diffOrderKanbanQuantity = order.OrderQuantity - (yearlyOrderStatus.preProductionQuantity + yearlyOrderStatus.onProductionQuantity + yearlyOrderStatus.inspectingQuantity + yearlyOrderStatus.afterProductionQuantity);
                yearlyOrderStatus.diffOrderShipmentQuantity = order.OrderQuantity - yearlyOrderStatus.shipmentQuantity;

                result.Add(yearlyOrderStatus);
            }

            result.Add(new YearlyOrderStatusReportViewModel()
            {
                afterProductionQuantity = result.Sum(s => s.afterProductionQuantity),
                diffOrderKanbanQuantity = result.Sum(s => s.diffOrderKanbanQuantity),
                diffOrderShipmentQuantity = result.Sum(s => s.diffOrderShipmentQuantity),
                inspectingQuantity = result.Sum(s => s.inspectingQuantity),
                onProductionQuantity = result.Sum(s => s.onProductionQuantity),
                orderQuantity = result.Sum(s => s.orderQuantity),
                preProductionQuantity = result.Sum(s => s.preProductionQuantity),
                shipmentQuantity = result.Sum(s => s.shipmentQuantity),
                storageQuantity = result.Sum(s => s.storageQuantity),
                name = "Total",
            });

            return result;
        }

        private Task<double> GetInventoryQuantity(List<int> orderIds)
        {
            var packingIds = _packingDbSet.Where(w => orderIds.Contains(w.ProductionOrderId)).Select(s => s.Id).ToList();
            var packingReceiptIds = _packingReceiptDbSet.Where(packingReceipt => packingIds.Contains(packingReceipt.PackingId) && !packingReceipt.IsVoid).Select(s => s.Id).ToList();

            return _packingReceiptItemDbSet.Where(packingReceiptItem => packingReceiptIds.Contains(packingReceiptItem.PackingReceiptId)).SumAsync(s => s.AvailableQuantity * s.Length);
        }

        private Task<double> GetShipmentQuantity(List<int> orderIds)
        {
            var shipmentHeaderIds = _shipmentDetailDbSet.Where(shipmentItem => orderIds.Contains(shipmentItem.ProductionOrderId)).Select(s => s.ShipmentDocumentId).ToList();
            shipmentHeaderIds = _shipmentDbSet.Where(shipment => !shipment.IsVoid && shipmentHeaderIds.Contains(shipment.Id)).Select(s => s.Id).ToList();

            var shipmentDetailIds = _shipmentDetailDbSet.Where(shipmentDetail => shipmentHeaderIds.Contains(shipmentDetail.ShipmentDocumentId)).Select(s => s.Id).ToList();

            var shipmentItemIds = _shipmentItemDbSet.Where(shipmentItem => shipmentDetailIds.Contains(shipmentItem.ShipmentDocumentDetailId)).Select(s => s.Id).ToList();

            return _shipmentPackingReceiptItem.Where(packingReceiptItem => shipmentItemIds.Contains(packingReceiptItem.ShipmentDocumentItemId)).SumAsync(s => s.Quantity * s.Length);
        }

        private Task<double> GetPreProductionQuantity(List<int> orderIds)
        {
            return _kanbanDbSet.Where(kanban => !kanban.IsComplete && !kanban.IsInactive && kanban.CurrentStepIndex == 0 && orderIds.Contains(kanban.ProductionOrderId)).SumAsync(s => s.CurrentQty);
        }

        private Task<double> GetOnProductionQuantity(List<int> orderIds)
        {
            return _kanbanDbSet.Include(kanban => kanban.Instruction).ThenInclude(instruction => instruction.Steps).Where(kanban => !kanban.IsComplete && !kanban.IsInactive && kanban.CurrentStepIndex != 0 && kanban.CurrentStepIndex != kanban.Instruction.Steps.Count && orderIds.Contains(kanban.ProductionOrderId)).SumAsync(s => s.CurrentQty);
        }

        private Task<double> GetInspectingQuantity(List<int> orderIds)
        {
            return _kanbanDbSet.Include(kanban => kanban.Instruction).ThenInclude(instruction => instruction.Steps).Where(kanban => !kanban.IsComplete && !kanban.IsInactive && kanban.CurrentStepIndex != 0 && kanban.CurrentStepIndex == kanban.Instruction.Steps.Count && orderIds.Contains(kanban.ProductionOrderId)).SumAsync(s => s.CurrentQty);
        }

        private Task<double> GetAfterProductionQuantity(List<int> orderIds)
        {
            return _kanbanDbSet.Include(kanban => kanban.Instruction).ThenInclude(instruction => instruction.Steps).Where(kanban => kanban.IsComplete && !kanban.IsInactive && kanban.CurrentStepIndex != 0 && kanban.CurrentStepIndex == kanban.Instruction.Steps.Count && orderIds.Contains(kanban.ProductionOrderId)).SumAsync(s => s.CurrentQty);
        }

        private async Task<List<YearlyOrderQuantity>> GetYearlyOrderQuantity(int year, int orderTypeId)
        {
            var http = _serviceProvider.GetService<IHttpClientService>();
            var getYearlyQueryUrl = $"{APIEndpoint.Sales}/sales/production-orders/by-year-and-order-type?year={year}&orderTypeId={orderTypeId}";
            var requestResponse = await http.GetAsync(getYearlyQueryUrl);

            var requestResult = new HttpDefaultResponse<List<YearlyOrderQuantity>>();
            if (requestResponse.IsSuccessStatusCode)
            {
                requestResult = JsonConvert.DeserializeObject<HttpDefaultResponse<List<YearlyOrderQuantity>>>(await requestResponse.Content.ReadAsStringAsync());
            }

            return requestResult.data;
        }
    }

    public class YearlyOrderQuantity
    {
        public int Month { get; set; }
        public List<int> OrderIds { get; set; }
        public double OrderQuantity { get; set; }
    }
}
