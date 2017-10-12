using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMYKhub.ResellerApi.Client.Manufacturing
{
    /// <summary>
    /// Interface for accessing the CMYKhub Hublink REST API Manufacturing functions
    /// </summary>
    public interface IHublinkManufacturingClient
    {
        /// <summary>
        /// Creates an order for printing
        /// </summary>
        Task<CreatedOrderResource> CreateOrderAsync(CreateOrderRequest request);
        
        /// <summary>
        /// Creates a price for a standard product
        /// </summary>
        Task<ProductPrice> CreatePriceAsync(StandardPriceRequest request);

        /// <summary>
        /// Creates a price for a booklet product
        /// </summary>
        Task<ProductPrice> CreatePriceAsync(BookletProductRequest request);

        /// <summary>
        /// Returns the customer with the given id
        /// </summary>
        Task<Customer> GetCustomerAsync(string id);

        /// <summary>
        /// Returns all customers of the reseller
        /// </summary>
        Task<IEnumerable<Customer>> GetCustomersAsync();

        /// <summary>
        /// Returns the finishing with the given id
        /// </summary>
        Task<Finishing> GetFinishingAsync(string id);

        /// <summary>
        /// Returns all possible finishings
        /// </summary>
        Task<IEnumerable<Finishing>> GetFinishingsAsync();

        /// <summary>
        /// Returns finishings applicable to the given specification
        /// </summary>
        Task<IEnumerable<Finishing>> GetFinishingsByAvailableAsync(FinishingAvailableSpec spec);

        /// <summary>
        /// Returns the finishings filtered by the given name
        /// </summary>
        Task<IEnumerable<Finishing>> GetFinishingsByNameAsync(string name);

        /// <summary>
        /// Returns the order with the given id
        /// </summary>
        Task<Order> GetOrderAsync(string id);

        /// <summary>
        /// Returns all orders placed
        /// </summary>
        Task<IEnumerable<Order>> GetOrdersAsync();

        /// <summary>
        /// Returns the paper with the given id
        /// </summary>
        Task<Paper> GetPaperAsync(string id);

        /// <summary>
        /// Returns all papers
        /// </summary>
        Task<IEnumerable<Paper>> GetPapersAsync();

        /// <summary>
        /// Returns papers filtered by the given name
        /// </summary>
        Task<IEnumerable<Paper>> GetPapersByNameAsync(string name);

        /// <summary>
        /// Returns the product with the given id
        /// </summary>
        Task<Product> GetProductAsync(string id);

        /// <summary>
        /// Returns all products
        /// </summary>
        Task<IEnumerable<Product>> GetProductsAsync();
        
        /// <summary>
        /// Returns products filtered by the given name
        /// </summary>
        Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
    }
}