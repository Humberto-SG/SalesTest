using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/sales/events")]
public class SaleEventsController : ControllerBase
{
    private readonly SaleEventLogService _eventLogService;

    public SaleEventsController(SaleEventLogService eventLogService)
    {
        _eventLogService = eventLogService;
    }

    [HttpGet("{saleId}")]
    public async Task<IActionResult> GetEventsBySaleId(Guid saleId)
    {
        var events = await _eventLogService.GetEventsBySaleIdAsync(saleId);
        if (events.Count == 0) return NotFound(new { message = "Nenhum evento encontrado." });

        return Ok(events);
    }
}
