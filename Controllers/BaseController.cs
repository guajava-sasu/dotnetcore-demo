using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ModustaAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly Versionneur _version;

        public BaseController(IOptions<Versionneur> version)
        {
            _version = version.Value;
        }

        //[HttpGet(Name = "Home")]
        //public string Home()
        //{
        //    var version = $"Version: {_version.Major}.{_version.Minor}.{_version.Patch}";       
        //    return version;
        //}

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
    }
}
