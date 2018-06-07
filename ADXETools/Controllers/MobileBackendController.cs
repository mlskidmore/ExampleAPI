using ADXETools.FalconRequests;
using ADXETools.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ADXETools.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/xml", "application/json")]
    [Consumes("application/xml", "application/json")]
    public class MobileBackEndController : Controller
    {
        readonly IFalconPort _falconPort;
        const string _aspPage = "MobileBackEndSVR.asp";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="falconPort"></param>
        public MobileBackEndController(IFalconPort falconPort)
        {
            _falconPort = falconPort;
        }

        /// <summary>
        /// Adds attachment from mobile device
        /// </summary>
        /// <param name="xmlInput"></param>
        /// <returns></returns>
        /// <response code = "201">Returns saved attachment info</response>
        /// <response code = "400">Invalid input parameters</response>
        /// <response code = "401">Active authorization is missing</response>
        /// <response code = "1000">Work_Assignment ID Missing</response>
        /// <response code = "2000">Role Code Missing</response>
        /// <response code = "3000">usiness Category Missing Missing</response>
        /// <response code = "4000">Invalid Ticket</response>
        //[SwaggerRequestExample(typeof(string),typeof(OONLookupVehicleExample))]
        [HttpPost("MobileBackEndAddAttachment")]
        [ProducesResponseType(typeof(MBESVRAddAttachmentRequest), 201)]
        public async Task<IActionResult> MobileBackEndAddAttachment([FromBody]MBESVRAddAttachmentRequest xmlInput)
        {
            try
            {
                if (xmlInput == null)
                {
                    string msg = string.Format("Invalid input data received <{0}>.  Verify input request data.", xmlInput);
                    return BadRequest(msg);
                }
                //var xmlOutput = xmlInput.ToXml("Request");
                var xmlOutput = await _falconPort.SubmitFalconRequest(_aspPage, xmlInput);
                return StatusCode(StatusCodes.Status201Created, xmlOutput);
            }
            catch (Exception ex)
            {
                string msg = string.Format("Processing error <{0}> received for PostAttachments request <{1}>.  Verify input request data.", ex.Message, xmlInput);
                return BadRequest(msg);
            }
        }
        /// <summary>
        /// Adds event from mobile device
        /// </summary>
        [HttpPost("MobileBackEndAddAttEvent")]
        [ProducesResponseType(typeof(MBESVRAddAttachmentRequest), 201)]
        public async Task<IActionResult> MobileBackEndAddAttEvent([FromBody]MBESVRAddAttEventRequest xmlInput)
        {
            try
            {
                if (xmlInput == null)
                {
                    string msg = string.Format("Invalid input data received <{0}>.  Verify input request data.", xmlInput);
                    return BadRequest(msg);
                }
                var xmlOutput = xmlInput.ToXml("Request");//await falconPort.SubmitFalconRequest(_aspPage, xmlInput);
                return StatusCode(StatusCodes.Status201Created, xmlOutput);
            }
            catch (Exception ex)
            {
                string msg = string.Format("Processing error <{0}> received for PostAttachments request <{1}>.  Verify input request data.", ex.Message, xmlInput);
                return BadRequest(msg);
            }
        }

        /// <summary>
        /// Updates a vehicle from a mobile device
        /// </summary>
        [HttpPost("MobileBackEndUpdateVehicle")]
        [ProducesResponseType(typeof(MBESVRAddAttachmentRequest), 201)]
        public async Task<IActionResult> MobileBackEndUpdateVehicle([FromBody]MBESVRUpdateVehicle xmlInput)
        {
            try
            {
                if (xmlInput == null)
                {
                    string msg = string.Format("Invalid input data received <{0}>.  Verify input request data.", xmlInput);
                    return BadRequest(msg);
                }
                var xmlOutput = xmlInput.ToXml("Request");
                xmlOutput = await _falconPort.SubmitFalconRequest(_aspPage, FalconRequest<MBESVRUpdateVehicle>.CreateRequest(xmlInput, this.GetMethodName()));
                return StatusCode(StatusCodes.Status201Created, xmlInput);
            }
            catch (Exception ex)
            {
                string msg = string.Format("Processing error <{0}> received for PostAttachments request <{1}>.  Verify input request data.", ex.Message, xmlInput);
                return BadRequest(msg);
            }
        }
    }
}