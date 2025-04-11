using Api.Domain.Entities;

namespace Api.Domain.Commands
{
    public record SaleCreatedEvent(Sale sale)
    {
        readonly Guid id = sale.id;
    }
}
