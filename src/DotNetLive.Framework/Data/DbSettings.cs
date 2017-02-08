using Npgsql;

namespace DotNetLive.Framework.Data
{
    public class DbSettings
    {
        public string QueryDbConnectionString { get; set; }
        public string CommandDbConnectionString { get; set; }
    }
}
