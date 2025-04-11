using Api.Domain.Commands;
using Api.Domain.Queries;
using Api.Domain.Entities;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Wolverine.Attributes;

namespace Api.Application.Handlers
{
    public static class ProductHandlers
    {
        [WolverineHandler]
        public static async Task<Product> Handle(CreateProductCommand command, AppDbContext context)
        {
            var product = new Product
            {
                title = "Product Name",
                description = command.description,
                category = command.category,
                price = command.price,
                image = command.image,
                create_at = DateTime.UtcNow
            };

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            return product;
        }

        [WolverineHandler]
        public static async Task<Product?> Handle(GetProductQuery query, AppDbContext context)
        {
            return await context.Products.FindAsync(query.id);
        }

        [WolverineHandler]
        public static async Task<IEnumerable<Product>> Handle(GetAllProductsQuery query, AppDbContext context)
        {
            return await context.Products.ToListAsync();
        }
    }
} 