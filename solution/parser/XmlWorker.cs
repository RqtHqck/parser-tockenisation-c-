using System;
using System.IO;
using System.Xml.Serialization;

public static class XmlWorker
{
    // Сериализация объекта в XML
    public static void SerializeToXml<T>(T obj, string filePath)
    {
        var serializer = new XmlSerializer(typeof(T));
        using (var writer = new StreamWriter(filePath))
        {
            serializer.Serialize(writer, obj);
        }
    }
}