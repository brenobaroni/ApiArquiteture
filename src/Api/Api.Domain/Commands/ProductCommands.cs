namespace Api.Domain.Commands
{
    public record class CreateProductCommand
    {
        public int name { get; init; }
        public float price { get; init; }
    }
}

namespace Api.Domain.Queries
{
    public record class GetProductQuery(int id);
    public record class GetAllProductsQuery();
}