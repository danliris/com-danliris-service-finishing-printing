using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Daily_Operation
{

    public class DailyOperationViewModel : BaseViewModel, IValidatableObject
    {
        public string UId { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Shift { get; set; }
        public DateTimeOffset? DateInput { get; set; }
        public double? TimeInput { get; set; }
        public double? Input { get; set; }
        public DateTimeOffset? DateOutput { get; set; }
        public double? TimeOutput { get; set; }
        public double? GoodOutput { get; set; }
        public double? BadOutput { get; set; }

        public bool? IsEdit { get; set; }

        public bool IsChangeable { get; set; }

        //step
        public MachineStepViewModel Step { get; set; }

        //kanban
        public KanbanViewModel Kanban { get; set; }

        //machine
        public MachineViewModel Machine { get; set; }

        public ICollection<DailyOperationBadOutputReasonsViewModel> BadOutputReasons { get; set; }

        public string BadOutputDescription { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {


            if (string.IsNullOrEmpty(this.Type))
                yield return new ValidationResult("harus diisi", new List<string> { "Type" });

            if (string.IsNullOrEmpty(this.Shift))
                yield return new ValidationResult("harus diisi", new List<string> { "Shift" });

            if (this.Machine == null)
            {
                yield return new ValidationResult(" tidak boleh kosong", new List<string> { "Machine" });
            }

            if (this.Step == null)
            {
                yield return new ValidationResult(" tidak boleh kosong", new List<string> { "Step" });
            }
            if (this.Kanban != null)
            {
                if (!IsEdit.GetValueOrDefault())
                {
                    DailyOperationLogic service = (DailyOperationLogic)validationContext.GetService(typeof(DailyOperationLogic));

                    HashSet<int> hasInput = service.hasInput(this);

                    if (hasInput.Count > 0)
                    {
                        if (!(Kanban.CurrentStepIndex.HasValue && Kanban.Instruction.Steps.Where(x => Step.Process.Equals(x.Process)).Select(x => x.StepIndex).Contains(Kanban.CurrentStepIndex.GetValueOrDefault())))
                        {
                            yield return new ValidationResult("Data input tidak dapat disimpan karena ada data input yang belum dibuat output di mesin ini", new List<string> { "Machine" });
                            yield return new ValidationResult("Data input tidak dapat disimpan, Kereta harus melewati step " + this.Step.Process, new List<string> { "Kanban" });
                        }

                    }
                    else
                    {
                        if (Kanban.CurrentStepIndex.HasValue && !(Kanban.CurrentStepIndex.Value + 1 > Kanban.Instruction.Steps.Count))
                        {
                            var activeStep = Kanban.Instruction.Steps[Kanban.CurrentStepIndex.Value];
                            if (!activeStep.Process.Equals(Step.Process))
                            {
                                yield return new ValidationResult("step proses tidak sesuai", new List<string> { "Kanban" });
                            }
                        }
                        //var stepProcess = this.Kanban.Instruction.Steps.Find(x => x.Process.Equals(this.Step.Process));
                        //var kanbanCurrentStepIndex = this.Kanban.CurrentStepIndex != null ? this.Kanban.CurrentStepIndex : 0;
                        //if (!(stepProcess.SelectedIndex > kanbanCurrentStepIndex))
                        //{
                        //    yield return new ValidationResult("step proses tidak sesuai", new List<string> { "Kanban" });
                        //}
                    }
                }
            }
            else if (this.Kanban == null)
            {
                yield return new ValidationResult(" tidak boleh kosong", new List<string> { "Kanban" });
            }



            if (this.Type == "input")
            {
                if (this.TimeInput == 0)
                {
                    yield return new ValidationResult("harus diisi", new List<string> { "TimeInput" });
                }

                if (this.Input <= 0)
                {
                    yield return new ValidationResult("harus diisi", new List<string> { "Input" });
                }

                if (this.DateInput == null)
                {
                    yield return new ValidationResult("harus diisi", new List<string> { "DateInput" });
                }

                if (this.DateInput > DateTime.Now)
                {
                    yield return new ValidationResult("date input lebih dari hari ini", new List<string> { "DateInput" });
                }

            }
            else if (this.Type == "output")
            {
                if (this.TimeOutput == 0)
                {
                    yield return new ValidationResult("harus diisi", new List<string> { "TimeOutput" });
                }

                if (this.GoodOutput <= 0)
                {
                    yield return new ValidationResult("harus diisi, tidak boleh kurang dari 0", new List<string> { "GoodOutput" });
                }

                if (this.DateOutput == null)
                {
                    yield return new ValidationResult("harus diisi", new List<string> { "DateOutput" });
                }

                if (this.DateOutput > DateTime.Now)
                {
                    yield return new ValidationResult("date output lebih dari hari ini", new List<string> { "DateOutput" });
                }

                if ((this.BadOutputReasons.Count.Equals(0) && this.BadOutput > 0) || (this.BadOutput > 0 && this.BadOutputReasons == null))
                {
                    yield return new ValidationResult("BadOutputReasons harus di isi", new List<string> { "BadOutputReasons" });
                }
                else if (this.BadOutputReasons.Count > 0 && this.BadOutput > 0)
                {
                    int Count = 0;
                    string BadOutputReasons = "[";
                    foreach (DailyOperationBadOutputReasonsViewModel item in this.BadOutputReasons)
                    {
                        BadOutputReasons += "{";
                        if (item.BadOutput == null)
                        {
                            Count++;
                            BadOutputReasons += "BadOutput:'alasan harus di isi', ";
                        }

                        if (item.Length <= 0)
                        {
                            Count++;
                            BadOutputReasons += " Length:'panjang harus di isi' , ";
                        }

                        if (string.IsNullOrEmpty(item.Action))
                        {
                            Count++;
                            BadOutputReasons += " Action:'action harus di isi' , ";
                        }

                        if (item.Machine == null)
                        {
                            Count++;
                            BadOutputReasons += " Machine: 'mesin harus di isi' , ";
                        }
                        BadOutputReasons += "}";
                    }

                    BadOutputReasons += "]";

                    if (Count > 0)
                    {
                        yield return new ValidationResult(BadOutputReasons, new List<string> { "BadOutputReasons" });
                    }
                }
            }
        }

    }
}
