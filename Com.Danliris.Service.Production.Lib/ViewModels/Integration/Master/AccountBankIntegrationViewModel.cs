using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master
{
    public class AccountBankIntegrationViewModel : BaseViewModel
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BankAddress { get; set; }
        public string BankName { get; set; }
        public string Code { get; set; }
        public string SwiftCode { get; set; }
    }
}