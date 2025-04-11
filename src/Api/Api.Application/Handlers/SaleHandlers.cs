using Api.Data;
using Api.Domain.Commands;
using Api.Domain.Entities;
using Api.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using Wolverine;
using Wolverine.Attributes;

namespace Api.Application.Handlers
{
    public static class SaleHandlers
    {
        [WolverineHandler]
        public static async Task<Sale> Handle(CreateSaleCommand command, AppDbContext context, IMessageBus _bus)
        {
            var sale = new Sale
            {
                id = Guid.NewGuid(),
                sale_number = command.SaleNumber,
                sale_date = command.SaleDate,
                customer_id = command.CustomerId,
                branch_id = command.BranchId,
                cancelled = false,
                total_amount = 0
            };

            decimal totalAmount = 0;

            foreach (var item in command.Items)
            {
                var product = await context.Products.FindAsync(item.ProductId);
                if (product == null)
                    throw new InvalidOperationException($"Product with ID {item.ProductId} not found");

                if (item.Quantity > 20)
                    throw new InvalidOperationException($"Maximum quantity allowed per product is 20. Product: {product.title}");

                decimal discountPercentage = 0;
                if (item.Quantity >= 10 && item.Quantity <= 20)
                {
                    discountPercentage = 0.20m;
                }
                else if (item.Quantity >= 4)
                {
                    discountPercentage = 0.10m;
                }

                decimal unitPrice = product.price;
                decimal discountAmount = unitPrice * discountPercentage;
                decimal finalUnitPrice = unitPrice - discountAmount;
                decimal itemTotal = finalUnitPrice * item.Quantity;

                var saleItem = new SaleItem
                {
                    id = Guid.NewGuid(),
                    sale_id = sale.id,
                    product_id = item.ProductId,
                    quantity = item.Quantity,
                    unit_price = unitPrice,
                    discount = discountAmount,
                    total = itemTotal,
                    is_cancelled = false
                };

                sale.items.Add(saleItem);
                totalAmount += itemTotal;
            }

            sale.total_amount = totalAmount;
            await context.Sales.AddAsync(sale);
            await context.SaveChangesAsync();

            await _bus.PublishAsync(sale);

            return sale;
        }

        [WolverineHandler]
        public static async Task<Sale> Handle(UpdateSaleCommand command, AppDbContext context)
        {
            var sale = await context.Sales
                .Include(s => s.items)
                .FirstOrDefaultAsync(s => s.id == command.Id);

            if (sale == null)
                throw new InvalidOperationException("Sale not found");

            if (sale.cancelled)
                throw new InvalidOperationException("Cannot update a cancelled sale");

            sale.sale_number = command.SaleNumber;
            sale.sale_date = command.SaleDate;
            sale.customer_id = command.CustomerId;
            sale.branch_id = command.BranchId;
            sale.total_amount = 0;

            context.RemoveRange(sale.items);
            sale.items.Clear();

            decimal totalAmount = 0;

            foreach (var item in command.Items)
            {
                var product = await context.Products.FindAsync(item.ProductId);
                if (product == null)
                    throw new InvalidOperationException($"Product with ID {item.ProductId} not found");

                if (item.Quantity > 20)
                    throw new InvalidOperationException($"Maximum quantity allowed per product is 20. Product: {product.title}");

                decimal discountPercentage = 0;
                if (item.Quantity >= 10 && item.Quantity <= 20)
                {
                    discountPercentage = 0.20m;
                }
                else if (item.Quantity >= 4)
                {
                    discountPercentage = 0.10m;
                }

                decimal unitPrice = product.price;
                decimal discountAmount = unitPrice * discountPercentage;
                decimal finalUnitPrice = unitPrice - discountAmount;
                decimal itemTotal = finalUnitPrice * item.Quantity;

                var saleItem = new SaleItem
                {
                    id = Guid.NewGuid(),
                    sale_id = sale.id,
                    product_id = item.ProductId,
                    quantity = item.Quantity,
                    unit_price = unitPrice,
                    discount = discountAmount,
                    total = itemTotal,
                    is_cancelled = false
                };

                sale.items.Add(saleItem);
                totalAmount += itemTotal;
            }

            sale.total_amount = totalAmount;
            await context.SaveChangesAsync();
            return sale;
        }

        private static decimal CalculateDiscount(int quantity, decimal unitPrice)
        {
            if (quantity < 4)
                return 0;

            if (quantity >= 10 && quantity <= 20)
                return (unitPrice * quantity) * 0.2m; // 20% discount

            if (quantity >= 4)
                return (unitPrice * quantity) * 0.1m; // 10% discount

            return 0;
        }

        [WolverineHandler]
        public static async Task<Sale?> Handle(GetSaleQuery query, AppDbContext context)
        {
            return await context.Sales
                .Include(s => s.items)
                .ThenInclude(si => si.product)
                .FirstOrDefaultAsync(s => s.id == query.id);
        }

        [WolverineHandler]
        public static async Task<IEnumerable<Sale>> Handle(GetAllSalesQuery query, AppDbContext context)
        {
            return await context.Sales
                .Include(s => s.items)
                .ThenInclude(si => si.product)
                .ToListAsync();
        }

        [WolverineHandler]
        public static async Task<IEnumerable<Sale>> Handle(GetSalesByCustomerQuery query, AppDbContext context)
        {
            return await context.Sales
                .Include(s => s.items)
                .ThenInclude(si => si.product)
                .Where(s => s.customer_id == query.customerId)
                .ToListAsync();
        }

        [WolverineHandler]
        public static async Task<IEnumerable<Sale>> Handle(GetSalesByBranchQuery query, AppDbContext context)
        {
            return await context.Sales
                .Include(s => s.items)
                .ThenInclude(si => si.product)
                .Where(s => s.branch_id == query.branchId)
                .ToListAsync();
        }

        [WolverineHandler]
        public static async Task<IEnumerable<Sale>> Handle(GetSalesByDateRangeQuery query, AppDbContext context)
        {
            return await context.Sales
                .Include(s => s.items)
                .ThenInclude(si => si.product)
                .Where(s => s.sale_date >= query.startDate && s.sale_date <= query.endDate)
                .ToListAsync();
        }

        [WolverineHandler]
        public static async Task Handle(DeleteSaleCommand command, AppDbContext context)
        {
            var sale = await context.Sales.FindAsync(command.id);
            if (sale != null)
            {
                context.Sales.Remove(sale);
                await context.SaveChangesAsync();
            }
        }

        [WolverineHandler]
        public static async Task Handle(CancelSaleCommand command, AppDbContext context)
        {
            var sale = await context.Sales
                .Include(s => s.items)
                .FirstOrDefaultAsync(s => s.id == command.id);

            if (sale != null && !sale.cancelled)
            {
                sale.cancelled = true;
                foreach (var item in sale.items)
                {
                    item.is_cancelled = true;
                }
                await context.SaveChangesAsync();
            }
        }
    }
}