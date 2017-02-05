using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.DependencyModel;

namespace DotNetLive.Framework.Testing
{
    public class TestHostingEnvironment : IHostingEnvironment
    {
        public TestHostingEnvironment()
        {
            this.ApplicationName = "UnitTest Application";
            this.EnvironmentName = "UnitTesting";

            var workDirectory = PlatformServices.Default.Application.ApplicationBasePath;
            this.ContentRootPath = workDirectory.IndexOf($@"{Path.DirectorySeparatorChar}bin") > 0 ? workDirectory.Substring(0, workDirectory.IndexOf($@"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase)) : workDirectory;
            this.ContentRootFileProvider = new PhysicalFileProvider(this.ContentRootPath);

            this.WebRootPath = null;
            this.WebRootFileProvider = new NullFileProvider();
        }
        public string ApplicationName { get; set; }

        public IFileProvider ContentRootFileProvider { get; set; }

        public string ContentRootPath { get; set; }

        public string EnvironmentName { get; set; }

        public IFileProvider WebRootFileProvider { get; set; }

        public string WebRootPath { get; set; }
    }
}
