using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesTest.src.Entities;
using SalesTest.src.Services;

namespace SalesTest.src.Controllers
{
    [ApiController]
    [Route("api/sales")]
    public class SalesController : ControllerBase
    {
        private readonly SaleService _saleService;

        public SalesController(SaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSale([FromBody] Sale sale)
        {
            var createdSale = await _saleService.CreateSaleAsync(sale);
            return CreatedAtAction(nameof(GetSaleById), new { id = createdSale.Id }, createdSale);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllSales()
        {
            var sales = await _saleService.GetAllSalesAsync();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetSaleById(Guid id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null) return NotFound(new { message = "Venda não encontrada." });
            return Ok(sale);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateSale(Guid id, [FromBody] Sale updatedSale)
        {
            var sale = await _saleService.UpdateSaleAsync(id, updatedSale);
            if (sale == null) return NotFound(new { message = "Venda não encontrada." });
            return Ok(sale);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSale(Guid id)
        {
            var deleted = await _saleService.DeleteSaleAsync(id);
            if (!deleted) return NotFound(new { message = "Venda não encontrada." });
            return NoContent();
        }

        [HttpGet("filtered")]
        [Authorize]
        public async Task<IActionResult> GetFilteredSales(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] string? customer,
            [FromQuery] string? branch,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var sales = await _saleService.GetFilteredSalesAsync(startDate, endDate, customer, branch, page, pageSize);
            return Ok(sales);
        }

        [HttpPatch("{id}/cancel-item/{productId}")]
        [Authorize]
        public async Task<IActionResult> CancelSaleItem(Guid id, int productId)
        {
            var updatedSale = await _saleService.CancelSaleItemAsync(id, productId);
            if (updatedSale == null) return NotFound(new { message = "Venda ou item não encontrado." });

            return Ok(updatedSale);
        }
    }
}
