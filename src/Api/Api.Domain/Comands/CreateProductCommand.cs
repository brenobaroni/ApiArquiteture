using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Domain.Comands
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.price).NotEmpty().WithMessage("Price is required.").GreaterThan(0).WithMessage("Price must be greater than 0.");
            RuleFor(x => x.name).NotNull().WithMessage("Name is required.").NotEmpty().WithMessage("Name is required.");
        }
    }

    public record class CreateProductCommand
    {
        public int name { get; init; }
        public float price { get; init; }
    }
}
