using System.Collections.Generic;
using System.Linq;

namespace CMYKhub.ResellerApi.Client.Extensions
{
    public static class LinkExtensions
    {
        public static Link FindLinkByRelation(this IEnumerable<Link> links, string rel)
        {
            return links.SingleOrDefault(x => x.Relation == rel);
        }
    }
}
