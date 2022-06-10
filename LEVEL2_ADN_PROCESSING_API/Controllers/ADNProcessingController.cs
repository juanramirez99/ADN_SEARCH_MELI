using LEVEL2_ADN_PROCESSING_API.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace LEVEL2_ADN_PROCESSING_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ADNProcessingController : ControllerBase
    {
        private readonly ILogger<ADNProcessingController> _logger;
        private readonly DNAProcessingService _processingService;

        public ADNProcessingController(ILogger<ADNProcessingController> logger, DNAProcessingService dnaProcessingService)
        {
            _logger = logger;
            _processingService = dnaProcessingService;
        }

        [HttpPost(Name = "Mutant")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> IsMutant(List<string> val)
        {
            var x = CastToDNAMatrix(val);
            int k = await _processingService.IsMutant(x);
            if (k >= 2)
            {
                return (true);
            }
            else
                return NotFound(false);
        }

        private static string [,] CastToDNAMatrix(List<string> val)
        {
            int len = val[0].Length;
            string[,] dna = new string[len, len];
            for (int i = 0; i < len; i++)
            {
                var res = val[i].Select(x => new string(x, 1)).ToArray();
                for (int j = 0; j < len; j++)
                {
                    dna[i, j] = res[j];

                }
            }
            return dna;
        }

        [HttpGet(Name = "Stats")]
        public Stats Stats()
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
