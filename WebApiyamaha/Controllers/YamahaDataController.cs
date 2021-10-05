using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApiyamaha.Models;
using WebApiyamaha.Services.SQL;

namespace WebApiyamaha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YamahaDataController : ControllerBase
    {
        // GET: api/YamahaData
        [HttpGet]
        public ActionResult Categories() 
        {
            var responce = SqlService.GetModelsInfo();
            return Ok(responce);
        }

        // GET: api/YamahaData/Diseplacement/10
        [HttpGet("{id}"), Route("Diseplacement/{id}")]
        public ActionResult Diseplacement(int id)
        {
            string idToString = Convert.ToString(id);
            var responce = SqlService.GetModelsInfo(idToString, nameof(Diseplacement));
            return Ok(responce);
        }

        // GET: api/YamahaData/ModelYears/1
        [HttpGet("{id}"), Route("ModelYears/{id}")]
        public ActionResult ModelYears(int id)
        {
            string idToString = Convert.ToString(id);
            var responce = SqlService.GetModelsInfo(idToString, nameof(ModelYears));
            return Ok(responce);
        }

        //GET: api/YamahaData/Model/1
        [HttpGet("{id}"), Route("Model/{id}")]
        public ActionResult Model(int id)
        {
            string idToString = Convert.ToString(id);
            var responce = SqlService.GetModelsInfo(idToString, nameof(Model));
            return Ok(responce);
        }

        //GET: api/YamahaData/ModelsList/1
        [HttpGet("id"), Route("ModelsList/{id}")]
        public ActionResult ModelsList(int id)
        {
            string idToString = Convert.ToString(id);
            var responce = SqlService.GetModelsList(idToString);
            return Ok(responce);
        }

        //GET: api/YamahaData/Catalog/1
        [HttpGet("id"), Route("Catalog/{id}")]
        public ActionResult Catalog(int id)
        {
            string idToString = Convert.ToString(id);
            var responce = SqlService.GetCatalog(idToString);
            return Ok(responce);
        }

        //GET: api/YamahaData/Part/1
        [HttpGet("id"), Route("Part/{id}")]
        public ActionResult Part(int id)
        {
            string idToString = Convert.ToString(id);
            var responce = SqlService.GetPart(idToString);
            return Ok(responce);
        }
    }
}
