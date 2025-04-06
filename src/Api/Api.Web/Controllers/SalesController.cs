using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Commands;
using Api.Domain.Queries;
using Wolverine;

namespace Api.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMessageBus _bus;

        public SalesController(IMessageBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateSaleCommand command)
        {
            var sale = await _bus.InvokeAsync<Api.Domain.Entities.Sale>(command);
            return CreatedAtAction(nameof(Get), new { id = sale.id }, sale);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var sale = await _bus.InvokeAsync<Api.Domain.Entities.Sale?>(new GetSaleQuery(id));
            if (sale == null)
                return NotFound();
            return Ok(sale);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var sales = await _bus.InvokeAsync<IEnumerable<Api.Domain.Entities.Sale>>(new GetAllSalesQuery());
            return Ok(sales);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _bus.InvokeAsync(new DeleteSaleCommand(id));
            return NoContent();
        }
    }
}
