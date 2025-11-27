
using BulkeyBook.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulkeyBook.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categorys { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Jewelry" },
                new Category { Id = 2, Name = "Bags" },
                new Category { Id = 3, Name = "Shoes" }
            );

            modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Title = "Leather Wallet",
                Description = "Premium handcrafted leather wallet with multiple card slots and a slim, elegant design.",
                Price = 25,
                CategoryId = 1,
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/61BygHH-M2L.jpg"
            },

            new Product
            {
                Id = 2,
                Title = "Classic Wristwatch",
                Description = "Minimalist stainless-steel wristwatch with waterproof build and adjustable strap.",
                Price = 120,
                CategoryId = 1,
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTO4mkuWYl3Od8zCp371ZqsWRdMZ_67G5DrXA&s"
            },

            new Product
            {
                Id = 3,
                Title = "Sunglasses",
                Description = "UV400-protected sunglasses with lightweight frame and modern style.",
                Price = 35,
                CategoryId = 1,
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSH1uw5ScCjhOOkas_LI2yn2Z6ef-n04Z8V_g&s"
            },

            new Product
            {
                Id = 4,
                Title = "Travel Backpack",
                Description = "Durable water-resistant backpack with 3 compartments and laptop pocket.",
                Price = 65,
                CategoryId = 1,
                ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRcRk7MJ8S4wwT_4SYkSt5uNgHWhr_u6wo0dQ&s"
            },

            new Product
            {
                Id = 5,
                Title = "Wireless Earbuds",
                Description = "Noise-isolating Bluetooth earbuds with fast charging and 8-hour battery life.",
                Price = 55,
                CategoryId = 1,
                ImageUrl = "https://m.media-amazon.com/images/I/51GEDaBzrwL.jpg"
            }

            );
        }

    }
}
