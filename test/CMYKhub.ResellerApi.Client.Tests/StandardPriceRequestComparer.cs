using System.Linq;
using CMYKhub.ResellerApi.Client.Manufacturing;

namespace CMYKhub.ResellerApi.Client.Tests
{
    class StandardPriceRequestComparer
    {
        internal static bool Compare(StandardPriceRequest left, StandardPriceRequest right)
        {
            return left.DeliveryType == right.DeliveryType &&
                   left.FinishedSize.Width == right.FinishedSize.Width &&
                   left.FinishedSize.Height == right.FinishedSize.Height &&
                   left.Finishing.Length == right.Finishing.Length &&
                   left.Finishing.All(x => right.Finishing.Any(y => y.FinishingId == x.FinishingId && y.NoItems == x.NoItems)) &&
                   left.FreightProviderId == right.FreightProviderId &&
                   left.Kinds == right.Kinds &&
                   left.ProductId == right.ProductId &&
                   left.Quantity == right.Quantity;
        }
    }
}
