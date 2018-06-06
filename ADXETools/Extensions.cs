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
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_this"></param>
    /// <returns></returns>
    static public string ToXml<T>(this T _this) where T : class
    {
        string xml;
        using (var stream = new StringWriter())
        {
            var opts = new XmlWriterSettings { OmitXmlDeclaration = true };
            using (var xw = XmlWriter.Create(stream, opts))
            {
                new XmlSerializer(_this.GetType()).Serialize(xw, _this);
            }
            xml = stream.ToString();
        }
        var doc = XDocument.Parse(xml);
        doc.Root.RemoveAttributes();
        var xml2 = doc.ToString();

        return xml;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_this"></param>
    /// <param name="memberName"></param>
    /// <returns></returns>
    static public string GetMethodName<T>(this T _this, [CallerMemberName]string memberName = "") where T : class
    {
        return memberName;
    }
}