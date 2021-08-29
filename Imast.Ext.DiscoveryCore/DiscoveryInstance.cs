namespace Imast.Ext.DiscoveryCore
{
    /// <summary>
    /// The single discovery instance
    /// </summary>
    public class DiscoveryInstance
    {
        /// <summary>
        /// The resolved host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// The resolved port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// The base address
        /// </summary>
        public string Base { get; set; }
    }
}
