using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

/// <summary>
/// 
/// </summary>
static public class Extensions
{
    /// <summary>
    /// Gets the XmlRootAttribute.ElementName or the type's Name if no XmlRootAttribute.ElementName
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    static public string GetRootName(this Type type)
    {
        var rootAttributes = type?.GetCustomAttributes(typeof(XmlRootAttribute), false);
        if (rootAttributes?.Length <= 0)
            return type.Name;
        string name = ((XmlRootAttribute)rootAttributes?[0]).ElementName ?? type.Name;
        return name;
    }

    /// <summary>
    /// serializes 'this' instance as XML
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="thisObj"></param>
    /// <param name="xmlRootName"></param>
    /// <returns>returns a string containing the XML representation of the object</returns>
    static public string ToXml<T>(this T thisObj, string xmlRootName = null) where T : class
    {
        if (thisObj == null)
            return null;
    
        using (var stream = new StringWriter())
        {
            using (var xw = XmlWriter.Create(stream, new XmlWriterSettings { OmitXmlDeclaration = true }))
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add("", ""); // tell the serializer we don't want the default xmlns= or xsi= attributes
                new XmlSerializer(thisObj.GetType(), new XmlRootAttribute(xmlRootName ?? thisObj.GetType().GetRootName()) { Namespace = "" }).Serialize(xw, thisObj, ns);
            }
            string xml = stream.ToString();
            return xml;
        }
    }

    /// <summary>
    /// serializes 'this' instance as JSON
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="thisObj"></param>
    /// <returns>returns a string containing the JSON representation of the object</returns>
    static public string ToJson<T>(this T thisObj) where T : class
    {
        if (thisObj == null)
            return null;

        string json = JsonConvert.SerializeObject(thisObj);
        return json;
    }

    /// <summary>
    /// returns the name of the calling method
    /// </summary>
    /// <param name="thisObj"></param>
    /// <param name="memberName"></param>
    /// <returns></returns>
    static public string GetMethodName<T>(this T thisObj, [CallerMemberName]string memberName = "") where T : class
    {
        return memberName;
    }
}