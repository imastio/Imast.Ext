using System.Threading.Tasks;

namespace Imast.Ext.DiscoveryCore
{
    /// <summary>
    /// The discovery client interface
    /// </summary>
    public interface IDiscoveryClient
    {
        /// <summary>
        /// The discovery client type
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Gets the instance of the service
        /// </summary>
        /// <param name="service">The service</param>
        /// <returns></returns>
        Task<DiscoveryInstance> GetInstance(string service);

        /// <summary>
        /// Gets the next base service
        /// </summary>
        /// <param name="service">The service</param>
        /// <returns></returns>
        Task<string> GetNextBaseUrl(string service);

        /// <summary>
        /// Gets the API url based on the service and api location
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="api">The api</param>
        /// <returns></returns>
        Task<string> GetApiUrl(string service, string api);
    }
}