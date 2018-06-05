using System;
using System.Threading.Tasks;
using ADXETools.FalconRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADXETools.Controllers
{
    [Produces("application/xml")]
    public class MobileServicesController : Controller
    {
        //TO BE ADDED - Deserialize the XML Output so it displays correctly.
        //TO BE ADDED - Separate Controller for every service, use Falcon Base URL and added the appropriate *.asp file to it when making Falcon calls.
        private readonly IFalconPort falconPort;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimsAdapter"></param>
        /// <param name="logger"></param>
        public MobileServicesController(IFalconPort falconPort)
        {
            this.falconPort = falconPort;
        }

        /// <summary>
        /// Saves the attachment to the ADXE claim
        /// </summary>
        /// <param name="serviceInput"></param>
        /// <returns></returns>
        /// <response code = "201">Returns saved attachment info</response>
        /// <response code = "400">Invalid input parameters</response>
        /// <response code = "401">Active authorization is missing</response>
        [HttpPost("MobileVINDecode")]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> PostAsync([FromBody]string xmlInput)
        {
            try
            {
                if (xmlInput == null)
                {
                    string msg = string.Format("Invalid input data received <{0}>.  Verify input request data.", xmlInput);
                    return BadRequest(msg);
                }
                var xmlOutput = await falconPort.SubmitFalconRequest(xmlInput);
                return StatusCode(StatusCodes.Status201Created, xmlOutput);
            }
            catch (Exception ex)
            {
                string msg = string.Format("Processing error <{0}> received for PostAttachments request <{1}>.  Verify input request data.", ex.Message, xmlInput);
                return BadRequest(msg);
            }

            
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        //// GET api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
    }
}
