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
    static string GetRootName(Type type)
    {
        var rootAttributes = type.GetCustomAttributes(typeof(XmlRootAttribute), false);
        string name = ((XmlRootAttribute)rootAttributes?[0]).ElementName ?? type.Name;
        return name;
    }

    /// <summary>
    /// serializes 'this' instance as XML
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_this"></param>
    /// <param name="rootName"></param>
    /// <returns></returns>
    static public string ToXml<T>(this T _this, string rootName = null) where T : class
    {
        using (var stream = new StringWriter())
        {
            using (var xw = XmlWriter.Create(stream, new XmlWriterSettings { OmitXmlDeclaration = true }))
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                new XmlSerializer(_this.GetType(), new XmlRootAttribute(rootName ?? GetRootName(_this.GetType())) { Namespace = "" }).Serialize(xw, _this, ns);
            }
            string xml = stream.ToString();
            return xml;
        }
    }

    /// <summary>
    /// returns the name of the calling method
    /// </summary>
    /// <param name="_this"></param>
    /// <param name="memberName"></param>
    /// <returns></returns>
    static public string GetMethodName<T>(this T _this, [CallerMemberName]string memberName = "") where T : class
    {
        return memberName;
    }
}