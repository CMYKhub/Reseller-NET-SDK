using System.Collections.Generic;
using CMYKhub.ResellerApi.Client.Extensions;

namespace CMYKhub.ResellerApi.Client.Prepress
{
    public class PrepressApiDiscovery
    {
        public const string Self = "self";
        public const string ChunksRelation = "http://schemas.cmykhub.com/api/Relations/Files/Chunks";
        public const string OrdersRelation = "http://schemas.cmykhub.com/api/Relations/Orders";
        public const string UploadRelation = "http://schemas.cmykhub.com/api/Relations/Uploads";
        public const string PackageRelation = "http://schemas.cmykhub.com/api/Relations/Orders/Artwork";
        public const string UploadContentRelation = "http://schemas.cmykhub.com/api/Relations/Files";

        public PrepressApiDiscovery(IEnumerable<Link> links)
        {
            var packageLink = links.FindLinkByRelation(PackageRelation);
            if (packageLink != null)
                PackageUrl = packageLink.Uri;

            var uploadLink = links.FindLinkByRelation(UploadRelation);
            if (uploadLink != null)
                UploadUrl = uploadLink.Uri;

            var ordersLink = links.FindLinkByRelation(OrdersRelation);
            if (ordersLink != null)
                OrdersUrl = ordersLink.Uri;
        }

        public string UploadUrl { get; set; }
        public string OrdersUrl { get; set; }
        public string PackageUrl { get; set; }
    }
}
