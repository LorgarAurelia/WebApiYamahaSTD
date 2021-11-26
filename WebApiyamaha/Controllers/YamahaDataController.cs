using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApiyamaha.Services.YamahaBll;

namespace WebApiyamaha.Controllers
{
    [Route("api/epc/yamaha/online")]
    [ApiController]
    [Consumes("application/json")]
    public class YamahaDataController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public YamahaDataController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/epc/yamaha/online
        [HttpGet]
        public async Task<ActionResult> LanguageAsync()
        {
            var responce = await YamahaService.GetParametr(_configuration);
            return Ok(responce);
        }

        // GET: /api/epc/yamaha/online/parameter?idx
        [HttpGet("parameter"), Route("online/parameter")]
        public async Task<ActionResult> ParameterAsync([FromQuery][Required] string idx)
        {
            if (string.IsNullOrEmpty(idx))
                return BadRequest(400);

            var responce = await YamahaService.GetParametr(_configuration, idx);
            return Ok(responce);
        }

        // GET: api/YamahaData/ModelYears/1
        [HttpGet("parts"), Route("online/parts")]
        public async Task<ActionResult> PartsAsync([FromQuery][Required] string idx)
        {
            if (string.IsNullOrEmpty(idx))
                return BadRequest(400);

            var responce = await YamahaService.GetParts(_configuration, idx);
            return Ok(responce);
        }

        [HttpGet("findpart"), Route("online/findpart")]
        public async Task<ActionResult> FindPartAsync([FromQuery][Required] string value, [FromQuery][Required] string searchType)
        {
            if (string.IsNullOrEmpty(value))
                return BadRequest(400);
            if (string.IsNullOrEmpty(searchType))
                return BadRequest(400);

            var responce = await YamahaService.SearchParts(_configuration, value, searchType);
            return Ok(responce);
        }


    }
}
