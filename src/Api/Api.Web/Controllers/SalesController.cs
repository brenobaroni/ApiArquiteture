using Api.Domain.Commands;
using Api.Domain.Queries;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<ResponseModel<Api.Domain.Entities.Sale>>> Create([FromBody] CreateSaleCommand command)
        {
            try
            {
                var sale = await _bus.InvokeAsync<Api.Domain.Entities.Sale>(command);
                return Ok(new ResponseModel<Api.Domain.Entities.Sale>(sale, "success", "Sale created successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErrorResponseModel("BadRequest", "Invalid Sell", ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<Api.Domain.Entities.Sale>>> Update(Guid id, [FromBody] UpdateSaleCommand command)
        {
            try
            {
                if (id != command.Id)
                    return BadRequest(new ErrorResponseModel("BadRequest", "Invalid Update", "ID mismatch"));

                var sale = await _bus.InvokeAsync<Api.Domain.Entities.Sale>(command);
                return Ok(new ResponseModel<Api.Domain.Entities.Sale>(sale, "success", "Sale updated successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErrorResponseModel("BadRequest", "Invalid Update", ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<Api.Domain.Entities.Sale>>> Get(Guid id)
        {
            var sale = await _bus.InvokeAsync<Api.Domain.Entities.Sale?>(new GetSaleQuery(id));
            if (sale == null)
                return NotFound(new ErrorResponseModel("NotFound", "Sale not found", $"Sale with id {id} not found"));
            return Ok(new ResponseModel<Api.Domain.Entities.Sale>(sale));
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<Api.Domain.Entities.Sale>>>> GetAll(
            [FromQuery] Guid? customerId = null,
            [FromQuery] Guid? branchId = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            if (customerId.HasValue)
            {
                var customerSales = await _bus.InvokeAsync<IEnumerable<Api.Domain.Entities.Sale>>(new GetSalesByCustomerQuery(customerId.Value));
                return Ok(new ResponseModel<IEnumerable<Api.Domain.Entities.Sale>>(customerSales));
            }

            if (branchId.HasValue)
            {
                var branchSales = await _bus.InvokeAsync<IEnumerable<Api.Domain.Entities.Sale>>(new GetSalesByBranchQuery(branchId.Value));
                return Ok(new ResponseModel<IEnumerable<Api.Domain.Entities.Sale>>(branchSales));
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                var dateSales = await _bus.InvokeAsync<IEnumerable<Api.Domain.Entities.Sale>>(new GetSalesByDateRangeQuery(startDate.Value, endDate.Value));
                return Ok(new ResponseModel<IEnumerable<Api.Domain.Entities.Sale>>(dateSales));
            }

            var sales = await _bus.InvokeAsync<IEnumerable<Api.Domain.Entities.Sale>>(new GetAllSalesQuery());
            return Ok(new ResponseModel<IEnumerable<Api.Domain.Entities.Sale>>(sales));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<object>>> Delete(Guid id)
        {
            await _bus.InvokeAsync(new DeleteSaleCommand(id));
            return Ok(new ResponseModel<object>(null, "success", "Sale removed successfully"));
        }

        [HttpPost("{id}/cancel")]
        public async Task<ActionResult<ResponseModel<object>>> Cancel(Guid id)
        {
            await _bus.InvokeAsync(new CancelSaleCommand(id));
            return Ok(new ResponseModel<object>(null, "success", "Sale cancelled successfully"));
        }
    }
}
