using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace CMYKhub.ResellerApi.Client.Prepress
{
    public class PrepressJsonMediaTypeFormatter : JsonMediaTypeFormatter
    {
        public PrepressJsonMediaTypeFormatter() : base()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/vnd.cmykhub+json"));
        }
    }
}
