using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoSweetSpotApiClientLib;
using GoSweetSpotApiClientLib.Models;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class ApiClientTests
    {
        string api_token = "572014901603BD9CC53918988264B788D83CDA9BFA9E48F508";

        [TestMethod]
        public void CustomerOrders_Get_ShouldReturnNothing()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var orders = client.CustomerOrders_GetAsync("this order number should not exist").Result;
            Assert.IsTrue(orders.Count == 0);
        }

        [TestMethod]
        public void CustomerOrders_Send_Basic_Order()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var orders = client.CustomerOrders_Send(new GoSweetSpotApiClientLib.Models.CustomerOrder
            {
                OrderNumber = "test1-" + DateTime.Now.ToString("yy-MM-dd"),
                Address1 = "1 Queens Street",
                Address2 = "",
                Suburb = "Auckland Central",
                City = "Auckland",
                PostCode = "",
                Consignee = "Test 1"
            }).Result;

            Assert.IsTrue(orders[0].Result);
        }

        [TestMethod]
        public void CustomerOrders_Send_Order_With_Products()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var orders = client.CustomerOrders_Send(new GoSweetSpotApiClientLib.Models.CustomerOrder
            {
                OrderNumber = "test1-" + DateTime.Now.ToString("yy-MM-dd"),
                Address1 = "1 Queens Street",
                Address2 = "",
                Suburb = "Auckland Central",
                City = "Auckland",
                PostCode = "",
                Consignee = "Test 1",
                Products = new System.Collections.Generic.List<GoSweetSpotApiClientLib.Models.CustomerOrder.Product>(
                    new GoSweetSpotApiClientLib.Models.CustomerOrder.Product[]
                    {
                        new GoSweetSpotApiClientLib.Models.CustomerOrder.Product
                        {
                             Code = "ABC",
                             Description = "Wall Paint",
                             Currency = "NZD",
                             UnitKg= 1,
                             Units = 1,
                             UnitValue = 10
                        }
                    })
            }).Result;

            Assert.IsTrue(orders[0].Result);
        }

        [TestMethod]
        public void CustomerOrders_Get_Recent_Created()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var orders = client.CustomerOrders_GetAsync("test1-" + DateTime.Now.ToString("yy-MM-dd")).Result;

            Assert.IsTrue(orders.Count > 0);
        }

        [TestMethod]
        public void CustomerOrders_Get_With_Products()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var orders = client.CustomerOrders_GetAsync("test1-" + DateTime.Now.ToString("yy-MM-dd"), true).Result;


            Assert.IsTrue(orders.Count > 0);
            Assert.IsTrue(orders.First().Products.Count > 0);
        }
        [TestMethod]
        public void Printers_GetActive()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var data = client.Printers_GetAsync().Result;

            Assert.IsTrue(data.Count > 0);
        }

        [TestMethod]
        public void RatesQuery_Domestics_Get_Any()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var data = client.RatesQuery_GetAsync(new GoSweetSpotApiClientLib.Models.RatesQueryOrShipmentRequest
            {
                Destination = new RatesQueryOrShipmentRequest.Contact
                {
                    Name = "DestinationName",
                    Address = new RatesQueryOrShipmentRequest.Contact.AddressModel
                    {
                        BuildingName = "",
                        StreetAddress = "DestinationStreetAddress",
                        Suburb = "Whangarei",
                        City = "Northland",
                        PostCode = "0142",
                        CountryCode = "NZ",
                    },
                    ContactPerson = "DestinationContact",
                    PhoneNumber = "123456789",
                    Email = "destinationemail@email.com",
                    DeliveryInstructions = "Desinationdeliveryinstructions"
                },
                IsSaturdayDelivery = false,
                IsSignatureRequired = true,
                Packages = new System.Collections.Generic.List<RatesQueryOrShipmentRequest.RatesPackage>(
                    new RatesQueryOrShipmentRequest.RatesPackage[] {
                        new  RatesQueryOrShipmentRequest.RatesPackage{
                            Height = 1,
                            Length = 1,
                            Id = 0,
                            Width = 10,
                            Kg = 0.1M,
                            Name = "GSS-DLE SATCHEL",
                            //Type = "Box"
                        }
                    })
            }).Result;

            Assert.IsTrue(data.Available.Count > 0);
        }

        [TestMethod]
        public void RatesQuery_Internation_Get_Any()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var data = client.RatesQuery_GetAsync(new GoSweetSpotApiClientLib.Models.RatesQueryOrShipmentRequest
            {
                Destination = new RatesQueryOrShipmentRequest.Contact
                {
                    Name = "DestinationName",
                    Address = new RatesQueryOrShipmentRequest.Contact.AddressModel
                    {
                        BuildingName = "",
                        StreetAddress = "DestinationStreetAddress",
                        Suburb = "Mascot",
                        City = "NSW",
                        PostCode = "2020",
                        CountryCode = "AU",
                    },
                    ContactPerson = "DestinationContact",
                    PhoneNumber = "123456789",
                    Email = "destinationemail@email.com",
                    DeliveryInstructions = "Desinationdeliveryinstructions"
                },
                IsSaturdayDelivery = false,
                IsSignatureRequired = true,
                IncludeInsurance = true,
                Packages = new System.Collections.Generic.List<RatesQueryOrShipmentRequest.RatesPackage>(
                    new RatesQueryOrShipmentRequest.RatesPackage[] {
                        new  RatesQueryOrShipmentRequest.RatesPackage{
                            Height = 1,
                            Length = 1,
                            Id = 0,
                            Width = 10,
                            Kg = 0.1M,
                            Name = "GSS-DLE SATCHEL",
                            //Type = "Box"
                        }
                    }),
                Commodities = new System.Collections.Generic.List<RatesQueryOrShipmentRequest.Commodity>(
                        new RatesQueryOrShipmentRequest.Commodity[] {
                            new RatesQueryOrShipmentRequest.Commodity{
                                Country = "NZ",
                                Currency = "NZD",
                                UnitKg = 1.5M,
                                Units = 10,
                                UnitValue = 50.50M
                            }
                        })
            }).Result;

            Assert.IsTrue(data.Available.Count > 0);
        }

        [TestMethod]
        public void CreateDomesticOutboundShipment()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var data = client.Shipment_CreateAsync(new RatesQueryOrShipmentRequest
            {
                Carrier = "Post Haste",
                //Service = "Overnight",
                //QuoteId = "3132131",
                DeliveryReference = "ORDER123",
                Destination = new RatesQueryOrShipmentRequest.Contact
                {
                    Name = "DestinationName",
                    Address = new RatesQueryOrShipmentRequest.Contact.AddressModel
                    {
                        BuildingName = "",
                        StreetAddress = "DestinationStreetAddress",
                        Suburb = "Avonside",
                        City = "Christchurch",
                        PostCode = "8061",
                        CountryCode = "NZ",
                    },
                    ContactPerson = "DestinationContact",
                    PhoneNumber = "123456789",
                    Email = "destinationemail@email.com",
                    DeliveryInstructions = "Desinationdeliveryinstructions"
                },
                IsSaturdayDelivery = false,
                IsSignatureRequired = true,
                Packages = new List<RatesQueryOrShipmentRequest.RatesPackage>(new RatesQueryOrShipmentRequest.RatesPackage[] { new RatesQueryOrShipmentRequest.RatesPackage{
                    Height = 1,
                    Length = 1,
                    Id = 0,
                    Width = 10,
                    Kg = 0.1M,
                    Name = "GSS-DLE SATCHEL",
                }
                }),
                PrintToPrinter = "false"
            }).Result;

            Assert.IsTrue(data.Consignments.Count > 0);
        }
        [TestMethod]
        public void CreateDomesticOutboundShipmentWithDangerousGoods()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var data = client.Shipment_CreateAsync(new RatesQueryOrShipmentRequest
            {
                Carrier = "Post Haste",
                //Service = "Overnight",
                //QuoteId = "3132131",
                DeliveryReference = "ORDER123",
                Destination = new RatesQueryOrShipmentRequest.Contact
                {
                    Name = "DestinationName",
                    Address = new RatesQueryOrShipmentRequest.Contact.AddressModel
                    {
                        BuildingName = "",
                        StreetAddress = "DestinationStreetAddress",
                        Suburb = "Avonside",
                        City = "Christchurch",
                        PostCode = "8061",
                        CountryCode = "NZ",
                    },
                    ContactPerson = "DestinationContact",
                    PhoneNumber = "123456789",
                    Email = "destinationemail@email.com",
                    DeliveryInstructions = "Desinationdeliveryinstructions"
                },
                IsSaturdayDelivery = false,
                IsSignatureRequired = true,
                Packages = new List<RatesQueryOrShipmentRequest.RatesPackage>(new RatesQueryOrShipmentRequest.RatesPackage[] { new RatesQueryOrShipmentRequest.RatesPackage{
                    Height = 1,
                    Length = 1,
                    Id = 0,
                    Width = 10,
                    Kg = 0.1M,
                    Name = "GSS-DLE SATCHEL",
                }
                }),
                PrintToPrinter = "false",
                HasDG = true,
                DangerousGoods = new RatesQueryOrShipmentRequest.DangerousGood
                {
                    AdditionalHandlingInfo = "Some info",
                    HazchemCode = "HC",
                    IsRadioActive = false,
                    CargoAircraftOnly = false,
                    LineItems = new List<RatesQueryOrShipmentRequest.DangerousGood.DangerousGoodItem>(new[]{
                        new  RatesQueryOrShipmentRequest.DangerousGood.DangerousGoodItem{
                            Description = "desc",
                            ClassOrDivision = "class",
                            UNorIDNo = "",
                            PackingGroup = "",
                            SubsidaryRisk = "",
                            Packing = "",
                            PackingInstr = "",
                            Authorization = ""
                        }
                    })
                },
                Outputs = new List<RatesQueryOrShipmentRequest.OutputEnum>(new[] { RatesQueryOrShipmentRequest.OutputEnum.DG_FORM_PDF })
            }).Result;

            Assert.IsTrue(data.Consignments.Count > 0);
        }
        [TestMethod]
        public void CreateInternationalOutboundShipment()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var data = client.Shipment_CreateAsync(new RatesQueryOrShipmentRequest
            {
                Carrier = "FedEx",
                //Service = "Overnight",
                //QuoteId = "3132131",
                DeliveryReference = "ORDER123",
                Destination = new RatesQueryOrShipmentRequest.Contact
                {
                    Name = "DestinationName",
                    Address = new RatesQueryOrShipmentRequest.Contact.AddressModel
                    {
                        BuildingName = "",
                        StreetAddress = "DestinationStreetAddress",
                        Suburb = "Mascot",
                        City = "NSW",
                        PostCode = "2020",
                        CountryCode = "AU",
                    },
                    ContactPerson = "DestinationContact",
                    PhoneNumber = "123456789",
                    Email = "destinationemail@email.com",
                    DeliveryInstructions = "Desinationdeliveryinstructions"
                },
                IsSaturdayDelivery = false,
                IsSignatureRequired = true,
                Packages = new List<RatesQueryOrShipmentRequest.RatesPackage>(new RatesQueryOrShipmentRequest.RatesPackage[] { new RatesQueryOrShipmentRequest.RatesPackage{
                    Height = 50,
                    Length = 20,
                    Width = 30,
                    Kg = 10.0M,
                    Name = "Custom",
                }
                }),
                PrintToPrinter = "false",
                Commodities = new List<RatesQueryOrShipmentRequest.Commodity>(
                        new RatesQueryOrShipmentRequest.Commodity[] {
                            new RatesQueryOrShipmentRequest.Commodity{
                                Description ="Mens Socks",
                                Country = "NZ",
                                Currency = "NZD",
                                UnitKg = 1.0M,
                                Units = 10,
                                UnitValue = 50.50M
                            }
                        })
            }).Result;

            Assert.IsTrue(data.Consignments.Count > 0);
        }
        [TestMethod]
        public void CreateDomesticReturnsShipment()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var data = client.Shipment_CreateAsync(new RatesQueryOrShipmentRequest
            {
                Carrier = "Post Haste",
                //Service = "Overnight",
                //QuoteId = "3132131",
                DeliveryReference = "ORDER123",
                Origin = new RatesQueryOrShipmentRequest.Contact
                {
                    Name = "Bob Jones",
                    Address = new RatesQueryOrShipmentRequest.Contact.AddressModel
                    {
                        BuildingName = "",
                        StreetAddress = "Bob Jones Close",
                        Suburb = "Avonside",
                        City = "Christchurch",
                        PostCode = "8061",
                        CountryCode = "NZ",
                    },
                    ContactPerson = "DestinationContact",
                    PhoneNumber = "123456789",
                    Email = "destinationemail@email.com",
                    DeliveryInstructions = "Desinationdeliveryinstructions"
                },
                Destination = new RatesQueryOrShipmentRequest.Contact
                {
                    Name = "DestinationName",
                    Address = new RatesQueryOrShipmentRequest.Contact.AddressModel
                    {
                        BuildingName = "",
                        StreetAddress = "DestinationStreetAddress",
                        Suburb = "Avondale",
                        City = "Auckland",
                        PostCode = "0600",
                        CountryCode = "NZ",
                    },
                    ContactPerson = "DestinationContact",
                    PhoneNumber = "123456789",
                    Email = "destinationemail@email.com",
                    DeliveryInstructions = "Desinationdeliveryinstructions"
                },
                IsSaturdayDelivery = false,
                IsSignatureRequired = true,
                Packages = new List<RatesQueryOrShipmentRequest.RatesPackage>(new RatesQueryOrShipmentRequest.RatesPackage[] { new RatesQueryOrShipmentRequest.RatesPackage{
                    Height = 1,
                    Length = 1,
                    Id = 0,
                    Width = 10,
                    Kg = 0.1M,
                    Name = "GSS-DLE SATCHEL",
                }
                }),
                PrintToPrinter = "false"
            }).Result;

            Assert.IsTrue(data.Consignments.Count > 0);
        }
        [TestMethod]
        public void DeleteShipments()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var data = client.Shipment_DeleteAsync(new List<string>()
            {
                "SSPOT014115","SSPOT014114"
            }).Result;


        }
        [TestMethod]
        public void CreateDomesticOutboundShipmentWithPreRating()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var data = client.Shipment_CreateAsync(new RatesQueryOrShipmentRequest
            {
                DeliveryReference = "ORDER123",
                Destination = new RatesQueryOrShipmentRequest.Contact
                {
                    Name = "DestinationName",
                    Address = new RatesQueryOrShipmentRequest.Contact.AddressModel
                    {
                        BuildingName = "",
                        StreetAddress = "DestinationStreetAddress",
                        Suburb = "Avonside",
                        City = "Christchurch",
                        PostCode = "8061",
                        CountryCode = "NZ",
                    },
                    ContactPerson = "DestinationContact",
                    PhoneNumber = "123456789",
                    Email = "destinationemail@email.com",
                    DeliveryInstructions = "Desinationdeliveryinstructions"
                },
                IsSaturdayDelivery = false,
                IsSignatureRequired = true,
                Packages = new List<RatesQueryOrShipmentRequest.RatesPackage>(new RatesQueryOrShipmentRequest.RatesPackage[] { new RatesQueryOrShipmentRequest.RatesPackage{
                    Height = 1,
                    Length = 1,
                    Id = 0,
                    Width = 10,
                    Kg = 0.1M,
                    Name = "GSS-DLE SATCHEL",
                }
                })
            }).Result;

            Assert.IsTrue(data.Consignments.Count > 0);
        }

        [TestMethod]
        public void BookPickup()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);

            var req = new BookPickupRequest()
            {
                Carrier = "FedEx",
                Parts = 0,
                TotalKg = 0
            };

            req.Consignments = new List<string>();
            req.Consignments.Add("ABC123");
            req.Consignments.Add("ABC124");

            /* Warning - This will book a real courier to come for collection for the job */
            string result = "";
           //result = client.PickupBooking_PostAsync(req).Result;

            Assert.IsTrue(result.Contains("success"));
        }

    }
}
