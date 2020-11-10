using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RuneSharing
{
    class HttpConnections
    {
        static HttpClient client = new HttpClient();

        public static void Initialize(string url, string auth)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);            
        }
        
        public static async Task<RunePage> GetRunes(string path)
        {
            RunePage runes = null;            

            HttpResponseMessage response = await client.GetAsync(path);
            if(response.IsSuccessStatusCode)
            {
                runes = await response.Content.ReadAsAsync<RunePage>();
            }
            return runes;
        }

        public static async Task<HttpStatusCode> DeleteRunes(string request)
        {
            HttpResponseMessage response = await client.DeleteAsync(request);
            return response.StatusCode;
        }

        public static async Task<Uri> PostRunesAsync(string request, RunePage runes)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(request, runes);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
    }
}
