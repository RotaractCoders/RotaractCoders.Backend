using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Distrito")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class DistritoController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new List<string> { "4430" });
        }
    }
}