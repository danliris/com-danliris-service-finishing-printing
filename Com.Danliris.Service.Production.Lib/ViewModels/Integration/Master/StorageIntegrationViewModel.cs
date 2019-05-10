using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master
{
    public class StorageIntegrationViewModel : BaseViewModel
    {
        public int _id { get; set; }

        public bool _deleted { get; set; }

        public bool _active { get; set; }

        public DateTime _createdDate { get; set; }

        public string _createdBy { get; set; }

        public string _createAgent { get; set; }

        public DateTime _updatedDate { get; set; }

        public string _updatedBy { get; set; }

        public string _updateAgent { get; set; }

        public string code { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public StorageUnitViewModel unit { get; set; }
    }

    public class StorageUnitViewModel
    {
        public int? _id { get; set; }

        public string name { get; set; }
        public string code { get; set; }
        public divisionViewModel division { get; set; }
    }

    public class divisionViewModel
    {
        public int? _id { get; set; }

        public string name { get; set; }

        public string code { get; set; }

        public DivisionViewModel division { get; set; }
    }
}

