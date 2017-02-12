using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLive.WebApiClient.Query
{
    public class SessionQuery : CoreQuery
    {
        [Query(Name = "sessionKey")]
        public string SessionKey { get; set; }

        public SessionQuery()
        {

        }

        public SessionQuery(SessionQuery query)
        {
            this.SessionKey = query.SessionKey;
        }
    }
}
