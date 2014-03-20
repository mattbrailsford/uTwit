using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Our.Umbraco.uTwit.Extensions
{
    internal static class ObjectExtensions
    {
        internal static XPathNavigator ToXml<T>(this T obj)
        {
            return obj.ToXml(new Dictionary<string, string>());
        }

        internal static XPathNavigator ToXml<T>(this T obj,
            IDictionary<string, string> nodeNameOverrides)
        {
            var dcs = new DataContractSerializer(typeof(T));
            var sb = new StringBuilder();
            var writer = XmlWriter.Create(sb);
            dcs.WriteObject(writer, obj);
            writer.Close();

            var xml = XElement.Parse(sb.ToString());
            xml = xml.CleanupNodes(nodeNameOverrides);

            return xml.CreateNavigator();
        }

        private static XElement CleanupNodes(this XElement xmlDocument,
            IDictionary<string, string> nodeNameOverrides)
        {
            var nodeName = xmlDocument.Name.LocalName.ToLower();

            if (nodeNameOverrides.ContainsKey(nodeName))
                nodeName = nodeNameOverrides[nodeName];

            if (!xmlDocument.HasElements)
            {
                return new XElement(nodeName)
                {
                    Value = xmlDocument.Value 
                };
            }
            return new XElement(nodeName,
                xmlDocument.Elements().Select(x => CleanupNodes(x, nodeNameOverrides)));
        }
    }
}