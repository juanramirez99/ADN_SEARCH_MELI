using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace LEVEL2_ADN_PROCESSING_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ADNProcessingController : ControllerBase
    {
        private readonly ILogger<ADNProcessingController> _logger;

        public ADNProcessingController(ILogger<ADNProcessingController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "Mutant")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> Post()
        {
            short i = 1;
            if (i>0)
            {
                return true;
            }
            else
                return NotFound(false);
        }

        [HttpGet(Name = "Stats")]
        public Stats Get()
        {
            Stats stats = new Stats()
            {
                CountHumanDNA = 40,
                CountMutantDNA = 60,
                Ratio = 6F
            };
            return stats;
        }
    }
}
