using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class AccountIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string UserName = "UserName test";
            string FirstName = "FirstName test";
            string LastName = "LastName test";
            string Gender = "Gender test";

            AccountIntegrationViewModel aivm = new AccountIntegrationViewModel();
            aivm.Id = 1;
            aivm.UserName = UserName;
            aivm.FirstName = FirstName;
            aivm.LastName = LastName;
            aivm.Gender = Gender;

            Assert.Equal(Id, aivm.Id);
            Assert.Equal(UserName, aivm.UserName);
            Assert.Equal(FirstName, aivm.FirstName);
            Assert.Equal(LastName, aivm.LastName);
            Assert.Equal(Gender, aivm.Gender);
        }
    }
}
