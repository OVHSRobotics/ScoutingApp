namespace FRCScout
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web;

    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path)
        {
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            string oldFileName = headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            string newFileName = DateTime.UtcNow.ToString("yyyyMMddThhmmssFFFFFF");
            return newFileName + "." + oldFileName.Split('.').Last();
        }
    }
}