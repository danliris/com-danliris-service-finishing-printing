using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ReturToQC;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.ReturToQC
{
    public class ReturToQCViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            ReturToQCViewModel viewModel = new ReturToQCViewModel()
            {
                UId = "UId"
            };

            Assert.Equal("UId", viewModel.UId);

        }

        [Fact]
        public void validate_default()
        {

            ReturToQCViewModel viewModel = new ReturToQCViewModel()
            {
                Items = new List<ReturToQCItemViewModel>()
                {
                    new ReturToQCItemViewModel()
                    {
                        Details =new List<ReturToQCItemDetailViewModel>()
                        {
                            new ReturToQCItemDetailViewModel()
                            {
                                ReturQuantity =0
                            }
                        }
                    }
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_Material_Empty()
        {

            ReturToQCViewModel viewModel = new ReturToQCViewModel()
            {
                Material =new MaterialIntegrationViewModel()
                {
                    Id =0
                },
                Items = new List<ReturToQCItemViewModel>()
                {
                    new ReturToQCItemViewModel()
                    {
                        Details =new List<ReturToQCItemDetailViewModel>()
                        {
                            new ReturToQCItemDetailViewModel()
                            {
                                ReturQuantity =0
                            }
                        }
                    }
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_MaterialConstruction_Empty()
        {

            ReturToQCViewModel viewModel = new ReturToQCViewModel()
            {
                MaterialConstruction =new MaterialConstructionIntegrationViewModel()
                {
                    Id =0
                },
                Items = new List<ReturToQCItemViewModel>()
                {
                    new ReturToQCItemViewModel()
                    {
                        Details =new List<ReturToQCItemDetailViewModel>()
                        {
                            new ReturToQCItemDetailViewModel()
                            {
                                ReturQuantity =3,
                                QuantityBefore =1
                            }
                        }
                    }
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }

        [Fact]
        public void validate_when_ProductionOrderNo_Empty()
        {

            ReturToQCViewModel viewModel = new ReturToQCViewModel()
            {
               
                Items = new List<ReturToQCItemViewModel>()
                {
                    new ReturToQCItemViewModel()
                    {
                        ProductionOrder =new ProductionOrderIntegrationViewModel()
                        {
                            Id =0
                        },
                        
                        Details =new List<ReturToQCItemDetailViewModel>()
                        {
                            //new ReturToQCItemDetailViewModel()
                            //{
                            //    ReturQuantity =0,
                            //    QuantityBefore =1
                            //}
                        }
                    }
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }


        [Fact]
        public void validate_when_QuantityBefore_moreThan_1()
        {

            ReturToQCViewModel viewModel = new ReturToQCViewModel()
            {

                Items = new List<ReturToQCItemViewModel>()
                {
                    new ReturToQCItemViewModel()
                    {
                        ProductionOrder =new ProductionOrderIntegrationViewModel()
                        {
                            Id =0
                        },

                        Details =new List<ReturToQCItemDetailViewModel>()
                        {
                            new ReturToQCItemDetailViewModel()
                            {
                              
                                QuantityBefore =1
                            }
                        }
                    }
                }
            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }
}
