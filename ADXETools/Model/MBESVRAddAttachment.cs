using ADXETools.FalconRequests;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ADXETools.Model
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot]
    public class MBESVRAddAttachmentRequest : ServiceInput
    {
        /// <summary>
        /// 
        /// </summary>
        public MBESVRAddAttachmentRequest() { }

        /// <summary>
        /// 
        /// </summary>
        [BindNever, XmlAttribute, JsonIgnore]
        public string Method { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        [Required, XmlElement]
        public string roleCode { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Required, XmlElement]
        public string businessCategory { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Required, XmlElement]
        public string WorkAssignmentID { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [Required, XmlArray]
        public List<Attachment> Attachments { get; set; } = null;
    }
}
