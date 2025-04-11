namespace Api.Domain.Commands
{
    public record class CreateProductCommand
    {
        public string description { get; init; }
        public decimal price { get; init; }
        public string category { get; init; }
        public string image { get; init; }
    }
}

namespace Api.Domain.Queries
{
    public record class GetProductQuery(Guid id);
    public record class GetAllProductsQuery();
}