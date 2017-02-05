using Npgsql;

namespace DotNetLive.AccountWeb.Data
{
    public class DbSettings
    {
        public string QueryDbConnectionString { get; set; }
        public string CommandDbConnectionString { get; set; }
    }
}
