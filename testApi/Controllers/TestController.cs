using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace testApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        public TestController()
        {
        }

        [HttpGet]
        [Route("public-data")]
        public IActionResult PublicData()
        {
            return Ok("public data");
        }

        [Authorize]
        [HttpGet]
        [Route("private-data")]
        public IActionResult PrivateData()
        {
            return Ok("private data");
        }
    }
}
