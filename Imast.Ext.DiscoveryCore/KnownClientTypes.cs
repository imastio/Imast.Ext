namespace Imast.Ext.DiscoveryCore
{
    /// <summary>
    /// The definitions of the known client types
    /// </summary>
    public static class KnownClientTypes
    {
        /// <summary>
        /// The static discovery client
        /// </summary>
        public const string STATIC = "static";

        /// <summary>
        /// The gateway-based discovery client
        /// </summary>
        public const string GATEWAY = "gateway";

        /// <summary>
        /// The kubernetes-based service discovery
        /// </summary>
        public const string KUBERNETES = "kubernetes";
    }
}