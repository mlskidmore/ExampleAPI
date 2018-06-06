using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ADXETools.FalconRequests;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Xml.Serialization;

namespace ADXETools.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot]
    public class CommentsRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [BindNever, XmlAttribute, JsonIgnore]
        public string Method { get; set; } = null;
        /// <summary>
        /// 
        /// </summary>
        [Required, XmlElement]
        public string WorkAssignmentId { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [Required, XmlElement]
        public string CreatedForProfileId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Required, XmlElement]
        public string CommentSubject { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Required, XmlElement]
        public string CommentText { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string Author { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public string DestinationEmails { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore, BindNever]
        public string ClientToken { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public CommentsRequest() { }
    }

    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    public class CommentsController : Controller
    {
        readonly IFalconPort falconPort;
        const string aspPage = "CommentsSvr.asp";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="falconPort"></param>
        public CommentsController(IFalconPort falconPort)
        {
            this.falconPort = falconPort;
        }

        bool Validate(CommentsRequest request)
        {
            return request != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("SaveComments")]
        [ProducesResponseType(typeof(CommentsRequest), 201)]
        public async Task<IActionResult> SaveComments([FromBody]CommentsRequest request)
        {
            try
            {
                request.Method = this.GetMethodName();
                if (!Validate(request))
                {
                    return BadRequest("Invalid input data received. Verify input request data.");
                }
                var xmlResponse = request.ToXml(); //await falconPort.SubmitFalconRequest(aspPage, request.ToXml());
                return StatusCode(StatusCodes.Status201Created, xmlResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Processing error [{ ex }] received for { this.GetMethodName() } request [{ request.ToXml() }].  Verify input request data.");
            }
        }
    }
}