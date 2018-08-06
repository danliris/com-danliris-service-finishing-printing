using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;

namespace Com.Danliris.Service.Production.Lib.ViewModels.Master.Step
{
    public class StepIndicatorViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Uom { get; set; }
        public double Value { get; set; }
    }
}