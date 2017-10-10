using System;

namespace CMYKhub.ResellerApi.Client.Prepress
{
    public class FileUploadResource: ApiResource
    {
        public string Id { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
