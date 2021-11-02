using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Imast.Ext.DiscoveryCore;

namespace Imast.Ext.ApiClient
{
    /// <summary>
    /// The API client base
    /// </summary>
    public class ApiClientBase : IDisposable
    {
        /// <summary>
        /// The authorization header
        /// </summary>
        protected static readonly string AUTH_HEADER = "Authorization";

        /// <summary>
        /// The Content-Type header
        /// </summary>
        protected static readonly string CONTENT_TYPE_HEADER = "Content-Type";

        /// <summary>
        /// The client header
        /// </summary>
        protected static readonly string CLIENT_HEADER = "X-Imast-Client";

        /// <summary>
        /// The JSON media type 
        /// </summary>
        protected static readonly string JSON_MEDIA = "application/json";

        /// <summary>
        /// The client name
        /// </summary>
        protected readonly string client;

        /// <summary>
        /// The target service
        /// </summary>
        protected readonly string service;

        /// <summary>
        /// The discovery client
        /// </summary>
        protected readonly IDiscoveryClient discovery;

        /// <summary>
        /// The base web client
        /// </summary>
        protected readonly HttpClient webClient;

        /// <summary>
        /// Creates new instance of API client
        /// </summary>
        /// <param name="client">The client</param>
        /// <param name="service">The service</param>
        /// <param name="discovery">The discovery</param>
        protected ApiClientBase(string client, string service, IDiscoveryClient discovery)
        {
            this.client = client;
            this.service = service;
            this.discovery = discovery;
            this.webClient = new HttpClient
            {
                DefaultRequestHeaders = { {CLIENT_HEADER, this.client }}
            };
        }

        /// <summary>
        /// Gets the API url to call
        /// </summary>
        /// <param name="api">The api</param>
        /// <returns></returns>
        public virtual async Task<string> GetApiUrl(string api)
        {
            return await this.discovery.GetApiUrl(this.service, api);
        }

        /// <summary>
        /// Creates the authorization bearer header
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns></returns>
        protected static HeaderSpec Auth(string token)
        {
            return new HeaderSpec { Header = AUTH_HEADER, Value = $"Bearer {token}" };
        }

        /// <summary>
        /// Creates the content-type header
        /// </summary>
        /// <param name="type">The content type</param>
        /// <returns></returns>
        protected static HeaderSpec ContentType(string type)
        {
            return new HeaderSpec { Header = CONTENT_TYPE_HEADER, Value = type };
        }

        /// <summary>
        /// Creates the authorization basic header
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        protected static HeaderSpec BasicAuth(string username, string password)
        {
            // get username in base64
            var base64Username = Convert.ToBase64String(Encoding.UTF8.GetBytes(username));

            // get password in base64
            var base64Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

            // create basic auth header
            return new HeaderSpec { Header = AUTH_HEADER, Value = $"Basic {base64Username}:{base64Password}" };
        }

        /// <summary>
        /// Get input body as json content
        /// </summary>
        /// <param name="input">The input to serialize</param>
        /// <param name="headers">The extra headers</param>
        /// <returns></returns>
        protected static HttpContent AsJson(object input, params HeaderSpec[] headers)
        {
            // build the content
            var content = JsonContent.Create(input, null, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            // apply header
            foreach (var header in headers)
            {
                content.Headers.Add(header.Header, new [] { header.Value });
            }
            
            return content;
        }

        /// <summary>
        /// Dispose once done with the client
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose safely
        /// </summary>
        /// <param name="disposing">The disposing indicator</param>
        protected virtual void Dispose(bool disposing)
        {
            // not disposing
            if (!disposing)
            {
                return;
            }

            // dispose if any
            this.webClient?.Dispose();
        }
    }
}