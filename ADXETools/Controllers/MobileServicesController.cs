using System;
using System.Threading.Tasks;
using ADXETools.FalconRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ADXETools.Models;

namespace ADXETools.Controllers.Mobile
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/xml", "application/json")]
    [Consumes("application/xml", "application/json")]

    public class MobileServicesController : Controller
    {
        readonly IFalconPort _falconPort;
        const string _aspPage = "Mobile.asp";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="falconPort"></param>
        public MobileServicesController(IFalconPort falconPort)
        {
            _falconPort = falconPort;
        }

        bool Validate(OONVehicleLookupInput request)
        {
            return request != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("OONVehicleLookup")]
        [ProducesResponseType(typeof(OONVehicleLookupInput), 201)]
        public async Task<IActionResult> OONVehicleLookup([FromBody] OONVehicleLookupInput request)
        {
            try
            {
                //string incomingText = this.Request.ToString.ReadAsStringAsync().Result;
                //XElement incomingXml = XElement.Parse(incomingText);

                if (!Validate(request))
                {
                    return BadRequest("Invalid input data received. Verify input request data.");
                }
                var xmlOutput = await _falconPort.SubmitFalconRequest(_aspPage, request);
                return StatusCode(StatusCodes.Status201Created, xmlOutput);
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
