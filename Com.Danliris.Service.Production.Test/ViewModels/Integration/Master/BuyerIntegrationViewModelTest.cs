using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class BuyerIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string Address = "Address test";
            string City = "City test";
            string Code = "Code test";
            string Contact = "Contact test";
            string Country = "Country test";
            string Name = "Name test";
            string NPWP = "NPWP test";
            string Tempo = "Tempo test";
            string Type = "Type test";

            BuyerIntegrationViewModel bivm = new BuyerIntegrationViewModel();
            bivm.Id = 1;
            bivm.Address = Address;
            bivm.City = City;
            bivm.Code = Code;
            bivm.Contact = Contact;
            bivm.Country = Country;
            bivm.Name = Name;
            bivm.NPWP = NPWP;
            bivm.Tempo = Tempo;
            bivm.Type = Type;

            Assert.Equal(Id, bivm.Id);
            Assert.Equal(Address, bivm.Address);
            Assert.Equal(City, bivm.City);
            Assert.Equal(Code, bivm.Code);
            Assert.Equal(Contact, bivm.Contact);
            Assert.Equal(Country, bivm.Country);
            Assert.Equal(Name, bivm.Name);
            Assert.Equal(NPWP, bivm.NPWP);
            Assert.Equal(Tempo, bivm.Tempo);
            Assert.Equal(Type, bivm.Type);
        }
    }
}
