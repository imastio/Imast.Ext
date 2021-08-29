using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imast.Ext.DiscoveryCore
{
    /// <summary>
    /// The static discovery client implementation
    /// </summary>
    public class StaticDiscoveryClient : IDiscoveryClient
    {
        /// <summary>
        /// The protocol to use in discovery
        /// </summary>
        protected readonly string protocol;

        /// <summary>
        /// The static environment host if given
        /// </summary>
        protected readonly string host;

        /// <summary>
        /// The fallback port to use
        /// </summary>
        protected readonly int? fallbackPort;

        /// <summary>
        /// The map of ports in the discovery
        /// </summary>
        protected readonly IDictionary<string, int> ports;

        /// <summary>
        /// The type of client
        /// </summary>
        public virtual string Type => KnownClientTypes.STATIC;

        /// <summary>
        /// Creates new static discovery client
        /// </summary>
        /// <param name="protocol">The protocol http/https</param>
        /// <param name="host">The static host</param>
        /// <param name="fallbackPort">The fallback port</param>
        /// <param name="ports">The set of ports</param>
        public StaticDiscoveryClient(string protocol, string host, int? fallbackPort, IDictionary<string, int> ports)
        {
            this.protocol = protocol;
            this.host = host;
            this.fallbackPort = fallbackPort;
            this.ports = ports ?? new Dictionary<string, int>();
        }

        /// <summary>
        /// Gets the instance for the given service
        /// </summary>
        /// <param name="service">The service</param>
        /// <returns></returns>
        public virtual Task<DiscoveryInstance> GetInstance(string service)
        {
            // try get port
            var prt = this.ports.TryGetValue(service, out var p) ? p : this.fallbackPort.GetValueOrDefault(0);

            // try get host
            var hst = string.IsNullOrWhiteSpace(this.host) ? service : this.host;

            return Task.FromResult(new DiscoveryInstance
            {
                Host = hst,
                Port = prt
            });
        }

        /// <summary>
        /// Gets the next base service
        /// </summary>
        /// <param name="service">The service</param>
        /// <returns></returns>
        public virtual async Task<string> GetNextBaseUrl(string service)
        {
            // get instance
            var instance = await this.GetInstance(service);

            // build the url
            return $"{this.protocol}://{instance.Host}:{instance.Port}/";
        }

        /// <summary>
        /// Gets the API url based on the service and api location
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="api">The api</param>
        /// <returns></returns>
        public virtual async Task<string> GetApiUrl(string service, string api)
        {
            // the next base url
            var baseUrl = await this.GetNextBaseUrl(service);

            // build and return the url
            return baseUrl == null ? null : $"{baseUrl}{api}";
        }
    }
}