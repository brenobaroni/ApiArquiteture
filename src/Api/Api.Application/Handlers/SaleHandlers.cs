using Api.Domain.Commands;
using Api.Domain.Queries;
using Api.Domain.Entities;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Wolverine.Attributes;

namespace Api.Application.Handlers
{
    public static class SaleHandlers
    {
        [WolverineHandler]
        public static async Task<Sale> Handle(CreateSaleCommand command, AppDbContext context)
        {
            var sale = new Sale
            {
                create_at = DateTime.UtcNow,
                sale_items = command.items.Select(item => new SaleItem
                {
                    product_id = item.product_id,
                    quantity = item.quantity,
                    price = item.CalculateTotal(),
                }).ToList()
            };

            await context.Sales.AddAsync(sale);
            await context.SaveChangesAsync();

            return sale;
        }

        [WolverineHandler]
        public static async Task<Sale?> Handle(GetSaleQuery query, AppDbContext context)
        {
            return await context.Sales
                .Include(s => s.sale_items)
                .ThenInclude(si => si.product)
                .FirstOrDefaultAsync(s => s.id == query.id);
        }

        [WolverineHandler]
        public static async Task<IEnumerable<Sale>> Handle(GetAllSalesQuery query, AppDbContext context)
        {
            return await context.Sales
                .Include(s => s.sale_items)
                .ThenInclude(si => si.product)
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
    }
} 