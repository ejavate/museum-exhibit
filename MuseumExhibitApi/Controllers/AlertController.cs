using Microsoft.AspNetCore.Mvc;
using MuseumExhibitApi.Models;
using MuseumExhibitApi.Services;

namespace MuseumExhibitApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertController : ControllerBase
    {
        private readonly ReportService _reportService;

        public AlertController(ReportService reportService)
        {
            _reportService = reportService;
        }

        public class AlertRequest
        {
            public string Description { get; set; }
            public string Metadata { get; set; }
        }

        [HttpPost("report")]
        public async Task<IActionResult> ReceiveAlert([FromBody] AlertRequest request)
        {
            var alert = _reportService.AddAlert(request.Description, request.Metadata);
            var report = await _reportService.GenerateReportForAlertAsync(
                alert.Id,
                request.Description,
                request.Metadata
            );
            return Ok(report);
        }
    }
}