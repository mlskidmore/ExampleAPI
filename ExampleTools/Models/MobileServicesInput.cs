using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Xml.Serialization;
using ADXETools.FalconRequests;

namespace ADXETools.Models
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("Request")]
    public class OONVehicleLookupInput : ServiceInput
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public Header Header { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required, XmlElement]
        public string CreatedForProfileId { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [Required, XmlElement]
        public string VehId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string AudaVINUsed { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string AudaVINGUID { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string EstID { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string HQ_Engine { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string HQ_Transmission { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public OONVehicleLookupInput() {
            Header = new Header("OONShopLookupVehicle");
        }
    }
}