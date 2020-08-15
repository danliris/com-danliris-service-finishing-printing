using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Packing;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Packing
{
    public class PackingQCGudangViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            PackingQCGudangViewModel vm = new PackingQCGudangViewModel()
            {
                Date =DateTime.Now,
                Dyeing =1,
                Jumlah =1,
                Printing =1,
                UlanganPrinting=1,
                UlanganSolid=1,
                White=1
            };

            Assert.True( DateTime.MinValue < vm.Date);
            Assert.Equal(1, vm.Dyeing);
            Assert.Equal(1, vm.Jumlah);
            Assert.Equal(1, vm.Printing);
            Assert.Equal(1, vm.UlanganPrinting);
            Assert.Equal(1, vm.UlanganSolid);
            Assert.Equal(1, vm.White);
        }
        }
}
