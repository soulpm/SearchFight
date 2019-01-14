using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SearchFight.Utilities
{
    public class ResponseService
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> GetAsync(string urlPath, List<string[]> headers = null)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (headers != null)
            {
                foreach (string[] header in headers)
                {
                    client.DefaultRequestHeaders.Add(header[0], header[1]);
                }
            }
            
            var response = await client.GetAsync(urlPath);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();    
            }
            
            throw new HttpRequestException("There was an error processing your request. Please try again later ...");
        }
    }
}