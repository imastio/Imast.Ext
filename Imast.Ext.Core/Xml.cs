using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Imast.Ext.Core
{
    /// <summary>
    /// Set of extension methods for XML documents
    /// </summary>
    public static class Xml
    {
        /// <summary>
        /// Get attribute values in dictionary for the XML Element
        /// </summary>
        /// <param name="element">The target element</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetAttributeValues(this XElement element)
        {
            return element.Attributes().ToDictionary(attr => attr.Name.LocalName.ToUpper(), attr => attr.Value);
        }

        /// <summary>
        /// Converts xml document to x document 
        /// </summary>
        /// <param name="xmlDocument">Xml document</param>
        /// <returns></returns>
        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using var nodeReader = new XmlNodeReader(xmlDocument);
            nodeReader.MoveToContent();
            return XDocument.Load(nodeReader);
        }

        /// <summary>
        /// Gets scalar attribute values in dictionary for the XML Element
        /// </summary>
        /// <param name="element">The target element</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetScalarAttributeValues(this XElement element)
        {
            var attrs = new Dictionary<string, string>();

            element.Elements().Each(childElement =>
            {
                if (childElement.HasElements || childElement.HasAttributes || childElement.IsEmpty)
                {
                    return;
                }

                attrs.Put(childElement.Name.LocalName.ToUpper(), childElement.Value);
            });

            return attrs;
        }
    }
}