using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
namespace ClassLibrary
{
    public static class XMLExtension
    {
        // Writes the XML declaration and encoding to the document.
        public static void WriteStartDocument(this XmlWriter writer, string encoding = "UTF-8")
        {
            writer.WriteStartDocument();
            writer.WriteProcessingInstruction("xml", $"version=\"1.0\" encoding=\"{encoding}\"");
        }
        // Writes the start element tag for the root element of the document.
        public static void WriteStartRootElement(this XmlWriter writer)
        {
            writer.WriteStartElement("root");
        }
        // Writes the end element tag for the root element of the document.
        public static void WriteEndRootElement(this XmlWriter writer)
        {
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }
        // Writes the start element tag for an element within the document.
        public static void WriteStartElement(this XmlWriter writer)
        {
            writer.WriteStartElement("element");
        }
        // Writes the end element tag for an element within the document.
        public static void WriteEndElement(this XmlWriter writer)
        {
            writer.WriteEndElement();
        }
        // Writes an attribute for an element within the document.
        public static void WriteAttribute(this XmlWriter writer, string name, string value)
        {
            writer.WriteStartElement(name);
            writer.WriteString(value);
            writer.WriteEndElement();
        }

        
    }
}
