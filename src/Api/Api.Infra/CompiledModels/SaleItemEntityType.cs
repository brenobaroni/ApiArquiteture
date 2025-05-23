﻿// <auto-generated />
using System;
using System.Collections.Generic;
using System.Reflection;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#pragma warning disable 219, 612, 618
#nullable disable

namespace Api.Data.CompiledModels
{
    [EntityFrameworkInternal]
    public partial class SaleItemEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "Api.Domain.Entities.SaleItem",
                typeof(SaleItem),
                baseEntityType,
                propertyCount: 10,
                navigationCount: 2,
                foreignKeyCount: 2,
                unnamedIndexCount: 2,
                keyCount: 1);

            var id = runtimeEntityType.AddProperty(
                "id",
                typeof(Guid),
                propertyInfo: typeof(Base).GetProperty("id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Base).GetField("<id>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                valueGenerated: ValueGenerated.OnAdd,
                afterSaveBehavior: PropertySaveBehavior.Throw,
                sentinel: new Guid("00000000-0000-0000-0000-000000000000"));
            id.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

            var create_at = runtimeEntityType.AddProperty(
                "create_at",
                typeof(DateTime),
                propertyInfo: typeof(BaseData).GetProperty("create_at", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(BaseData).GetField("<create_at>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
            create_at.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

            var discount = runtimeEntityType.AddProperty(
                "discount",
                typeof(decimal),
                propertyInfo: typeof(SaleItem).GetProperty("discount", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(SaleItem).GetField("<discount>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: 0m);
            discount.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

            var is_cancelled = runtimeEntityType.AddProperty(
                "is_cancelled",
                typeof(bool),
                propertyInfo: typeof(SaleItem).GetProperty("is_cancelled", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(SaleItem).GetField("<is_cancelled>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: false);
            is_cancelled.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

            var product_id = runtimeEntityType.AddProperty(
                "product_id",
                typeof(Guid),
                propertyInfo: typeof(SaleItem).GetProperty("product_id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(SaleItem).GetField("<product_id>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: new Guid("00000000-0000-0000-0000-000000000000"));
            product_id.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

            var quantity = runtimeEntityType.AddProperty(
                "quantity",
                typeof(int),
                propertyInfo: typeof(SaleItem).GetProperty("quantity", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(SaleItem).GetField("<quantity>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: 0);
            quantity.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

            var sale_id = runtimeEntityType.AddProperty(
                "sale_id",
                typeof(Guid),
                propertyInfo: typeof(SaleItem).GetProperty("sale_id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(SaleItem).GetField("<sale_id>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: new Guid("00000000-0000-0000-0000-000000000000"));
            sale_id.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

            var total = runtimeEntityType.AddProperty(
                "total",
                typeof(decimal),
                propertyInfo: typeof(SaleItem).GetProperty("total", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(SaleItem).GetField("<total>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: 0m);
            total.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

            var unit_price = runtimeEntityType.AddProperty(
                "unit_price",
                typeof(decimal),
                propertyInfo: typeof(SaleItem).GetProperty("unit_price", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(SaleItem).GetField("<unit_price>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                sentinel: 0m);
            unit_price.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

            var update_at = runtimeEntityType.AddProperty(
                "update_at",
                typeof(DateTime?),
                propertyInfo: typeof(BaseData).GetProperty("update_at", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(BaseData).GetField("<update_at>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            update_at.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

            var key = runtimeEntityType.AddKey(
                new[] { id });
            runtimeEntityType.SetPrimaryKey(key);

            var index = runtimeEntityType.AddIndex(
                new[] { product_id });

            var index0 = runtimeEntityType.AddIndex(
                new[] { sale_id });

            return runtimeEntityType;
        }

        public static RuntimeForeignKey CreateForeignKey1(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
        {
            var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("product_id") },
                principalEntityType.FindKey(new[] { principalEntityType.FindProperty("id") }),
                principalEntityType,
                deleteBehavior: DeleteBehavior.Cascade,
                required: true);

            var product = declaringEntityType.AddNavigation("product",
                runtimeForeignKey,
                onDependent: true,
                typeof(Product),
                propertyInfo: typeof(SaleItem).GetProperty("product", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(SaleItem).GetField("<product>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            var sale_items = principalEntityType.AddNavigation("sale_items",
                runtimeForeignKey,
                onDependent: false,
                typeof(ICollection<SaleItem>),
                propertyInfo: typeof(Product).GetProperty("sale_items", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Product).GetField("<sale_items>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            return runtimeForeignKey;
        }

        public static RuntimeForeignKey CreateForeignKey2(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
        {
            var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("sale_id") },
                principalEntityType.FindKey(new[] { principalEntityType.FindProperty("id") }),
                principalEntityType,
                deleteBehavior: DeleteBehavior.Cascade,
                required: true);

            var sale = declaringEntityType.AddNavigation("sale",
                runtimeForeignKey,
                onDependent: true,
                typeof(Sale),
                propertyInfo: typeof(SaleItem).GetProperty("sale", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(SaleItem).GetField("<sale>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            var items = principalEntityType.AddNavigation("items",
                runtimeForeignKey,
                onDependent: false,
                typeof(ICollection<SaleItem>),
                propertyInfo: typeof(Sale).GetProperty("items", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Sale).GetField("<items>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            return runtimeForeignKey;
        }

        public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
        {
            runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
            runtimeEntityType.AddAnnotation("Relational:Schema", "shop");
            runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
            runtimeEntityType.AddAnnotation("Relational:TableName", "sales_items");
            runtimeEntityType.AddAnnotation("Relational:ViewName", null);
            runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

            Customize(runtimeEntityType);
        }

        static partial void Customize(RuntimeEntityType runtimeEntityType);
    }
}
