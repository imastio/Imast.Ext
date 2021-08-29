using System;
using System.Threading.Tasks;

namespace Imast.Ext.DiscoveryCore
{
    /// <summary>
    /// The gateway-based discovery client
    /// </summary>
    public class GatewayDiscoveryClient : IDiscoveryClient
    {
        /// <summary>
        /// The protocol
        /// </summary>
        protected readonly string proto;

        /// <summary>
        /// The host name
        /// </summary>
        protected readonly string hostname;

        /// <summary>
        /// The port 
        /// </summary>
        protected readonly int port;

        /// <summary>
        /// The gateway base
        /// </summary>
        protected readonly string gatewayBase;

        /// <summary>
        /// The type of client
        /// </summary>
        public virtual string Type => KnownClientTypes.GATEWAY;

        /// <summary>
        /// Creates new discovery client based on gateway
        /// </summary>
        /// <param name="gateway">The gateway</param>
        public GatewayDiscoveryClient(string gateway)
        {
            // create new url
            var url = new UriBuilder(gateway);

            // assign params
            var givenHost = url.Host;
            var givenPort = url.Port;
            var givenProtocol = url.Scheme;
            var file = $"{url.Path}{url.Query}";

            // remove first slash
            if (file.StartsWith("/"))
            {
                file = file.Substring(1);
            }
            
            // add last slash to file
            if (!string.IsNullOrWhiteSpace(file) && !file.EndsWith("/"))
            {
                file = $"{file}/";
            }

            // keep url details
            this.proto = givenProtocol;
            this.hostname = givenHost;
            this.port = givenPort;
            this.gatewayBase = file;
        }

        /// <summary>
        /// Gets the instance for the given service
        /// </summary>
        /// <param name="service">The service</param>
        /// <returns></returns>
        public virtual Task<DiscoveryInstance> GetInstance(string service)
        {
            return Task.FromResult(new DiscoveryInstance
            {
                Host = this.hostname,
                Port = this.port,
                Base = this.gatewayBase
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
            return $"{this.proto}://{instance.Host}:{instance.Port}/{instance.Base}";
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
            return baseUrl == null ? null : $"{baseUrl}{service}/{api}";
        }
    }
}