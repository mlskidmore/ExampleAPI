using ADXETools.FalconRequests;
using ADXETools.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ADXETools.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/xml", "application/json")]
    [Consumes("application/xml", "application/json")]
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

        bool Validate(CommentRequest request)
        {
            return request != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("SaveComments")]
        [ProducesResponseType(typeof(CommentRequest), 201)]
        public async Task<IActionResult> SaveComments([FromBody] CommentRequest request)
        {
            try
            {
                request.Method = this.GetMethodName();
                if (!Validate(request))
                {
                    return BadRequest("Invalid input data received. Verify input request data.");
                }
                var xmlResponse = request.ToXml("Request"); //await falconPort.SubmitFalconRequest(aspPage, request.ToXml());
                return StatusCode(StatusCodes.Status201Created, request);
            }
            catch (Exception ex)
            {
                return BadRequest($"Processing error [{ ex }] received for { this.GetMethodName() } request [{ request.ToXml("Request") }].  Verify input request data.");
            }
        }
    }
}