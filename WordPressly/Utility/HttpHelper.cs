using System;
using Newtonsoft.Json;
using System.Net;
using RestSharp;
using System.Threading.Tasks;

namespace WordPressly.Utility
{
    public class HttpHelper
    {
        protected static internal async Task<T> GetRequests<T>(string Route)
        {
            try
            {
                var Requests = new RestRequest(Route, Method.GET);
                var Response = await Platform.RestClient.ExecuteTaskAsync(Requests);

                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<T>(Response.Content);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
            }

            return default(T);
        }
    }
}
