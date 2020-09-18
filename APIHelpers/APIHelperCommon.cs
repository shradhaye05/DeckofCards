using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DeckofcardsApi.APIHelpers
{
    class APIHelperCommon
    {
        /// <summary>
        /// Get service call asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="action"></param>   
        /// <param name="query"></param>
        /// <returns></returns>
        public static ResponseModel<T> Get<T>(string ServiceUri, string controller, string query,
            Dictionary<string, string> headers = null)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = System.TimeSpan.Parse(Constants.TimeOut);

                var serviceUri = new Uri(ServiceUri);
                var response = default(ResponseModel<T>);
                client.BaseAddress = serviceUri;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var actionUri = serviceUri + controller + "/" + query;


                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                var webResponse = client.GetAsync(actionUri).GetAwaiter().GetResult();
                var result = webResponse.Content.ReadAsStringAsync().Result;
                if (webResponse.IsSuccessStatusCode)
                {
                    response = new ResponseModel<T>
                    {
                        StatusCode = webResponse.StatusCode,
                        Data = CommonHelper.TryDeserializeResponse<T>(result),
                        Response = webResponse,
                        ActualResponse = result
                    };

                }
                else
                {
                    //Exceptions to be handled
                    response = new ResponseModel<T>
                    {
                        StatusCode = webResponse.StatusCode,
                        Response = webResponse,
                        ActualResponse = result
                    };
                }
                return response;
            }
        }

    }
}
