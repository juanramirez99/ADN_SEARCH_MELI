using Belgrade.SqlClient;
using Belgrade.SqlClient.SqlDb;
using LEVEL2_ADN_PROCESSING_API.Service;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Net;


namespace LEVEL2_ADN_PROCESSING_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ADNProcessingController : ControllerBase
    {
        private readonly ILogger<ADNProcessingController> _logger;
        private readonly DNAProcessingService _processingService;




        public ADNProcessingController(ILogger<ADNProcessingController> logger, DNAProcessingService dnaProcessingService )
        {
            _logger = logger;
            _processingService = dnaProcessingService;
           
        }

        [HttpPost(Name = "Mutant")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> IsMutant(List<string> val)
        {
            var x = _processingService.CastToDNAMatrix(val);
            int k = _processingService.IsMutant(x);
            if (k >= 2)
            {
                return (true);
            }
            else
                return NotFound(false);
        }

        [HttpGet(Name = "Stats")]
        public Task<Stats> Stats()
        {
            var result = _processingService.GetRatio();
            return result;
            
         }
    }
}
