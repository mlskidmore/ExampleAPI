using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ADXETools.Model
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot]
    public class Attachment
    {
        /// <summary>
        /// 
        /// </summary>
        public Attachment() { }
        
        /// <summary>
        /// 
        /// </summary>
        [Required, XmlElement]
        public string Extension { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Required, XmlElement]
        public int FileType { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string Notation { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Required, XmlElement]
        public long FileSize { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string CreatedBy { get; set; } = string.Empty;
        
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public int SupportsEstId { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string AttachmentGUID { get; set; } = string.Empty;
        
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string Id { get; set; } = string.Empty;
        
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string AttachmentDatetime { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Required, XmlElement]
        public string ImageData { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Required, XmlElement]
        public string Action { get; set; } = string.Empty;        
    }
}
