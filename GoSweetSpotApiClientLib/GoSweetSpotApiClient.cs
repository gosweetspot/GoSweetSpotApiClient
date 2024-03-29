using GoSweetSpotApiClientLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoSweetSpotApiClientLib
{
    public class GoSweetSpotApiClient
    {
        string API_TOKEN = string.Empty;
        private readonly int? siteId;

        /// <summary>
        /// Instantiates a GoSweetSpot Api client
        /// </summary>
        /// <param name="apiToken">Access key obtained from Preferences & Settings screen</param>
        /// <param name="siteId">If the user with the access key has access to multiple sites in the account, please supply the site id this API client should use.</param>
        public GoSweetSpotApiClient(string apiToken, int? siteId = null)
        {
            API_TOKEN = apiToken;
            this.siteId = siteId;
        }

        public async Task<List<CustomerOrder>> CustomerOrders_GetAsync(string ordernumber, bool includeProducts = false)
        {
            return await CustomerOrders_GetFilteredAsync(new List<string>(new string[] { ordernumber }), null, null, false, includeProducts);
        }
        public async Task<List<CustomerOrder>> CustomerOrders_GetAsync(List<string> ordernumbers, bool includeProducts = false)
        {
            return await CustomerOrders_GetFilteredAsync(ordernumbers, null, null, false, includeProducts);
        }
        public async Task<List<CustomerOrder>> CustomerOrders_GetAsync(DateTime createdFrom, DateTime createdTo, bool excludecompleted, bool includeProducts = false)
        {
            return await CustomerOrders_GetFilteredAsync(new List<string>(), createdFrom, createdTo, excludecompleted, includeProducts);
        }
        private async Task<List<CustomerOrder>> CustomerOrders_GetFilteredAsync(List<string> ordernumbers, DateTime? createdFrom, DateTime? createdTo, bool excludecompleted, bool includeProducts)
        {
            try
            {
                int page = 1;

                List<CustomerOrder> ret = new List<CustomerOrder>();

            reloop:

                var querystring = string.Format("packingslipno={0}&createdfrom={1}&createdto={2}&excludecompleted={3}&includeProducts={4}&page={5}",
                    string.Join(",", ordernumbers.ToArray()),
                    (createdFrom ?? DateTime.UtcNow.AddYears(-1)).ToString("u"),
                    (createdTo ?? DateTime.UtcNow.AddDays(1)).ToString("u"),
                    excludecompleted,
                    includeProducts,
                    page);

                HttpResponseMessage response = await Common.GetHttpClient(API_TOKEN, siteId).GetAsync("api/customerorders?" + querystring);

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsAsync<PagedResult<CustomerOrder>>();

                    ret.AddRange(data.Result.Results);

                    if (data.Result.Pages > page)
                    {
                        page += 1;
                        goto reloop;
                    }
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("{0} ({1}) - {2}", (int)response.StatusCode, response.ReasonPhrase, response.Content.ReadAsStringAsync().Result);
                    throw new HttpRequestException(sb.ToString());
                }

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CustomerOrdersSendResponse>> CustomerOrders_Send(CustomerOrder order)
        {
            return await CustomerOrders_Send(new List<CustomerOrder>(new CustomerOrder[] { order }));
        }
        public async Task<List<CustomerOrdersSendResponse>> CustomerOrders_Send(List<CustomerOrder> orders)
        {
            HttpResponseMessage response = await Common.GetHttpClient(API_TOKEN, siteId).PutAsJsonAsync("api/customerorders", orders);

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<List<CustomerOrdersSendResponse>>();
                return data.Result;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ({1}) - {2}", (int)response.StatusCode, response.ReasonPhrase, response.Content.ReadAsStringAsync().Result);
                throw new HttpRequestException(sb.ToString());
            }
        }

        public async Task<List<byte[]>> Labels_GetAsync(string consignmentnumber, LabelFormat format = LabelFormat.LABEL_PDF, bool rotate = false)
        {
            return await Labels_GetAsync(new List<string>(new string[] { consignmentnumber }), format, rotate);
        }
        private async Task<List<byte[]>> Labels_GetAsync(List<string> consignmentnumbers, LabelFormat format = LabelFormat.LABEL_PDF, bool rotate = false)
        {
            string querystring = string.Format("connote={0}&format={1}&rotate={2}",
                string.Join(",", consignmentnumbers.ToArray()),
                format,
                rotate);

            HttpResponseMessage response = await Common.GetHttpClient(API_TOKEN, siteId).GetAsync("api/labels?" + querystring);

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<byte[]>>().Result;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ({1}) - {2}", (int)response.StatusCode, response.ReasonPhrase, response.Content.ReadAsStringAsync().Result);
                throw new HttpRequestException(sb.ToString());
            }
        }

        public async Task<string> Labels_PrintAsync(string consignmentNumber)
        {
            HttpResponseMessage response = await Common.GetHttpClient(API_TOKEN, siteId).PostAsync("api/labels?connote=" + consignmentNumber, null);

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync();
                return data.Result;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ({1}) - {2}", (int)response.StatusCode, response.ReasonPhrase, response.Content.ReadAsStringAsync().Result);
                throw new HttpRequestException(sb.ToString());
            }
        }
        public async Task<string> Labels_PrintAsync(byte[] labelImage, int copies, string printername = "")
        {
            var post = new
            {
                Copies = copies,
                Image = labelImage,
                PrintToPrinter = printername
            };

            HttpResponseMessage response = await Common.GetHttpClient(API_TOKEN, siteId).PostAsJsonAsync("api/labels/enqueue", post);

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync();
                return data.Result;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ({1}) - {2}", (int)response.StatusCode, response.ReasonPhrase, response.Content.ReadAsStringAsync().Result);
                throw new HttpRequestException(sb.ToString());
            }
        }

        public async Task<List< PrintAgentPrinter>> Printers_GetAsync()
        {
            HttpResponseMessage response = await Common.GetHttpClient(API_TOKEN, siteId).GetAsync("api/printers");

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<List< PrintAgentPrinter>>();
                return data.Result;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ({1}) - {2}", (int)response.StatusCode, response.ReasonPhrase, response.Content.ReadAsStringAsync().Result);
                throw new HttpRequestException(sb.ToString());
            }
        }

        public async Task<AvailabeRatesResponse> RatesQuery_GetAsync(RatesQueryOrShipmentRequest request)
        {
            HttpResponseMessage response = await Common.GetHttpClient(API_TOKEN, siteId).PostAsJsonAsync("api/rates", request);

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<AvailabeRatesResponse>();
                return data.Result;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ({1}) - {2}", (int)response.StatusCode, response.ReasonPhrase, response.Content.ReadAsStringAsync().Result);
                throw new HttpRequestException(sb.ToString());
            }

        }

        public async Task<CreateShipmentResponse> Shipment_CreateAsync(RatesQueryOrShipmentRequest request)
        {
            return await Shipment_CreateAsync(request, Guid.Empty);
        }
        public async Task<CreateShipmentResponse> Shipment_CreateAsync(RatesQueryOrShipmentRequest request, Guid quoteId)
        {
            request.QuoteId = quoteId;

            HttpResponseMessage response = await Common.GetHttpClient(API_TOKEN, siteId).PostAsJsonAsync("api/shipments", request);

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var data = response.Content.ReadAsAsync<CreateShipmentResponse>();
                return data.Result;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ({1}) - {2}", (int)response.StatusCode, response.ReasonPhrase, response.Content.ReadAsStringAsync().Result);
                throw new HttpRequestException(sb.ToString());
            }

        }

        public async Task<List<ShipmentStatusFull>> Shipment_GetAsync(List<string> consignmentNumbers, List<string> ordernumbers, DateTime? lastupdateminutc, DateTime? lastupdatemaxutc)
        {
            int page = 1;
            List<ShipmentStatusFull> ret = new List<ShipmentStatusFull>();

        reloop:
            var querystring = string.Format("?shipments={0}&ordernumbers={1}&lastupdateminutc={2}&lastupdatemaxutc={3}&page={4}",
                   string.Join(",", consignmentNumbers.ToArray()),
                   string.Join(",", ordernumbers.ToArray()),
                   (lastupdateminutc ?? DateTime.UtcNow.AddYears(-30)).ToString("U"),
                   (lastupdatemaxutc ?? DateTime.UtcNow.AddDays(1)).ToString("U"),
                   page);

            HttpResponseMessage response = await Common.GetHttpClient(API_TOKEN, siteId).GetAsync("api/shipments" + querystring);

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<PagedResult<ShipmentStatusFull>>();
                ret.AddRange(data.Result.Results);

                if (data.Result.Pages > page)
                {
                    page += 1;
                    goto reloop;
                }
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ({1}) - {2}", (int)response.StatusCode, response.ReasonPhrase, response.Content.ReadAsStringAsync().Result);
                throw new HttpRequestException(sb.ToString());
            }

            return ret;
        }

        public async Task<DeleteShipmentResponse> Shipment_DeleteAsync(List<string> consignmentNumbers)
        {
            var retval = new DeleteShipmentResponse();
            string querystring = "?id=" + String.Join(",", consignmentNumbers);
            HttpResponseMessage response = await Common.GetHttpClient(API_TOKEN, siteId).DeleteAsync("api/shipments" + querystring);
            if(response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsAsync<Dictionary<string, string>>().Result;
                foreach (var line in res)
                {
                    retval.Results.Add(new DeleteShipmentResponse.Result()
                    {
                        ConsignmentNumber = line.Key,
                        Message = line.Value
                    });
                }
            }

            return retval;
        }

        public async Task<string> PickupBooking_PostAsync(BookPickupRequest request)
        {
            HttpResponseMessage response = await Common.GetHttpClient(API_TOKEN, siteId).PostAsJsonAsync("api/bookpickup", request);

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ({1}) - {2}", (int)response.StatusCode, response.ReasonPhrase, response.Content.ReadAsStringAsync().Result);
                throw new HttpRequestException(sb.ToString());
            }
        }

        // Address Validation
        public async Task<AddressValidationResponse> AddressValidation_PostAsync(AddressToValidate request)
        {
            HttpResponseMessage response = await Common.GetHttpClient(API_TOKEN, siteId).PostAsJsonAsync("v2/addressvalidation", request);

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<AddressValidationResponse>().Result;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ({1}) - {2}", (int)response.StatusCode, response.ReasonPhrase, response.Content.ReadAsStringAsync().Result);
                throw new HttpRequestException(sb.ToString());
            }
        }

        // Stock Size
        public async Task<List<StockSizesResponse>> StockSizes_GetAsync()
        {
            HttpResponseMessage response = await Common.GetHttpClient(API_TOKEN, siteId).GetAsync("api/stocksizes");

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<List<StockSizesResponse>>().Result;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ({1}) - {2}", (int)response.StatusCode, response.ReasonPhrase, response.Content.ReadAsStringAsync().Result);
                throw new HttpRequestException(sb.ToString());
            }
        }
        public async Task<StockSizesResponse> StockSizes_PostAsync(StockSizesRequest request)
        {
            HttpResponseMessage response = await Common.GetHttpClient(API_TOKEN, siteId).PostAsJsonAsync("api/stocksizes", request);

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<StockSizesResponse>().Result;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ({1}) - {2}", (int)response.StatusCode, response.ReasonPhrase, response.Content.ReadAsStringAsync().Result);
                throw new HttpRequestException(sb.ToString());
            }
        }
        public async Task<Dictionary<string,string>> StockSizes_DeleteAsync(string ids)
        {
            HttpResponseMessage response = await Common.GetHttpClient(API_TOKEN, siteId).DeleteAsync($"api/stocksizes?id={ids}");

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Dictionary<string,string>>().Result;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ({1}) - {2}", (int)response.StatusCode, response.ReasonPhrase, response.Content.ReadAsStringAsync().Result);
                throw new HttpRequestException(sb.ToString());
            }
        }



    }
}
