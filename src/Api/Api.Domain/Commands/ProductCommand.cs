using FluentValidation;

namespace Api.Domain.Commands
{
    public class CreateProductValidator : AbstractValidator<ProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.price).NotEmpty().WithMessage("Price is required.").GreaterThan(0).WithMessage("Price must be greater than 0.");
            RuleFor(x => x.name).NotNull().WithMessage("Name is required.").NotEmpty().WithMessage("Name is required.");
        }
    }

    public record class ProductCommand
    {
        public int name { get; init; }
        public float price { get; init; }
    }
}
