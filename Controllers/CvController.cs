using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ModustaAPI.Models;
using ModustaAPI.Services;
using Serilog;
using Stripe;
using System;

namespace ModustaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CvController : BaseController
    {
        private readonly Versionneur _version;
        private readonly ILogger<CvController> _logger;
        private readonly CurriculumService _booksService;
        private readonly IConfiguration _configuration;
        private readonly ChartService _chartService;
        public CvController(IOptions<Versionneur> version,IConfiguration configuration, CurriculumService booksService, ChartService chartService, ILogger<CvController> logger):base(version)
        {
            _booksService = booksService;
            _configuration = configuration;
            _chartService = chartService;
            _logger = logger; _version = version.Value;
        }

        [HttpGet]
        public async Task<List<Curriculum>> Get() =>
            await _booksService.GetAsync();

        [HttpGet("allid")]
        public async Task<List<IdentificationCv>> GetAllId()
        {
            try
            {
                _logger.LogInformation("Récupération de tous les CVs");
                _logger.LogError( "Erreur de test ");
                var result = await _booksService.GetAllIdAsync();
                return result;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred in DoSomething");
            }
            return null;
        }


        [HttpGet("allidbyuser/{utilisateur}")]
        public async Task<List<IdentificationCv>> GetAllIdByUser(string utilisateur)
        {
            _logger.LogInformation("---------------ConnectionString utilisée: " + Environment.GetEnvironmentVariable("DOTNET_MONGODB_CONNECTION_STRING"));
            return await _booksService.GetAllIdAsync(utilisateur);
        }

        [HttpGet("allidcv")]
        public async Task<List<IdentificationCv>> GetAllIdCv() => await _booksService.GetAllIdAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<CvWithData>> Get(string id)
        {
            var cv = await _booksService.GetAsync(id);
            if (cv is null) { return NotFound(); }

            var barChartData = _chartService.GetPieChartDataForTest(cv);
            var pieChartDataFrForFramework = _chartService.GetPieChartDataForFrameworkAsync(cv);
            var pieChartDataForLang = _chartService.GetPieChartDataForLanguagesAsync(cv);
            var pieChartDataForBdd = _chartService.GetPieChartDataForBddsAsync(cv);
            var pieChartDataForIde = _chartService.GetPieChartDataForIdeAsync(cv);
            var pieChartDataForLog = _chartService.GetPieChartDataForLogicielsAsync(cv);
            var pieChartDataForAutre = _chartService.GetPieChartDataForAutreAsync(cv);

            var tartes = new List<ChartPieData>
            {
                pieChartDataFrForFramework,
                pieChartDataForIde,
                pieChartDataForLang,
                pieChartDataForBdd,
                pieChartDataForLog,
                pieChartDataForAutre
            };
            var result = new CvWithData { Cv = cv, ChartBarData = barChartData, Pies = tartes };

            return result;
        }

        [HttpGet("byuser/{id}")]
        public async Task<ActionResult<Curriculum>> GetByUser(string id)
        {
            var book = await _booksService.GetByUserAsync(id);

            if (book is null) { return NotFound(); }

            return book;
        }

        [HttpGet("allcv")]
        public async Task<ActionResult<List<Curriculum>>> GetAllCv()
        {
            var book = await _booksService.GetAsync();

            if (book is null)
            {
                return NotFound();
            }

            return book;
        }
        [HttpGet("test")]
        public string Test()
        {
            //var tmp = _configuration.GetSection("Stripe:PublishableKey").Value;
            //var tmp1 = _configuration.GetSection("Stripe").Value;
            //var tmp2 = _configuration.GetSection("Stripe:SecretKey").Value;
            return "Oui je fonctionne en sécurisé sur Hostinger 3 via Cv Controlleur!   ";
        }

        [HttpGet("environnement")]
        public string Environnement()
        {
            return _configuration.GetSection("Environnement").Value;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Curriculum newBook)
        {
            await _booksService.CreateAsync(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Curriculum updatedBook)
        {
            var book = await _booksService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            updatedBook.Id = book.Id;

            await _booksService.UpdateAsync(id, updatedBook);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _booksService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _booksService.RemoveAsync(id);

            return NoContent();
        }
    }
}