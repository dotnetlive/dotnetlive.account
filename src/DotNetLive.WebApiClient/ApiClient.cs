using Newtonsoft.Json;
using DotNetLive.WebApiClient.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DotNetLive.WebApiClient
{
    public class ApiClient
    {
        private const string EndpointPreffix = "api/";
        //
        public static ApiResponse<T> Get<T>(string apiUrl, string methodName, CoreQuery query = null)
        {
            return NExecute<T>(apiUrl, methodName, query);
        }

        public static ApiResponse<T> Post<T>(string apiUrl, string methodName, CoreQuery query = null, object postData = null)
        {
            return NExecute<T>(apiUrl, methodName, query, MethodType.Post, postData);
        }

        public static ApiResponse<T> NExecute<T>(string apiUrl, string methodName, CoreQuery query = null, MethodType method = MethodType.Get, object postData = null, bool useEndpointPreffix = true)
        {
            var result = new ApiResponse<T>();
            HttpClient client = CreateHttpClient(apiUrl);
            HttpResponseMessage response = null;
            string url = string.Empty;
            try
            {
                result.Content = ExecuteRequestInternal(useEndpointPreffix, methodName, query, method, postData, client, ref response, ref url, responseCallback: res => response.Content.ReadAsStringAsync());
                result.StatusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    result.ResponseResult = JsonConvert.DeserializeObject<T>(result.Content);
                    result.Success = true;
                }
                else
                {
                    //如果不成功，ApiMenssage会直接从Content中解析;
                }

            }
            catch (Exception ex)
            {
                //如Http请求发生异常,直接记录异常信息
                result.Exception = ex;
                result.Success = false;
            }
            return result;
        }

        public static ApiResponse<T> UploadFiles<T>(string apiUrl, string methodName, CoreQuery query = null, MethodType method = MethodType.Get, List<FileUpload> uploadFiles = null, bool useEndpointPreffix = true)
        {
            var result = new ApiResponse<T>();
            HttpClient client = CreateHttpClient(apiUrl);
            HttpResponseMessage response = null;
            string url = string.Empty;
            try
            {
                MultipartFormDataContent sendContent = new MultipartFormDataContent(string.Format("{0}{1}", new string('-', 10), DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture)));
                foreach (var item in uploadFiles)
                {
                    StreamContent streamContent = new StreamContent(item.Stream);
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue(item.ContentType);
                    var originalFileName = string.Format("\"{0}{1}\"", item.OriginalFileName, item.OriginalFileName.IndexOf(".") < 0 ? ".jpg" : string.Empty);
                    sendContent.Add(streamContent, item.NewFileName ?? originalFileName, originalFileName);
                }

                result.Content = ExecuteRequestInternal<string>(useEndpointPreffix, methodName, query, method, sendContent, client, ref response, ref url,
                    requestCallback: (a, b, c, d) =>
                    {
                        return c.PostAsync(d, b as HttpContent).Result;
                    },
                    responseCallback: rsp => rsp.Content.ReadAsStringAsync());

                result.StatusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    result.ResponseResult = JsonConvert.DeserializeObject<T>(result.Content);
                    result.Success = true;
                }
                else
                {
                    //如果不成功，ApiMenssage会直接从Content中解析;
                }

            }
            catch (Exception ex)
            {
                //如Http请求发生异常,直接记录异常信息
                result.Exception = ex;
                result.Success = false;
            }
            return result;
        }

        public static ApiResponse<Stream> DownloadFile(string apiUrl, string methodName, CoreQuery query = null, MethodType method = MethodType.Get, object postData = null, bool useEndpointPreffix = true)
        {
            var result = new ApiResponse<Stream>();
            HttpClient client = CreateHttpClient(apiUrl);
            HttpResponseMessage response = null;
            string url = string.Empty;
            try
            {
                result.ResponseResult = ExecuteRequestInternal(useEndpointPreffix, methodName, query, method, postData, client, ref response, ref url, responseCallback: res => response.Content.ReadAsStreamAsync());
                result.StatusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                    result.Success = true;
            }
            catch (Exception ex)
            {
                //如Http请求发生异常,直接记录异常信息
                result.Exception = ex;
                result.Success = false;
            }
            return result;
        }

        #region 基本HTTP请求
        /// <summary>
        /// 基本HTTP请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="useEndpointPreffix"></param>
        /// <param name="methodName"></param>
        /// <param name="query"></param>
        /// <param name="method"></param>
        /// <param name="postData"></param>
        /// <param name="client"></param>
        /// <param name="response"></param>
        /// <param name="url"></param>
        /// <param name="responseCallback"></param>
        /// <returns></returns>
        private static T ExecuteRequestInternal<T>(bool useEndpointPreffix, string methodName, CoreQuery query, MethodType method, object postData, HttpClient client, ref HttpResponseMessage response,
            ref string url, Func<MethodType, object, HttpClient, string, HttpResponseMessage> requestCallback = null, Func<HttpResponseMessage, Task<T>> responseCallback = null)
        {
            url = string.Format("{0}{1}{2}?{3}",
                useEndpointPreffix ? EndpointPreffix : string.Empty,
                string.IsNullOrEmpty(EndpointPreffix) || EndpointPreffix.EndsWith("/") || methodName.StartsWith("/")
                    ? string.Empty
                    : "/",
                methodName,
                query);
            TraceLog("APIClient", string.Format("Call API:{0} {1} ", method, url));

            url = AttachToken(url);

            //如果Query==null, 默认传false
            client.DefaultRequestHeaders.Add("IgnoreEnvelope", (query == null ? false : query.IgnoreEnvelope).ToString());

            using (client)
            {
                if (requestCallback != null)
                {
                    response = requestCallback(method, postData, client, url);
                }
                else
                {
                    switch (method)
                    {
                        case MethodType.Get:
                            response = client.GetAsync(url).Result;
                            break;
                        case MethodType.Post:
                            response = client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(postData))).Result;
                            break;
                        case MethodType.Delete:
                            response = client.DeleteAsync(url).Result;
                            break;
                        case MethodType.Put:
                            response = client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(postData))).Result;
                            break;
                    }
                }
            }
            //无论错误还是成功，都从流里面获取返回的文本
            return responseCallback(response).Result;
        }

        #endregion

        #region Http请求及URL辅助方法
        private static string AttachToken(string url)
        {
            //var apiToken = System.Web.Configuration.WebConfigurationManager.AppSettings["APITOKEN"];
            //if (!string.IsNullOrEmpty(apiToken))
            //{
            //    url = string.Concat(url, "&APITOKEN=", HttpUtility.UrlEncode(apiToken));
            //}
            return url;
        }

        private static HttpClient CreateHttpClient(string apiUrl)
        {
            if (!apiUrl.EndsWith("/"))
                apiUrl += "/";
            var client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("UserAgent", HttpContext.Current != null ? HttpContext.Current.Request.UserAgent : string.Empty);
            //client.DefaultRequestHeaders.Add("UserAddr", HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : string.Empty);
            return client;
        }
        #endregion

        static void TraceLog(string category, string message)
        {
            //if (HttpContext.Current != null)
            //    HttpContext.Current.Trace.Write(category, message);
        }
    }
}
