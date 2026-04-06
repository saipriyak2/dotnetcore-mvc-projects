using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaPanda_Store.Models;

namespace FoodRestaurant.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }


        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            // Define composite key and relationships for ProductIngredient
            modelbuilder.Entity<ProductIngredient>()
                .HasKey(pi => new { pi.ProductId, pi.IngredientId });

            modelbuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductIngredients)
                .HasForeignKey(pi => pi.ProductId);

            modelbuilder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Ingredient)
                .WithMany(p => p.ProductIngredients)
                .HasForeignKey(pi => pi.IngredientId);

            //Seed Data
            modelbuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Veg Pizza" },
                new Category { CategoryId = 2, Name = "Non-Veg Pizza" },
                new Category { CategoryId = 3, Name = "Beverages" },
                new Category { CategoryId = 4, Name = "Side Dish" },
                new Category { CategoryId = 5, Name = "Dessert" });

            modelbuilder.Entity<Ingredient>().HasData(
                new Ingredient { IngredientId = 1, Name = "Tortilla" },
                new Ingredient { IngredientId = 2, Name = "Lettuce" },
                new Ingredient { IngredientId = 3, Name = "Tomato" },
                new Ingredient { IngredientId = 4, Name = "Cheese" },
                new Ingredient { IngredientId = 5, Name = "Beef" },
                new Ingredient { IngredientId = 6, Name = "Chicken" },
                new Ingredient { IngredientId = 7, Name = "Fish" }
                );

            modelbuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Margherita",
                    Description = "A delicious marhherita pizza",
                    Price = 8.99m,
                    Stock = 100,
                    CategoryId = 1
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Pepperoni Pizza",
                    Description = "A delicious pepperoni pizza",
                    Price = 16.99m,
                    Stock = 80,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 3,
                    Name = "BBQ Chicken Pizza",
                    Description = "A delicious chicken pizza",
                    Price = 14.49m,
                    Stock = 60,
                    CategoryId = 2
                });

            modelbuilder.Entity<ProductIngredient>().HasData(
                new ProductIngredient { ProductId = 1, IngredientId = 1 },
                new ProductIngredient { ProductId = 1, IngredientId = 2 },
                new ProductIngredient { ProductId = 1, IngredientId = 3 },
                new ProductIngredient { ProductId = 1, IngredientId = 4 },
                new ProductIngredient { ProductId = 2, IngredientId = 5 },
                new ProductIngredient { ProductId = 2, IngredientId = 1 },
                new ProductIngredient { ProductId = 2, IngredientId = 2 },
                new ProductIngredient { ProductId = 2, IngredientId = 4 },
                new ProductIngredient { ProductId = 3, IngredientId = 6 },
                new ProductIngredient { ProductId = 3, IngredientId = 1 },
                new ProductIngredient { ProductId = 3, IngredientId = 2 },
                new ProductIngredient { ProductId = 3, IngredientId = 4 }

                );

        }
    }
}