using Microsoft.AspNetCore.Mvc;
using System;
using WebApiyamaha.Services.SQL;

namespace WebApiyamaha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YamahaDataController : ControllerBase
    {
        // GET: api/YamahaData
        [HttpGet]
        public ActionResult<string> Categories() //ActionResult<IEnumerable<ModelsInfo>>
        {
            string responce = SqlService.GetFromDataBase(nameof(Categories));
            return Ok(responce);
        }

        // GET: api/YamahaData/Diseplacement/10
        [HttpGet("{id}"), Route("Diseplacement/{id}")]
        public ActionResult<string> Diseplacement(int id)
        {
            string idToString = Convert.ToString(id);
            string responce = SqlService.GetFromDataBase(nameof(Diseplacement), idToString);
            return Ok(responce);
        }

        // GET: api/YamahaData/ModelYears/1
        [HttpGet("{id}"), Route("ModelYears/{id}")]
        public ActionResult<string> ModelYears(int id)
        {
            string idToString = Convert.ToString(id);
            string responce = SqlService.GetFromDataBase(nameof(ModelYears), idToString);
            return Ok(responce);
        }

        //GET: api/YamahaData/ModelYears/1
        [HttpGet("{id}"), Route("Model/{id}")]
        public ActionResult<string> Model(int id)
        {
            string idToString = Convert.ToString(id);
            string responce = SqlService.GetFromDataBase(nameof(Model), idToString);
            return Ok(responce);
        }

        //GET: api/YamahaData/ModelsList/1
        [HttpGet("id"), Route("ModelsList/{id}")]
        public ActionResult<string> ModelsList(int id)
        {
            string idToString = Convert.ToString(id);
            string responce = SqlService.GetFromDataBase(nameof(ModelsList), idToString);
            return Ok(responce);
        }

        //GET: api/YamahaData/Catalog/1
        [HttpGet("id"), Route("Catalog/{id}")]
        public ActionResult<string> Catalog(int id)
        {
            string idToString = Convert.ToString(id);
            string responce = SqlService.GetFromDataBase(nameof(Catalog), idToString);
            return Ok(responce);
        }

        //GET: api/YamahaData/Part/1
        [HttpGet("id"), Route("Part/{id}")]
        public ActionResult<string> Part(int id)
        {
            string idToString = Convert.ToString(id);
            string responce = SqlService.GetFromDataBase(nameof(Part), idToString);
            return Ok(responce);
        }
    }
}
