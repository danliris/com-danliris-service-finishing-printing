using System;
using static Com.Danliris.Service.Finishing.Printing.Lib.Enums.FabricQualityControlEnums.FabricQualityControlEnums;
using Com.Danliris.Service.Finishing.Printing.Lib.Utilities;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.FabricQualityControl
{
    public class CriteriaViewModel
    {
        public int? Id { get; set; }
        //private int _code;
        //private string _codeVM;
        //public int Code { get { return string.IsNullOrWhiteSpace(_codeVM) ? _code : (int)(Enum.Parse(typeof(GeneralCriteriaCode), _codeVM)); } set { Code = string.IsNullOrWhiteSpace(CodeVM) ? value : (int)(Enum.Parse(typeof(GeneralCriteriaCode), CodeVM)); } }
        ////public int Code { get { return string.IsNullOrWhiteSpace(_codeVM) ? _code : (int)(Enum.Parse(typeof(GeneralCriteriaCode), _codeVM)); } set { Code = string.IsNullOrWhiteSpace(CodeVM) ? value : (int)(Enum.Parse(typeof(GeneralCriteriaCode), CodeVM)); } }
        ////public string CodeVM { get { return Code > 0 ? ((GeneralCriteriaCode)Code).ToString() : CodeVM; } set { CodeVM = Code > 0 ? ((GeneralCriteriaCode)Code).ToString() : value; } }
        //public int Group { get { return string.IsNullOrWhiteSpace(GroupVM) ? Group : (int)(Enum.Parse(typeof(GeneralCriteriaGroup), GroupVM)); } set { Group = string.IsNullOrWhiteSpace(GroupVM) ? value : (int)(Enum.Parse(typeof(GeneralCriteriaGroup), GroupVM)); } }
        //public string GroupVM { get { return Group > 0 ? ((GeneralCriteriaGroup)Group).ToString() : GroupVM; } set { GroupVM = Group > 0 ? ((GeneralCriteriaGroup)Group).ToString() : value; } }
        //public int Name { get; set; }
        //public string NameVM { get { return Name > 0 ? ((GeneralCriteriaName)Name).ToString() : NameVM; } set { NameVM = Name > 0 ? EnumExtensions.GetDisplayName(((GeneralCriteriaName)Name)) : value; } }

        public string Code { get; set; }
        public string Group { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Score Score { get; set; }
    }
}