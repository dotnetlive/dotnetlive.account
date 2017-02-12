using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLive.WebApiClient
{
    public class ApiResponse<T> : IApiResponse
    {
        [JsonIgnore]
        private ApiMessage _apiMessage;

        [JsonIgnore]
        private string _content;

        public T ResponseResult { get; set; }

        [JsonIgnore]
        public bool Success { get; set; }

        [JsonIgnore]
        public Exception Exception { get; set; }

        [JsonIgnore]
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                ApiMessage = null;
            }
        }

        [JsonIgnore]
        public ApiMessage ApiMessage
        {
            get
            {
                if (Success)
                    return null;

                if (_apiMessage == null && !string.IsNullOrEmpty(Content))
                {
                    try
                    {
                        _apiMessage = ApiMessage.ToMessage(Content);
                    }
                    catch (Exception ex)
                    {
                        Exception = new Exception(ex.Message, Exception);
                    }
                }
                if (_apiMessage == null || string.IsNullOrWhiteSpace(_apiMessage.ErrorCode) || string.IsNullOrWhiteSpace(_apiMessage.ErrorMessage))
                    return _apiMessage = new ApiMessage() { ErrorCode = "GeneralError", ErrorMessage = Exception == null ? "Http Error:" + StatusCode.ToString() : Exception.FullMessage() };

                return _apiMessage;
            }
            private set { _apiMessage = value; }
        }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        [JsonIgnore]
        public virtual bool IsError
        {
            get { return !Success; }
        }
    }

    public interface IApiResponse
    {
        ApiMessage ApiMessage { get; }
    }
}
