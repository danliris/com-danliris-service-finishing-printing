using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Production.Lib.Models.Master.Step;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.Step;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.Lib.Models.Master.Instruction;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.Instruction;

namespace Com.Danliris.Service.Production.Lib.BusinessLogic.Facades.Master
{
    public class InstructionFacade : IInstructionFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<InstructionModel> DbSet;
        private readonly InstructionLogic InstructionLogic;

        public InstructionFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<InstructionModel>();
            InstructionLogic = serviceProvider.GetService<InstructionLogic>();
        }

        public ReadResponse<InstructionModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<InstructionModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Code", "Name"
            };
            query = QueryHelper<InstructionModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<InstructionModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
                {
                    "Id", "Name", "Code", "Steps"
                };
            query = query
                .Select(field => new InstructionModel
                {
                    Id = field.Id,
                    Code = field.Code,
                    Name = field.Name,
                    Steps = new List<InstructionStepModel>(field.Steps.Select(i => new InstructionStepModel
                    {
                        Alias = i.Alias,
                        Process = i.Process,
                        ProcessArea = i.ProcessArea,
                        StepIndicators = new List<InstructionStepIndicatorModel>(i.StepIndicators.Select(s => new InstructionStepIndicatorModel
                        {
                            Name = s.Name,
                            Uom = s.Uom,
                            Value = s.Value
                        }))
                    }))
                });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<InstructionModel>.Order(query, orderDictionary);

            Pageable<InstructionModel> pageable = new Pageable<InstructionModel>(query, page - 1, size);
            List<InstructionModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<InstructionModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<int> CreateAsync(InstructionModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));
            InstructionLogic.CreateModel(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<InstructionModel> ReadByIdAsync(int id)
        {
            return await InstructionLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, InstructionModel model)
        {
            InstructionLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await InstructionLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        //public ReadResponse<InstructionModel> ReadVM(int page, int size, string order, List<string> select, string keyword, string filter)
        //{
        //    IQueryable<InstructionModel> query = DbSet;

        //    List<string> searchAttributes = new List<string>()
        //    {
        //        "Code", "Name"
        //    };
        //    query = QueryHelper<InstructionModel>.Search(query, searchAttributes, keyword);

        //    Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
        //    query = QueryHelper<InstructionModel>.Filter(query, filterDictionary);

        //    List<string> selectedFields = new List<string>()
        //    {
        //        "Name", "Code", "Steps"
        //    };
        //    query = query
        //        .Select(field => new InstructionModel
        //        {
        //            Code = field.Code,
        //            Name = field.Name,
        //            Steps = new List<InstructionStepModel>(field.Steps.Select(i => new InstructionStepModel
        //            {
        //                Alias = i.Alias,
        //                Process = i.Process,
        //                ProcessArea = i.ProcessArea,
        //                StepIndicators = new List<InstructionStepIndicatorModel>(i.StepIndicators.Select(s => new InstructionStepIndicatorModel
        //                {
        //                    Name = s.Name,
        //                    Uom = s.Uom,
        //                    Value = s.Value
        //                }))
        //            }))
        //        });

        //    Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
        //    query = QueryHelper<InstructionModel>.Order(query, orderDictionary);

        //    Pageable<InstructionModel> pageable = new Pageable<InstructionModel>(query, page - 1, size);
        //    List<InstructionModel> data = pageable.Data.ToList();
        //    int totalData = pageable.TotalCount;

        //    return new ReadResponse<InstructionModel>(data, totalData, orderDictionary, selectedFields);
        //}

        public ReadResponse<InstructionModel> ReadVM(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<InstructionModel> query = DbSet;

            //query.Where(w => w.)

            query = query.Where(w => w.Name.Contains(keyword) || w.Code.Contains(keyword)).OrderBy(o => o.LastModifiedUtc).Skip((page - 1) * size).Take(size);

            //List<string> searchAttributes = new List<string>()
            //{
            //    "Code", "Name"
            //};
            //query = QueryHelper<InstructionModel>.Search(query, searchAttributes, keyword);

            //Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            //query = QueryHelper<InstructionModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
            {
                "Name", "Code", "Steps"
            };

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            //query = QueryHelper<InstructionModel>.Order(query, orderDictionary);

            //Pageable<InstructionModel> pageable = new Pageable<InstructionModel>(query, page - 1, size);
            //List<InstructionModel> data = pageable.Data.ToList();
            //int totalData = pageable.TotalCount;

            var data = query.Select(s => new InstructionModel() {
                Code = s.Code,
                Name = s.Name,
                Steps = new List<InstructionStepModel>(s.Steps.Select(i => new InstructionStepModel
                {
                    Alias = i.Alias,
                    Process = i.Process,
                    ProcessArea = i.ProcessArea,
                    StepIndicators = new List<InstructionStepIndicatorModel>(i.StepIndicators.Select(si => new InstructionStepIndicatorModel
                    {
                        Name = si.Name,
                        Uom = si.Uom,
                        Value = si.Value
                    }))
                }))
            }).ToList();

            return new ReadResponse<InstructionModel>(data, data.Count, orderDictionary, selectedFields);
        }
    }
}
