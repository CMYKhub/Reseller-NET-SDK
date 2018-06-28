using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMYKhub.ResellerApi.Client.Extensions;

namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class HublinkManufacturingClient : HublinkClient, IHublinkManufacturingClient
    {
        private const string OrderingRelation = "http://schemas.cmykhub.com/api/orders";
        private const string ManufacturingOrders = "http://schemas.cmykhub.com/api/hublink/relations/order";
        private const string ManufacturingCustomers = "http://schemas.cmykhub.com/api/customers/relations";
        private const string ManufacturingPapers = "http://schemas.cmykhub.com/api/hublink/relations/papers";
        private const string ManufacturingFinishings = "http://schemas.cmykhub.com/api/hublink/relations/finishings";
        private const string ManufacturingFinishingsAvailable = "http://schemas.cmykhub.com/api/hublink/relations/finishings/available";
        private const string ManufacturingProducts = "http://schemas.cmykhub.com/api/hublink/relations/products";
        private const string ManufacturingProductsWideFormat = "http://schemas.cmykhub.com/api/hublink/relations/products/wideFormat";
        private const string ManufacturingPricingStandard = "http://schemas.cmykhub.com/api/hublink/relations/pricing/standard";
        private const string ManufacturingPricingBooklet = "http://schemas.cmykhub.com/api/hublink/relations/pricing/booklet";

        public HublinkManufacturingClient(IHttpClientFactory clientFactory, ClientSettings settings)
            : base(clientFactory, settings) { }

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

        public Task<Order> GetOrderAsync(string id)
        {
            return GetByRelationAsync<Order>(ManufacturingOrders, $"orderid={id}");
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return (await GetByRelationAsync<Orders>(ManufacturingOrders)).Items;
        }

        public Task<Customer> GetCustomerAsync(string id)
        {
            return GetByRelationAsync<Customer>(ManufacturingCustomers, $"customerid={id}");
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return (await GetByRelationAsync<Customers>(ManufacturingCustomers)).Items;
        }

        public Task<Paper> GetPaperAsync(string id)
        {
            return GetByRelationAsync<Paper>(ManufacturingPapers, $"paperid={id}");
        }

        public async Task<IEnumerable<Paper>> GetPapersAsync()
        {
            return (await GetByRelationAsync<Papers>(ManufacturingPapers)).Items;
        }

        public async Task<IEnumerable<Paper>> GetPapersByNameAsync(string name)
        {
            return (await GetByRelationAsync<Papers>(ManufacturingPapers, $"name={name}")).Items;
        }

        public Task<Finishing> GetFinishingAsync(string id)
        {
            return GetByRelationAsync<Finishing>(ManufacturingFinishings, $"finishingid={id}");
        }

        public async Task<IEnumerable<Finishing>> GetFinishingsAsync()
        {
            return (await GetByRelationAsync<Finishings>(ManufacturingFinishings)).Items;
        }

        public async Task<IEnumerable<Finishing>> GetFinishingsByNameAsync(string name)
        {
            return (await GetByRelationAsync<Finishings>(ManufacturingFinishings, $"name={name}")).Items;
        }

        public async Task<IEnumerable<Finishing>> GetFinishingsByAvailableAsync(FinishingAvailableSpec spec)
        {
            var query = $"width={spec.Width}&height={spec.Height}&paperWeight={spec.PaperWeight}&printType={(int)spec.PrintType}";
            if (spec.Book != null)
                query = $"{query}&book.pp={spec.Book.Pp}&book.orientation={(int)spec.Book.Orientation}";

            return (await GetByRelationAsync<Finishings>(ManufacturingFinishingsAvailable, query)).Items;
        }

        public Task<Product> GetProductAsync(string id)
        {
            return GetByRelationAsync<Product>(ManufacturingProducts, $"productid={id}");
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return (await GetByRelationAsync<Products>(ManufacturingProducts)).Items;
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            return (await GetByRelationAsync<Products>(ManufacturingProducts, $"name={name}")).Items;
        }

        public Task<Product> GetWideFormatProductAsync(string id)
        {
            return GetByRelationAsync<Product>(ManufacturingProductsWideFormat, $"productid={id}");
        }

        public async Task<IEnumerable<Product>> GetWideFormatProductsAsync()
        {
            return (await GetByRelationAsync<Products>(ManufacturingProductsWideFormat)).Items;
        }

        public async Task<IEnumerable<Product>> GetWideFormatProductsByNameAsync(string name)
        {
            return (await GetByRelationAsync<Products>(ManufacturingProductsWideFormat, $"name={name}")).Items;
        }

        public async Task<ProductPrice> CreatePriceAsync(StandardPriceRequest request)
        {
            var discovery = await DiscoverManufacturingAsync();
            var pricingLink = discovery.Links.FindLinkByRelation(ManufacturingPricingStandard);
            return (await PostAsync<StandardPriceRequest, ProductPrice>(pricingLink.Uri, request));
        }

        public async Task<ProductPrice> CreatePriceAsync(BookletProductRequest request)
        {
            var discovery = await DiscoverManufacturingAsync();
            var pricingLink = discovery.Links.FindLinkByRelation(ManufacturingPricingBooklet);
            return (await PostAsync<BookletProductRequest, ProductPrice>(pricingLink.Uri, request));
        }

        public async Task<CreatedOrderResource> CreateOrderAsync(CreateOrderRequest request)
        {
            var discovery = await DiscoverManufacturingAsync();
            var pricingLink = discovery.Links.FindLinkByRelation(ManufacturingOrders);
            return (await PostAsync<CreateOrderRequest, CreatedOrderResource>(pricingLink.Uri, request));
        }
    }
}
