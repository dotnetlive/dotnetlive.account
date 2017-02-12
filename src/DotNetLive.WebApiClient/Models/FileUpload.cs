using System.Collections.Generic;
using System.IO;

namespace DotNetLive.WebApiClient
{
    public class FileUpload
    {
        public FileUpload(string originalFileName, string contentType, Stream stream)
        {
            OriginalFileName = originalFileName;
            ContentType = contentType;
            Stream = stream;
        }

        public string NewFileName { get; set; }
        public string OriginalFileName { get; set; }
        public string ContentType { get; set; }
        public Stream Stream { get; set; }
        public int FileType { get; set; }
    }
}
