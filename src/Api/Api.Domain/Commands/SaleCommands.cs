using FluentValidation;
using System;
using System.Collections.Generic;

namespace Api.Domain.Commands
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("Sale number is required.");
            RuleFor(x => x.SaleDate).NotEmpty().WithMessage("Sale date is required.");
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer ID is required.");
            RuleFor(x => x.BranchId).NotEmpty().WithMessage("Branch ID is required.");
            RuleFor(x => x.Items).NotEmpty().WithMessage("At least one item is required.");
            RuleForEach(x => x.Items).SetValidator(new CreateSaleItemValidator());
        }
    }

    public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemCommand>
    {
        public CreateSaleItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required.");
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantity is required.");
            RuleFor(x => x.Quantity).LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");
            RuleFor(x => x.UnitPrice).NotEmpty().WithMessage("Unit price is required.");
            RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("Unit price must be greater than zero.");
        }
    }

    public record class CreateSaleCommand
    {
        public string SaleNumber { get; init; }
        public DateTime SaleDate { get; init; }
        public Guid CustomerId { get; init; } = Guid.NewGuid();
        public Guid BranchId { get; init; } = Guid.NewGuid();
        public List<CreateSaleItemCommand> Items { get; init; } = new List<CreateSaleItemCommand>();
    }

    public record class CreateSaleItemCommand
    {
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
    }

    public record class UpdateSaleCommand
    {
        public Guid Id { get; init; }
        public string SaleNumber { get; init; }
        public DateTime SaleDate { get; init; }
        public Guid CustomerId { get; init; }
        public Guid BranchId { get; init; }
        public List<UpdateSaleItemCommand> Items { get; init; } = new List<UpdateSaleItemCommand>();
    }

    public record class UpdateSaleItemCommand
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
    }

    public record class DeleteSaleCommand(Guid id);
    public record class CancelSaleCommand(Guid id);
}

namespace Api.Domain.Queries
{
    public record class GetSaleQuery(Guid id);
    public record class GetAllSalesQuery();
    public record class GetSalesByCustomerQuery(Guid customerId);
    public record class GetSalesByBranchQuery(Guid branchId);
    public record class GetSalesByDateRangeQuery(DateTime startDate, DateTime endDate);
}