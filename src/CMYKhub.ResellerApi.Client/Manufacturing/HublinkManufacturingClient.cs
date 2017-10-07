using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMYKhub.ResellerApi.Client.Extensions;

namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    public class HublinkManufacturingClient : HublinkClient
    {
        private const string OrderingRelation = "http://schemas.cmykhub.com/api/orders";
        private const string ManufacturingOrders = "http://schemas.cmykhub.com/api/hublink/relations/order";
        private const string ManufacturingCustomers = "http://schemas.cmykhub.com/api/customers/relations";
        private const string ManufacturingPapers = "http://schemas.cmykhub.com/api/hublink/relations/papers";
        private const string ManufacturingFinishings = "http://schemas.cmykhub.com/api/hublink/relations/finishings";
        private const string ManufacturingFinishingsAvailable = "http://schemas.cmykhub.com/api/hublink/relations/finishings/available";
        private const string ManufacturingProducts = "http://schemas.cmykhub.com/api/hublink/relations/products";
        private const string ManufacturingPricingStandard = "http://schemas.cmykhub.com/api/hublink/relations/pricing/standard";
        private const string ManufacturingPricingBooklet = "http://schemas.cmykhub.com/api/hublink/relations/pricing/booklet";

        public HublinkManufacturingClient(IHttpClientFactory clientFactory, string baseUri, string resellerId, string apiKey)
            : base(clientFactory, baseUri, resellerId, apiKey) { }

        protected async Task<Discovery> DiscoverManufacturingAsync()
        {
            var rootDiscovery = await base.DiscoverAsync();
            var manufacturingLink = rootDiscovery.Links.Single(x => x.Relation == OrderingRelation);
            return await GetAsync<Discovery>(manufacturingLink.Uri);
        }

        public async Task<Order> GetOrderAsync(string id)
        {
            var discovery = await DiscoverManufacturingAsync();
            var ordersLink = discovery.Links.FindLinkByRelation(ManufacturingOrders);
            var orderUri = new UriBuilder(ordersLink.Uri) { Query = $"orderid={id}" }.Uri.ToString();
            return await GetAsync<Order>(orderUri);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            var discovery = await DiscoverManufacturingAsync();
            var ordersLink = discovery.Links.FindLinkByRelation(ManufacturingOrders);
            return (await GetAsync<Orders>(ordersLink.Uri)).Items;
        }

        public async Task<Customer> GetCustomerAsync(string id)
        {
            var discovery = await DiscoverManufacturingAsync();
            var customersLink = discovery.Links.FindLinkByRelation(ManufacturingCustomers);
            var customerUri = new UriBuilder(customersLink.Uri) { Query = $"customerid={id}" }.Uri.ToString();
            return await GetAsync<Customer>(customerUri);
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            var discovery = await DiscoverManufacturingAsync();
            var customersLink = discovery.Links.FindLinkByRelation(ManufacturingCustomers);
            return (await GetAsync<Customers>(customersLink.Uri)).Items;
        }

        public async Task<Paper> GetPaperAsync(string id)
        {
            var discovery = await DiscoverManufacturingAsync();
            var papersLink = discovery.Links.FindLinkByRelation(ManufacturingPapers);
            var paperUri = new UriBuilder(papersLink.Uri) { Query = $"paperid={id}" }.Uri.ToString();
            return await GetAsync<Paper>(paperUri);
        }

        public async Task<IEnumerable<Paper>> GetPapersAsync()
        {
            var discovery = await DiscoverManufacturingAsync();
            var papersLink = discovery.Links.FindLinkByRelation(ManufacturingPapers);
            return (await GetAsync<Papers>(papersLink.Uri)).Items;
        }

        public async Task<IEnumerable<Paper>> GetPapersByNameAsync(string name)
        {
            var discovery = await DiscoverManufacturingAsync();
            var papersLink = discovery.Links.FindLinkByRelation(ManufacturingPapers);
            var paperUri = new UriBuilder(papersLink.Uri) { Query = $"name={name}" }.Uri.ToString();
            return (await GetAsync<Papers>(paperUri)).Items;
        }

        public async Task<Finishing> GetFinishingAsync(string id)
        {
            var discovery = await DiscoverManufacturingAsync();
            var finishingsLink = discovery.Links.FindLinkByRelation(ManufacturingFinishings);
            var finishingUri = new UriBuilder(finishingsLink.Uri) { Query = $"finishingid={id}" }.Uri.ToString();
            return await GetAsync<Finishing>(finishingUri);
        }

        public async Task<IEnumerable<Finishing>> GetFinishingsAsync()
        {
            var discovery = await DiscoverManufacturingAsync();
            var finishingsLink = discovery.Links.FindLinkByRelation(ManufacturingFinishings);
            return (await GetAsync<Finishings>(finishingsLink.Uri)).Items;
        }

        public async Task<IEnumerable<Finishing>> GetFinishingsByNameAsync(string name)
        {
            var discovery = await DiscoverManufacturingAsync();
            var finishingsLink = discovery.Links.FindLinkByRelation(ManufacturingFinishings);
            var finishingUri = new UriBuilder(finishingsLink.Uri) { Query = $"name={name}" }.Uri.ToString();
            return (await GetAsync<Finishings>(finishingUri)).Items;
        }

        public async Task<IEnumerable<Finishing>> GetFinishingsByAvailableAsync(FinishingAvailableSpec spec)
        {
            var discovery = await DiscoverManufacturingAsync();
            var finishingsLink = discovery.Links.FindLinkByRelation(ManufacturingFinishingsAvailable);
            var query = $"width={spec.Width}&height={spec.Height}&paperWeight={spec.PaperWeight}&printType={(int)spec.PrintType}";
            if (spec.Book != null)
                query = $"{query}&book.pp={spec.Book.Pp}&book.orientation={(int)spec.Book.Orientation}";
            var finishingUri = new UriBuilder(finishingsLink.Uri) { Query = query }.Uri.ToString();
            return (await GetAsync<Finishings>(finishingUri)).Items;
        }
        
        public async Task<Product> GetProductAsync(string id)
        {
            var discovery = await DiscoverManufacturingAsync();
            var productsLink = discovery.Links.FindLinkByRelation(ManufacturingProducts);
            var productUri = new UriBuilder(productsLink.Uri) { Query = $"productid={id}" }.Uri.ToString();
            return await GetAsync<Product>(productUri);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var discovery = await DiscoverManufacturingAsync();
            var productsLink = discovery.Links.FindLinkByRelation(ManufacturingProducts);
            return (await GetAsync<Products>(productsLink.Uri)).Items;
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            var discovery = await DiscoverManufacturingAsync();
            var productsLink = discovery.Links.FindLinkByRelation(ManufacturingProducts);
            var productUri = new UriBuilder(productsLink.Uri) { Query = $"name={name}" }.Uri.ToString();
            return (await GetAsync<Products>(productUri)).Items;
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
    }
}
