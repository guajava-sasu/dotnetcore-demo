using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ModustaAPI.Models;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;
using Newtonsoft.Json;

namespace ModustaAPI.Controllers
{
    [ApiController]
    // [Route("api/[controller]")]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet("test")]
        public string Test()
        {
            return "Oui je fonctionne en sécurisé sur Hostinger 3";
        }

        private readonly Versionneur _version;

        public TestController(IOptions<Versionneur> version)
        {
            _version = version.Value;
        }

        [HttpGet("version")]
        public ActionResult<string> GetVersion()
        {
            return Ok(new
            {
                Major = _version.Major,
                Minor = _version.Minor,
                Patch = _version.Patch,
            });
        }
        //[HttpGet("readjson")]
        //public async Task<IActionResult> ReadJsonFile()
        //{
        //    // Chemin du fichier JSON
        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data\\cvseul.json");

        //    // Lire le fichier JSON
        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        return NotFound("Le fichier JSON n'existe pas.");
        //    }

        //    var jsonString = await System.IO.File.ReadAllTextAsync(filePath);

        //    // Désérialiser le contenu JSON
        //    var jsonData = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);

        //    // Renvoyer le résultat sous forme de JSON
        //    return Ok(jsonData);
        //}
        [HttpGet("getcv")]
        public async Task<IActionResult> GetById()
        {
            // Chemin du fichier JSON
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data\\cvseul.json");

            // Lire le fichier JSON
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Le fichier JSON n'existe pas.");
            }

            var jsonString = await System.IO.File.ReadAllTextAsync(filePath);
            if (jsonString == null) { return null; }

            var jsonData = JsonConvert.DeserializeObject<Curriculum>(jsonString);
            // Désérialiser le contenu JSON
           //  jsonData = JsonSerializer.Deserialize<Cv>();
            var result = jsonData;
            // Renvoyer le résultat sous forme de JSON
            return Ok(result);
        }


    }
}
