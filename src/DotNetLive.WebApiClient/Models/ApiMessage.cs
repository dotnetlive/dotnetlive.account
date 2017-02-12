using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLive.WebApiClient
{
    public class ApiMessage
    {
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public static ApiMessage ToMessage(string content)
        {
            return JsonConvert.DeserializeObject<ApiMessage>(content);
        }

        public override string ToString()
        {
            return string.Format("{0}", ErrorMessage);
        }
    }
}
