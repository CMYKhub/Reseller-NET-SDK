using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMYKhub.ResellerApi.Client.Prepress
{
    /// <summary>
    /// Interface for accessing the CMYKhub Hublink REST API Prepress functions
    /// </summary>
    public interface IHublinkPrepressClient
    {
        /// <summary>
        /// Returns prepress info for the order with the given id
        /// </summary>
        Task<OrderResource> GetOrderAsync(string id);

        /// <summary>
        /// Uploads artwork for the order with the given id
        /// </summary>
        Task UploadArtworkAsync(string orderId, string hubId, IEnumerable<string> files);
    }
}