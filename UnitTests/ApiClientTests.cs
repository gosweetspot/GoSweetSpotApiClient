using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoSweetSpotApiClientLib;
using GoSweetSpotApiClientLib.Models;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class ApiClientTests
    {
        string api_token = "XXXXXXXXXXXXXXXXXXXXXXXXX";
        int? siteId = null;

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
                OrderNumber = "test1-" + Guid.NewGuid().ToString(),
                Address1 = "1 Queens Street",
                Address2 = "",
                Suburb = "Auckland Central",
                City = "Auckland",
                PostCode = "",
                Consignee = "Test 1",
                RawAddress = "1 Queens Street\nAuckland Central",
                IconUrl = "https://dummyimage.com/600x400/000/fff"
            }).Result;

            Assert.IsTrue(orders[0].Result);
        }

        [TestMethod]
        public void CustomerOrders_Send_Order_With_Products()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var orders = client.CustomerOrders_Send(new GoSweetSpotApiClientLib.Models.CustomerOrder
            {
                OrderNumber = "test2-" + DateTime.Now.ToString("yy-MM-dd") + "-products",
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
                             Units = 10,
                             UnitValue = 10
                        }
                    })
            }).Result;

            Assert.IsTrue(orders[0].Result);
        }
        [TestMethod]
        public void CustomerOrders_Send_Order_With_Packages()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var orders = client.CustomerOrders_Send(new GoSweetSpotApiClientLib.Models.CustomerOrder
            {
                OrderNumber = "-" + Guid.NewGuid(),
                Address1 = "1 Queens Street",
                Address2 = "",
                Suburb = "Surry Hills",
                City = "Sydney",
                PostCode = "",
                Country = "AU",
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
                             Units = 10,
                             UnitValue = 10
                        }
                    }),
                Packages = new List<CustomerOrder.PackageSelection>()
                {
                    new CustomerOrder.PackageSelection()
                    {
                        PackageStockName = "SMALL BOX",
                        HeightCm = 10,
                        LengthCm = 10,
                        WidthCm = 10,
                        WeightKg = 5,
                        Quantity = 1
                    },
                    new CustomerOrder.PackageSelection()
                    {
                        PackageStockName = "Box 1",
                        HeightCm = 1.1m,
                        LengthCm = 1.1m,
                        WidthCm = 1.1m,
                        WeightKg = 1.1m,
                        Quantity = 2
                    },
                    new CustomerOrder.PackageSelection()
                    {
                        PackageStockName = "GSS-A5 SATCHEL*",
                        HeightCm = 5,
                        WidthCm = 1,
                        Quantity = 3
                    } 
                }
            }).Result;

            Assert.IsTrue(orders[0].Result);
        }

        [TestMethod]
        public void CustomerOrders_Get_Recent_Created()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var orders = client.CustomerOrders_GetAsync(DateTime.Now.AddDays(-2), DateTime.Now, true, true).Result;

            Assert.IsTrue(orders.Count > 0);
        }

        [TestMethod]
        public void CustomerOrders_Get_With_Products()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var orders = client.CustomerOrders_GetAsync("test1-" + DateTime.Now.ToString("yy-MM-dd") + "-products", true).Result;


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
                        StreetAddress = "152 Woodside Road",
                        Suburb = "Oxford",
                        City = "Oxford",
                        PostCode = "7495",
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
                            Height = 32,
                            Length = 32,
                            Id = 0,
                            Width = 38,
                            Kg = 4.91M,
                            Name = "CUSTOM",
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
        public void CreateDomesticOutboundShipment_WithPRN()
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
                Outputs = new List<RatesQueryOrShipmentRequest.OutputEnum>() { RatesQueryOrShipmentRequest.OutputEnum.GOPRINT_PRN }
            }).Result;
            File.WriteAllBytes(@"C:\Temp\TempFiles\Test.prn", data.Consignments[0].OutputFiles["GOPRINT_PRN"][0]);
        }        
        [TestMethod]
        public void CreateDomesticOutboundShipment_User_Configured_Label()
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
                Outputs = new List<RatesQueryOrShipmentRequest.OutputEnum>() { RatesQueryOrShipmentRequest.OutputEnum.USER_CONFIGURED }
            }).Result;
            File.WriteAllBytes($"C:\\Temp\\TempFiles\\{data.Consignments[0].Connote}.pdf", data.Consignments[0].OutputFiles["USER_CONFIGURED"][0]);
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
            var payload = new RatesQueryOrShipmentRequest
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
            };
            var rates = client.RatesQuery_GetAsync(payload).Result;

            var conn = client.Shipment_CreateAsync(payload, rates.Available.First().QuoteId).Result;

            Assert.IsTrue(conn.Consignments.Count > 0);
        }

        [TestMethod]
        public void GetLabelopeLabel()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var data = client.Labels_GetAsync("APD00020553", LabelFormat.LABEL_PDF_LABELOPE).Result;
            File.WriteAllBytes(@"C:\Temp\TempFiles\ADP00020553.pdf", data.First());
        }
        [TestMethod]
        public void GetLabelPrnFile()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var data = client.Labels_GetAsync("283341195920", LabelFormat.GOPRINT_PRN).Result;
            File.WriteAllBytes(@"C:\Temp\TempFiles\Test.prn", data.First());
        }
        [TestMethod]
        public void GetLabelFile_User_Configured()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);
            var data = client.Labels_GetAsync("283341195920", LabelFormat.USER_CONFIGURED).Result;
            File.WriteAllBytes(@"C:\Temp\TempFiles\283341195920.pdf", data.First());
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


        [TestMethod]
        public void AddressValidation_IncorrectSuburb()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);

            var data = new AddressToValidate
            {
                Consignee = "0123456789 0123456789 0123456789 0123456789",
                Address = new AddressToValidate.AddressObject
                {
                    BuildingName = "",
                    StreetAddress = "1 Some Street",
                    Suburb = "MascotX",
                    City = "NSW",
                    PostCode = "2020",
                    CountryCode = "AU",
                    IsRural = false
                },
                Email = "",
                ContactPerson = "",
                PhoneNumber = "",
                DeliveryInstructions = ""
            };

            var rsp = client.AddressValidation_PostAsync(data).Result;

            Assert.IsTrue(rsp.Errors.Any());
        }
        [TestMethod]
        public void AddressValidation_CorrectSuburb()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);

            var data = new AddressToValidate
            {
                Consignee = "0123456789 0123456789 0123456789 0123456789",
                Address = new AddressToValidate.AddressObject
                {
                    BuildingName = "",
                    StreetAddress = "1 Some Street",
                    Suburb = "Mascot",
                    City = "NSW",
                    PostCode = "2020",
                    CountryCode = "AU",
                    IsRural = false
                },
                Email = "",
                ContactPerson = "",
                PhoneNumber = "",
                DeliveryInstructions = ""
            };

            var rsp = client.AddressValidation_PostAsync(data).Result;

            Assert.IsFalse(rsp.Errors.Any());
        }
        [TestMethod]
        public void AddressValidation_IsRural()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);

            var data = new AddressToValidate
            {
                Consignee = "0123456789 0123456789 0123456789 0123456789",
                Address = new AddressToValidate.AddressObject
                {
                    BuildingName = "",
                    StreetAddress = "1 Chesham Lane",
                    Suburb = "Clevedon",
                    City = "Auckland",
                    PostCode = "2248",
                    CountryCode = "NZ",
                    IsRural = false
                },
                Email = "",
                ContactPerson = "",
                PhoneNumber = "",
                DeliveryInstructions = ""
            };

            var rsp = client.AddressValidation_PostAsync(data).Result;

            Assert.IsTrue(rsp.Address.AvailableServices.Any(a => a.IsRural));
        }

        [TestMethod]
        public async Task GetShipments_Recent()
        {
            GoSweetSpotApiClient client = new GoSweetSpotApiClient(api_token);

            var results = await client.Shipment_GetAsync(new List<string>(), new List<string>(), DateTime.UtcNow.AddDays(-7), DateTime.UtcNow);
            Assert.IsTrue(results.Count > 0);
        }

    }
}
