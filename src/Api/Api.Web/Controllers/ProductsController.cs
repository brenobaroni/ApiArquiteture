using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Commands;
using Api.Domain.Queries;
using Wolverine;

namespace Api.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMessageBus _bus;

        public ProductsController(IMessageBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProductCommand command)
        {
            var product = await _bus.InvokeAsync<Api.Domain.Entities.Product>(command);
            return CreatedAtAction(nameof(Get), new { id = product.id }, product);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var product = await _bus.InvokeAsync<Api.Domain.Entities.Product?>(new GetProductQuery(id));
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var products = await _bus.InvokeAsync<IEnumerable<Api.Domain.Entities.Product>>(new GetAllProductsQuery());
            return Ok(products);
        }
    }
}
