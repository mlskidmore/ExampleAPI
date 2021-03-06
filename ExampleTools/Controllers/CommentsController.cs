﻿using ADXETools.FalconRequests;
using ADXETools.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ADXETools.Controllers.Claim
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/xml", "application/json")]
    [Consumes("application/xml", "application/json")]
    public class CommentsController : Controller
    {
        readonly IFalconPort _falconPort;
        const string _aspPage = "CommentsSvr.asp";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="falconPort"></param>
        public CommentsController(IFalconPort falconPort)
        {
            _falconPort = falconPort;
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
                if (!Validate(request))
                {
                    return BadRequest("Invalid input data received. Verify input request data.");
                }
                request.Method = this.GetMethodName();
                var xmlResponse = await _falconPort.SubmitFalconRequest(_aspPage, request);
                return StatusCode(StatusCodes.Status201Created, xmlResponse);
            }
            catch (HttpStatusException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Processing error [{ ex }] received for { this.GetMethodName() } request [{ request.ToXml("Request") }].  Verify input request data.");
            }
        }
    }
}