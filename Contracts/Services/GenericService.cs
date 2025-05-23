using BasicPaymentGateway.Contracts.Interfaces;
using System.Text;

namespace BasicPaymentGateway.Contracts.Services
{
    public class GenericService : IGenericService
    {
        public async Task<string> ConsumeRestAPI(string apiEndpoint, string? serializedRequest, Dictionary<string, string> headers, string method)
        {
            var apiResponse = "";

            var data = new StringContent(serializedRequest, Encoding.UTF8, "application/json");


            var endpointMethod = HttpMethod.Post;

            if (method == "get")
            {
                endpointMethod = HttpMethod.Get;
            }

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(endpointMethod, apiEndpoint))
                {
                    if (headers != null && headers.Count > 0)
                    {
                        foreach (var header in headers)
                        {
                            request.Headers.TryAddWithoutValidation($"{header.Key}", header.Value);
                        }
                    }


                    request.Headers.TryAddWithoutValidation("Accept", "application/json");
                    request.Content = data;
                    var response = await httpClient.SendAsync(request);
                    apiResponse = await response.Content.ReadAsStringAsync();
                }

                return apiResponse;
            }
        }
    }
}
