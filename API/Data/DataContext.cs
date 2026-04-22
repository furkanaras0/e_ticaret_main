using API.Entity;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

    public class  DataContext(DbContextOptions options) : DbContext(options)
    {
            public DbSet<Product> Products =>  Set<Product>();
       
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().HasData(
           new List<Product> {
           new Product{Id=1, Name="Apple 16",
                    Price=1000,
                    Description="This is an apple",
                    IsActive=true,
                    ImageUrl="1.jpeg",
                    Stock=200},
            new Product{Id=2, Name="Apple 15",
                    Price=1000,
                    Description="This is an apple",
                    IsActive=true,
                    ImageUrl="2.jpeg",
                    Stock=100},
            new Product{Id=3, Name="Apple 17",
                    Price=3000,
                    Description="This is an apple",
                    IsActive=true,
                    ImageUrl="3.jpeg",
                    Stock=100},
            new Product{Id=4, Name="Apple 14",
                    Price=4000,
                    Description="This is an apple",
                    IsActive=true,
                    ImageUrl="4.jpeg",
                    Stock=500}
                    
                
            }
                    
                     
        );
    }
    }
