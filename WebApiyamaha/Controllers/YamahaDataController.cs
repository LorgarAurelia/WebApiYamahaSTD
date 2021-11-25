using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
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
        public ActionResult Language()
        {
            var responce = YamahaService.GetParametr(_configuration);
            return Ok(responce);
        }

        // GET: /api/epc/yamaha/online/parameter?idx
        [HttpGet("parameter"), Route("online/parameter")]
        public ActionResult Parameter([FromQuery][Required] string idx)
        {
            if (string.IsNullOrEmpty(idx))
                return BadRequest(404);

            var responce = YamahaService.GetParametr(_configuration, idx);
            return Ok(responce);
        }

        // GET: api/YamahaData/ModelYears/1
        [HttpGet("parts"), Route("online/parts")]
        public ActionResult Parts([FromQuery][Required] string idx)
        {
            if (string.IsNullOrEmpty(idx))
                return BadRequest(404);

            var responce = YamahaService.GetParts(_configuration, idx);
            return Ok(responce);
        }

        ////GET: api/YamahaData/Model/1
        //[HttpGet("{id}"), Route("Model/{id}")]
        //public ActionResult Model(int id)
        //{
        //    string idToString = Convert.ToString(id);
        //    var responce = SqlService.GetModel(idToString);
        //    return Ok(responce);
        //}


    }
}
