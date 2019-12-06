using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GoSweetSpotApiClientLib
{
    class Common
    {
        public static HttpClient GetHttpClient(string apiToken)
        {
            HttpClient client;
            MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();

            client = new HttpClient();
            client.BaseAddress = new Uri("https://dev-ship.gosweetspot.com");
            client.DefaultRequestHeaders.Add("access_key", apiToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "GoSweetSpotApiClient/" + LibraryVersion);

            return client;
        }
        public static string GetTempFolder()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "TempGSS\\";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
        public static string LibraryVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
    }
}
