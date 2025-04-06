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
            RuleFor(x => x.quantity).LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");
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
        public float price { get; set; }


        public float CalculateDiscount()
        {
            if (quantity < 4)
                return 0;

            if (quantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items");

            if (quantity >= 10 && quantity <= 20)
                return price * quantity * 0.2f; // 20% discount

            if (quantity >= 4)
                return price * quantity * 0.1f; // 10% discount

            return 0;
        }

        public float CalculateTotal()
        {
            return (price * quantity) - CalculateDiscount();
        }
    }

    public record class DeleteSaleCommand(int id);
}

namespace Api.Domain.Queries
{
    public record class GetSaleQuery(int id);
    public record class GetAllSalesQuery();
} 