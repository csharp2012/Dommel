﻿using System.Threading.Tasks;
using Xunit;

namespace Dommel.IntegrationTests
{
    [Collection("Database")]
    public class MultiMapTests
    {
        [Theory]
        [ClassData(typeof(DatabaseTestData))]
        public async Task GetAsync(DatabaseDriver database)
        {
            using (var con = database.GetConnection())
            {
                var product = await con.GetAsync<Product, Category, Product>((p, c) =>
                {
                    p.Category = c;
                    return p;
                }, 1);

                Assert.NotNull(product);
                Assert.NotEmpty(product.Name);
                Assert.NotNull(product.Category);
                Assert.NotNull(product.Category.Name);
            }
        }
    }
}
