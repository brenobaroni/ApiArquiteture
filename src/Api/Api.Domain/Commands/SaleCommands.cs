using FluentValidation;

namespace Api.Domain.Commands
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(x => x.customer_id).NotEmpty().WithMessage("Customer ID is required.");
            RuleFor(x => x.items).NotEmpty().WithMessage("At least one item is required.");
            RuleForEach(x => x.items).SetValidator(new CreateSaleItemValidator());
        }
    }

    public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemCommand>
    {
        public CreateSaleItemValidator()
        {
            RuleFor(x => x.product_id).NotEmpty().WithMessage("Product ID is required.");
            RuleFor(x => x.quantity).NotEmpty().WithMessage("Quantity is required.");
            RuleFor(x => x.price).NotEmpty().WithMessage("Price is required.");
            RuleFor(x => x.price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }

    public record class CreateSaleCommand
    {
        public int customer_id { get; init; }
        public List<CreateSaleItemCommand> items { get; init; } = new List<CreateSaleItemCommand>()!;
    }

    public record CreateSaleItemCommand
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
    }

    public record class DeleteSaleCommand(int id);
    public record class GetSaleQuery(int id);
    public record class GetAllSalesQuery();
}

namespace Api.Domain.Queries
{
    public record class GetSaleQuery(int id);
    public record class GetAllSalesQuery();
} 