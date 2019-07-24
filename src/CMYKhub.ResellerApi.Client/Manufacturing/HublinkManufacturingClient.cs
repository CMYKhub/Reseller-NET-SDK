using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMYKhub.ResellerApi.Client.Extensions;

namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    /// <summary>
    /// Client interface to Reseller API Manufacturing methods
    /// </summary>
    public class HublinkManufacturingClient : HublinkClient, IHublinkManufacturingClient
    {
        private const string OrderingRelation = "http://schemas.cmykhub.com/api/orders";
        private const string ManufacturingOrders = "http://schemas.cmykhub.com/api/hublink/relations/order";
        private const string ManufacturingCustomers = "http://schemas.cmykhub.com/api/customers/relations";
        private const string ManufacturingPapers = "http://schemas.cmykhub.com/api/hublink/relations/papers";
        private const string ManufacturingFinishings = "http://schemas.cmykhub.com/api/hublink/relations/finishings";
        private const string ManufacturingProducts = "http://schemas.cmykhub.com/api/hublink/relations/products";
        private const string ManufacturingProductsWideFormat = "http://schemas.cmykhub.com/api/hublink/relations/products/wideFormat";
        private const string ManufacturingPricingStandard = "http://schemas.cmykhub.com/api/hublink/relations/pricing/standard";
        private const string ManufacturingPricingBooklet = "http://schemas.cmykhub.com/api/hublink/relations/pricing/booklet";
        private const string ManufacturingPricingWideFormat = "http://schemas.cmykhub.com/api/hublink/relations/pricing/wideFormat";
        private const string ManufacturingCustomSizes = "http://schemas.cmykhub.com/api/hublink/relations/customSizes";
        private const string ManufacturingFixedSizes = "http://schemas.cmykhub.com/api/hublink/relations/fixedSizes";
        private const string ManufacturingQuoteTypes = "http://schemas.cmykhub.com/api/hublink/relations/quoteTypes";

        /// <summary>
        /// Construct to inject the client factory and settings
        /// </summary>
        /// <param name="clientFactory">The http client factory</param>
        /// <param name="settings">The client settings</param>
        public HublinkManufacturingClient(IHttpClientFactory clientFactory, ClientSettings settings)
            : base(clientFactory, settings) { }

        /// <summary>
        /// Discover the link relationship
        /// </summary>
        /// <returns>An asynchronous task with the link relationship</returns>
        protected async Task<Discovery> DiscoverManufacturingAsync()
        {
            var rootDiscovery = await base.DiscoverAsync();
            var manufacturingLink = rootDiscovery.Links.Single(x => x.Relation == OrderingRelation);
            return await GetAsync<Discovery>(manufacturingLink.Uri);
        }

        private async Task<T> GetByRelationAsync<T>(string relation, string query = null)
        {
            var discovery = await DiscoverManufacturingAsync();
            var link = discovery.Links.FindLinkByRelation(relation);
            var builder = new UriBuilder(link.Uri);
            if (!string.IsNullOrEmpty(query))
                builder.Query = query;
            var uri = builder.Uri.ToString();
            return await GetAsync<T>(uri);
        }

        /// <summary>
        /// Get order using order id
        /// </summary>
        /// <param name="id">The order id</param>
        /// <returns>An asynchronous task with the order record</returns>
        public Task<Order> GetOrderAsync(string id)
        {
            return GetByRelationAsync<Order>(ManufacturingOrders, $"orderid={id}");
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns>An asynchronous task with a list of order records</returns>
        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return (await GetByRelationAsync<Orders>(ManufacturingOrders)).Items;
        }

        /// <summary>
        /// Get customer using customer id
        /// </summary>
        /// <param name="id">The customer id</param>
        /// <returns>An asynchronous task with the customer record</returns>
        public Task<Customer> GetCustomerAsync(string id)
        {
            return GetByRelationAsync<Customer>(ManufacturingCustomers, $"customerid={id}");
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns>An asynchronous task with a list of customer records</returns>
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return (await GetByRelationAsync<Customers>(ManufacturingCustomers)).Items;
        }

        /// <summary>
        /// Get paper using paper id
        /// </summary>
        /// <param name="id">The paper id</param>
        /// <returns>An asynchronous task with the paper record</returns>
        public Task<Paper> GetPaperAsync(string id)
        {
            return GetByRelationAsync<Paper>(ManufacturingPapers, $"paperid={id}");
        }

        /// <summary>
        /// Get all papers
        /// </summary>
        /// <returns>An asynchronous task with a list of paper records</returns>
        public async Task<IEnumerable<Paper>> GetPapersAsync()
        {
            return (await GetByRelationAsync<Papers>(ManufacturingPapers)).Items;
        }

        /// <summary>
        /// Get all papers that contain a matching name
        /// </summary>
        /// <param name="name">The name used for the search</param>
        /// <returns>An asynchronous task with a list of paper records</returns>
        public async Task<IEnumerable<Paper>> GetPapersByNameAsync(string name)
        {
            return (await GetByRelationAsync<Papers>(ManufacturingPapers, $"name={name}")).Items;
        }

        /// <summary>
        /// Get finishing using finishing id
        /// </summary>
        /// <param name="id">The finishing id</param>
        /// <returns>An asynchronous task with the finishing record</returns>
        public Task<Finishing> GetFinishingAsync(string id)
        {
            return GetByRelationAsync<Finishing>(ManufacturingFinishings, $"finishingid={id}");
        }

        /// <summary>
        /// Get all finishing records
        /// </summary>
        /// <returns>An asynchronous task with a list of finishing records</returns>
        public async Task<IEnumerable<Finishing>> GetFinishingsAsync()
        {
            return (await GetByRelationAsync<Finishings>(ManufacturingFinishings)).Items;
        }

        /// <summary>
        /// Get all finishing records that contain a matching name
        /// </summary>
        /// <param name="name">The name used for the search</param>
        /// <returns>An asynchronous task with a list of finishing records</returns>
        public async Task<IEnumerable<Finishing>> GetFinishingsByNameAsync(string name)
        {
            return (await GetByRelationAsync<Finishings>(ManufacturingFinishings, $"name={name}")).Items;
        }

        /// <summary>
        /// Get all available finishing records that match the specification
        /// </summary>
        /// <param name="spec">The specification for finding the available finishing records</param>
        /// <returns>An asynchronous task with a list of finishing records</returns>
        public async Task<IEnumerable<Finishing>> GetFinishingsByAvailableAsync(FinishingAvailableSpec spec)
        {
            var query = $"width={spec.Width}&height={spec.Height}&paperWeight={spec.PaperWeight}&printType={(int)spec.PrintType}";
            if (spec.Book != null)
                query = $"{query}&pp={spec.Book.Pp}&orientation={(int)spec.Book.Orientation}";

            return (await GetByRelationAsync<Finishings>(ManufacturingFinishings, query)).Items;
        }

        /// <summary>
        /// Get product using product id
        /// </summary>
        /// <param name="id">The product id</param>
        /// <returns>An asynchronous task with the product record</returns>
        public Task<Product> GetProductAsync(string id)
        {
            return GetByRelationAsync<Product>(ManufacturingProducts, $"productid={id}");
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>An asynchronous task with a list of product records</returns>
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return (await GetByRelationAsync<Products>(ManufacturingProducts)).Items;
        }

        /// <summary>
        /// Get all products that contain a matching name
        /// </summary>
        /// <param name="name">The name used for the search</param>
        /// <returns>An asynchronous task with  a list of product records</returns>
        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            return (await GetByRelationAsync<Products>(ManufacturingProducts, $"name={name}")).Items;
        }

        /// <summary>
        /// Get wide format product using product id
        /// </summary>
        /// <param name="id">The wide format product id</param>
        /// <returns>An asynchronous task with the product record</returns>
        public Task<Product> GetWideFormatProductAsync(string id)
        {
            return GetByRelationAsync<Product>(ManufacturingProductsWideFormat, $"productid={id}");
        }

        /// <summary>
        /// Get all wide format products
        /// </summary>
        /// <returns>An asynchronous task with a list of products</returns>
        public async Task<IEnumerable<Product>> GetWideFormatProductsAsync()
        {
            return (await GetByRelationAsync<Products>(ManufacturingProductsWideFormat)).Items;
        }

        /// <summary>
        /// Get all wide format products that contains a matching name
        /// </summary>
        /// <param name="name">The name used for the search</param>
        /// <returns>An asynchronous task with a list of products</returns>
        public async Task<IEnumerable<Product>> GetWideFormatProductsByNameAsync(string name)
        {
            return (await GetByRelationAsync<Products>(ManufacturingProductsWideFormat, $"name={name}")).Items;
        }

        /// <summary>
        /// Create a standard price request
        /// </summary>
        /// <param name="request">The standard price request</param>
        /// <returns>An asynchronous task with a product price record</returns>
        public async Task<ProductPrice> CreatePriceAsync(StandardPriceRequest request)
        {
            var discovery = await DiscoverManufacturingAsync();
            var pricingLink = discovery.Links.FindLinkByRelation(ManufacturingPricingStandard);
            // Pre-validate the request
            if (request.FinishedSize.Width == 0 || request.FinishedSize.Height == 0) request.FinishedSize = null;
            if (string.IsNullOrEmpty(request.Finishing[0].FinishingId)) request.Finishing = null;
            return (await PostAsync<StandardPriceRequest, ProductPrice>(pricingLink.Uri, request));
        }

        /// <summary>
        /// Create a booklet price request
        /// </summary>
        /// <param name="request">The booklet price request</param>
        /// <returns>An asynchronous task with a product price record</returns>
        public async Task<ProductPrice> CreatePriceAsync(BookletProductRequest request)
        {
            var discovery = await DiscoverManufacturingAsync();
            var pricingLink = discovery.Links.FindLinkByRelation(ManufacturingPricingBooklet);
            return (await PostAsync<BookletProductRequest, ProductPrice>(pricingLink.Uri, request));
        }

        /// <summary>
        /// Create a wide format price request
        /// </summary>
        /// <param name="request">The wide format wide price request</param>
        /// <returns>An asynchronous task with a product price record</returns>
        public async Task<ProductPrice> CreatePriceAsync(WideFormatPriceRequest request)
        {
            var discovery = await DiscoverManufacturingAsync();
            var pricingLink = discovery.Links.FindLinkByRelation(ManufacturingPricingWideFormat);
            return (await PostAsync<WideFormatPriceRequest, ProductPrice>(pricingLink.Uri, request));
        }

        /// <summary>
        /// Create an order request
        /// </summary>
        /// <param name="request">The order request</param>
        /// <returns>An asynchronous task with a created order record</returns>
        public async Task<CreatedOrderResource> CreateOrderAsync(CreateOrderRequest request)
        {
            var discovery = await DiscoverManufacturingAsync();
            var pricingLink = discovery.Links.FindLinkByRelation(ManufacturingOrders);
            return (await PostAsync<CreateOrderRequest, CreatedOrderResource>(pricingLink.Uri, request));
        }

        /// <summary>
        /// Return custom size by the given id
        /// </summary>
        /// <param name="id">The id of the size</param>
        /// <returns>The custom size</returns>
        public Task<Size> GetCustomSizeAsync(string id)
        {
            return GetByRelationAsync<Size>(ManufacturingCustomSizes, $"sizeid={id}");
        }

        /// <summary>
        /// Returns a list of custom sizes
        /// </summary>
        /// <returns>A list of custom sizes</returns>
        public async Task<IEnumerable<Size>> GetCustomSizesAsync()
        {
            return (await GetByRelationAsync<Sizes>(ManufacturingCustomSizes)).Items;
        }

        /// <summary>
        /// Return fixed size by the given id
        /// </summary>
        /// <param name="id">The id of the size</param>
        /// <returns>The fixed size</returns>
        public Task<Size> GetFixedSizeAsync(string id)
        {
            return GetByRelationAsync<Size>(ManufacturingFixedSizes, $"sizeid={id}");
        }

        /// <summary>
        /// Returns a list of fixed sizes
        /// </summary>
        /// <returns>A list of fixed sizes</returns>
        public async Task<IEnumerable<Size>> GetFixedSizesAsync()
        {
            return (await GetByRelationAsync<Sizes>(ManufacturingFixedSizes)).Items;
        }

        /// <summary>
        /// Returns a list of fixed sizes for a given quote type
        /// </summary>
        /// <param name="quoteType">The quote type</param>
        /// <returns>A list of fixed sizes</returns>
        public async Task<IEnumerable<Size>> GetFixedSizesAsync(string quoteType)
        {
            return (await GetByRelationAsync<Sizes>(ManufacturingFixedSizes, $"quoteType={quoteType}")).Items;
        }

        /// <summary>
        /// Returns a list of quote types
        /// </summary>
        /// <returns>A list of quote types</returns>
        public async Task<IEnumerable<string>> GetQuoteTypesAsync()
        {
            return (await GetByRelationAsync<List<string>>(ManufacturingQuoteTypes));
        }
    }
}
