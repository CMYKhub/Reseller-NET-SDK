namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    //public enum QuoteTypeEnum
    //{
    //    /// <summary>
    //    /// Invalid quote type
    //    /// </summary>
    //    Invalid = 0,
    //    /// <summary>
    //    /// Flat sheet quote
    //    /// </summary>
    //    FlatSheet = 1,
    //    /// <summary>
    //    /// Basic booklet quote
    //    /// </summary>
    //    BasicBooklet = 2,
    //    /// <summary>
    //    /// Product Quote
    //    /// </summary>
    //    Product = 3,
    //    /// <summary>
    //    /// Custom quote
    //    /// </summary>
    //    Custom = 4,
    //    /// <summary>
    //    /// Centre group quote
    //    /// </summary>
    //    CentreGroup = 5,
    //    /// <summary>
    //    /// No quote available
    //    /// </summary>
    //    NotAvailable = 6,
    //    /// <summary>
    //    /// Wide Format quote
    //    /// </summary>
    //    WideFormat = 7
    //}

    public class QuoteTypes : ApiResource
    {
        public string[] Items { get;set; }
    }
}
