using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoSweetSpotApiClientLib;

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

            Assert.IsTrue(orders[0].Success);
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

            Assert.IsTrue(orders[0].Success);
        }
    }
}
