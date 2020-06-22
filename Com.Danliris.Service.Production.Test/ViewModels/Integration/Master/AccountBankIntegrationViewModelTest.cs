using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class AccountBankIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            string code = "code test";
            string AccountName = "AccountName test";
            string AccountNumber = "AccountNumber test";
            string BankName = "BankName test";
            string BankAddress = "BankAddress test";
            string SwiftCode = "SwiftCode test";

            AccountBankIntegrationViewModel abivm = new AccountBankIntegrationViewModel();
            abivm.Code = code;
            abivm.AccountName = AccountName;
            abivm.AccountNumber = AccountNumber;
            abivm.BankName = BankName;
            abivm.BankAddress = BankAddress;
            abivm.SwiftCode = SwiftCode;

            Assert.Equal(code, abivm.Code);
            Assert.Equal(AccountName, abivm.AccountName);
            Assert.Equal(AccountNumber, abivm.AccountNumber);
            Assert.Equal(BankName, abivm.BankName);
            Assert.Equal(BankAddress, abivm.BankAddress);
            Assert.Equal(SwiftCode, abivm.SwiftCode);
        }
    }
}
