using System.Linq;
using CMYKhub.ResellerApi.Client.Manufacturing;

namespace CMYKhub.ResellerApi.Client.Tests
{
    class BookletPriceComparer
    {
        internal static bool Compare(BookletProductRequest left, BookletProductRequest right)
        {
            return left.DeliveryType == right.DeliveryType &&
                   left.FinishedSize.Width == right.FinishedSize.Width &&
                   left.FinishedSize.Height == right.FinishedSize.Height &&

                   left.BindingId == right.BindingId &&

                   left.Body.PaperId == right.Body.PaperId &&
                   left.Body.Pp == right.Body.Pp &&

                   CompareCover(left.Cover, right.Cover) &&

                   left.FreightProviderId == right.FreightProviderId &&
                   left.Orientation == right.Orientation &&
                   left.PrintType == right.PrintType &&
                   left.Quantity == right.Quantity;
        }

        private static bool CompareCover(BookletCoverSection left, BookletCoverSection right)
        {
            if (left == null && right == null) return true;
            if (left == null) return false;
            if (right == null) return false;


            return left.PaperId == right.PaperId &&
                   left.Pp == right.Pp &&
                   left.ProductId == right.ProductId &&
                   left.Finishing.Length == right.Finishing.Length &&
                   left.Finishing.All(x => right.Finishing.Any(y => y.FinishingId == x.FinishingId && y.NoItems == x.NoItems));
        }
    }
}
