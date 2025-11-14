using Microsoft.AspNetCore.Mvc;
using ModustaAPI.Models;
using ModustaAPI.Services;

namespace ModustaAPI.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class UtilisateurController : ControllerBase
    {
        private readonly UtilisateurService _mongoService;

        public UtilisateurController(UtilisateurService booksService) =>
            _mongoService = booksService;

        [HttpGet]
        public async Task<List<Utilisateur>> Get() =>
            await _mongoService.GetAsync();

        //[HttpGet("allid")]
        //public async Task<List<string>> GetAllId() =>
        //await _mongoService.GetAllIdAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Utilisateur>> Get(string id)
        {
            var book = await _mongoService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpGet("test")]
        public string Test()
        {
            return "Oui je fonctionne en sécurisé sur Hostinger 3 via Cv Controlleur!";
        }

        [HttpPost]
        public async Task<IActionResult> Post(Utilisateur newBook)
        {
            var book = await _mongoService.GetAsync(newBook.Sub);

            if (book is null)
            {
                newBook.CreatedAt = DateTime.Now;
                newBook.UpdatedAt = DateTime.Now;
                await _mongoService.CreateAsync(newBook);
                return CreatedAtAction(nameof(Get), new { id = newBook.Sub }, newBook);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id:length(125)}")]
        public async Task<IActionResult> Update(string id, Utilisateur updatedBook)
        {
            updatedBook.UpdatedAt = DateTime.Now;
            var book = await _mongoService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            updatedBook.Sub = book.Sub;

            await _mongoService.UpdateAsync(id, updatedBook);

            return NoContent();
        }

        //[HttpDelete("{id:length(125)}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var book = await _mongoService.GetAsync(id);

        //    if (book is null)
        //    {
        //        return NotFound();
        //    }

        //    await _mongoService.RemoveAsync(id);

        //    return NoContent();
        //}
    }
}