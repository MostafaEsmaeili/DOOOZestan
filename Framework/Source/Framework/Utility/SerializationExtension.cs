using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Framework.Utility
{
    public static class SerializationExtension
    {
        public static byte[] Serialize(this object obj)
        {
            var memoryStream = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, obj);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var b = new byte[memoryStream.Length];
            memoryStream.Read(b, 0, (int)memoryStream.Length);
            return b;
        }

        public static object Deserialize(this byte[] bytes)
        {
            var memoryStream = new MemoryStream(bytes);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var binaryFormatter = new BinaryFormatter();
            return binaryFormatter.Deserialize(memoryStream);
        }

        public static T Deserialize<T>(this byte[] bytes)
        {
            return (T)Deserialize(bytes);
        }

        public static T DeepClone<T>(this object obj)
        {
            return (obj.Serialize()).Deserialize<T>();
        }

        public static T XmlDeserialize<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return default(T);
            }

            var serializer = new XmlSerializer(typeof(T));
            var settings = new XmlReaderSettings();

            using (var textReader = new StringReader(xml))
            {
                using (var xmlReader = XmlReader.Create(textReader, settings))
                {
                    return (T)serializer.Deserialize(xmlReader);
                }
            }
        }

        public static string XmlSerialize(object obj, Type type)
        {
            if (obj == null)
                return null;

            var serializer = new XmlSerializer(type);

            var settings = new XmlWriterSettings
                               {
                                   Encoding = new UnicodeEncoding(false, false),
                                   Indent = false,
                                   OmitXmlDeclaration = false
                               };
            // no BOM in a .NET string

            using (var textWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.Serialize(xmlWriter, obj);
                }
                return textWriter.ToString();
            }
        }
    }
}
