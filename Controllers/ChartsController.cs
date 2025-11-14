using Microsoft.AspNetCore.Mvc;
using ModustaAPI.Models;
using ModustaAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ModustaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {        private readonly ChartService _booksService;
        private readonly IConfiguration _configuration;
        public ChartsController(IConfiguration configuration, ChartService booksService)
        {
            _booksService = booksService;
            _configuration = configuration;
        }

      /*  [HttpGet("test")]
        public async Task<ChartPieData> Test()
        {
            var dataset = await _booksService.GetDonutsChartAsync("64f0a9a1e4ed9e4b106239d7");
            //var tmp = _configuration.GetSection("Environnement").Value;
            //   return "Oui je fonctionne en sécurisé sur Hostinger 3 via Cv Controlleur!" + tmp;
            return dataset;
        }


        [HttpGet("framework")]
        public async Task<ChartPieData> PieChartFm()
        {
            var dataset = await _booksService.GetDonutsChartAsync("64f0a9a1e4ed9e4b106239d7");
            //var tmp = _configuration.GetSection("Environnement").Value;
            //   return "Oui je fonctionne en sécurisé sur Hostinger 3 via Cv Controlleur!" + tmp;
            return dataset;
        }*/

        // GET: api/<ChartsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ChartsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChartBarData>> Get(string id)
        {
            var book = await _booksService.GetChartAsync(id);

            if (book is null)
            {
                return NotFound();
            }
            return book;
        }




        // POST api/<ChartsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ChartsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ChartsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
