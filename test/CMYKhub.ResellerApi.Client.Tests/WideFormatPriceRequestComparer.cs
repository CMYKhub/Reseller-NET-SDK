using System.Collections.Generic;
using System.Linq;
using CMYKhub.ResellerApi.Client.Manufacturing;

namespace CMYKhub.ResellerApi.Client.Tests
{
    class WideFormatPriceRequestComparer
    {
        internal static bool Compare(WideFormatPriceRequest left, WideFormatPriceRequest right)
        {
            return left.DeliveryType == right.DeliveryType &&
                   left.FinishedSize.Width == right.FinishedSize.Width &&
                   left.FinishedSize.Height == right.FinishedSize.Height &&
                   left.Finishing.Length == right.Finishing.Length &&
                   left.Finishing.All(x => right.Finishing.Any(y => y.FinishingId == x.FinishingId && Compare(y.Config, x.Config))) &&
                   left.FreightProviderId == right.FreightProviderId &&
                   left.Kinds == right.Kinds &&
                   left.ProductId == right.ProductId &&
                   left.Quantity == right.Quantity;
        }

        private static bool Compare(IDictionary<string, string> left, IDictionary<string, string> right)
        {
            if (left == null && right == null) return true;
            if (right == null  && !left.Keys.Any() ) return true;
            if (left == null  && !right.Keys.Any()) return true;
            if (left == null) return false;
            if (right == null) return false;

            if (left.Keys.Count != right.Keys.Count) return false;
            if (!left.Keys.All(x => right.Keys.Contains(x))) return false;
            foreach (var key in left.Keys)
            {
                if (left[key] != right[key]) return false;
            }

            return false;
        }
    }
}
