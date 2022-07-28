using ECommProject.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommProject.Models
{
    public class Category
    {
        public int id { get; set; }
        public string CategoryName { get; set; }

    }
    public class Product
    {
        public int id { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        [ForeignKey("category")]
        public int CategoryId { get; set; }
        public Category category { get; set; }
        public int ProductPrice { get; set; }
        [NotMapped]
        public IFormFile formFile { get; set; }
        public string path { get; set; }
        public bool check { get; set; }

    }


    
    public class Inventory
    {
        public int id { get; set; }
        [ForeignKey("product")]
        public int ProductId { get; set; }
        public Product product { get; set; }
       
        public int Quantity { get; set; }
        public int price { get; set; }

    }
    public class CartItem
    {
        public int Id { get; set; }
        [ForeignKey("category")]
        public int categoryId { get; set; }
        public Category category { get; set; }
        [ForeignKey("product")]
        public int productid { get; set; }
        public Product product { get; set; }
        public int Quantity { get; set; }
        

    }
    public class Sales
    {
        public int id { get; set; }
        public int totalPrice { get; set; }
        public DateTime timeStamp { get; set; } = DateTime.UtcNow;

    }
    public class ProductSold
    {
        public int Id { get; set; }
        [ForeignKey("sale")]
        public int saleId { get; set; }
        public Sales sale { get; set; }
        public string Name { get; set; }

    }
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) { }
        public DbSet<Category> category { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<Inventory> inventory { get; set; }
        public DbSet<CartItem> cart { get; set; }
        public DbSet<ProductSold> productSold { get; set; }
        public DbSet<Sales> sales { get; set; } 
    }
}