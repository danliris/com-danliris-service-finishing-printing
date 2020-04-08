using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Services.Master
{
    public class StorageIntegrationServiceTest
    {
        [Fact]
        public void Should_Success_Instanciate_NewShipmentDocumentPackingReceiptItemProduct_ViewModel()
        {

            var viewModel = new StorageIntegrationViewModel()
            {
                Id = 1,
                _deleted = true,
                _active = true,
                _createdBy = "_createdBy",
                _createAgent = "_createAgent",
                _updatedBy = "_updatedBy",
                _updateAgent = "_updateAgent",
                code = "code",
                name = "name",
                description = "description",
                unit = new StorageUnitViewModel()
                {
                    _id = 1,
                    code = "code",
                    name = "name",
                    division = new divisionViewModel()
                    {
                        _id = 1,
                        code = "code",
                        name = "name",
                    },
                },
            };

            Assert.NotNull(viewModel.Id);
            Assert.NotNull(viewModel._deleted);
            Assert.NotNull(viewModel._active);
            Assert.NotNull(viewModel._createdBy);
            Assert.NotNull(viewModel._createAgent);
            Assert.NotNull(viewModel._updatedBy);
            Assert.NotNull(viewModel._updateAgent);
            Assert.NotNull(viewModel.code);
            Assert.NotNull(viewModel.name);
            Assert.NotNull(viewModel.description);
            Assert.NotNull(viewModel.unit._id);
            Assert.NotNull(viewModel.unit.code);
            Assert.NotNull(viewModel.unit.name);
            Assert.NotNull(viewModel.unit.division._id);
            Assert.NotNull(viewModel.unit.division.code);
            Assert.NotNull(viewModel.unit.division.name);
        }
    }
}
