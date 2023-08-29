using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Timer.Models.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TimeLog> TimeLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modBuilder)
        {
            base.OnModelCreating(modBuilder);

            modBuilder.Entity<Category>().HasData(
                new Category {Id = 1, Name = "IT Support", Description = "Support for technology related issues", Billable = true, Active = true, VAT = 13.4},
                new Category { Id = 2, Name = "Delivery", Description = "Delivering products to customers", Billable = false, Active = false, VAT = 14.0 },
                new Category { Id = 3, Name = "Software Development", Description = "Creating new software solutions", Billable = true, Active = true, VAT = 13.7 }
            );

            modBuilder.Entity<Customer>().HasData(
                new Customer {Id = 1, Name = "Madrigal", Code = "MDR", Address = "Road Street 118", City = "New York", Email = "madrigal@gmail.com", Phone = "001/111-111", Active = true},
                new Customer { Id = 2, Name = "Company Inc.", Code = "CMP", Address = "Street Avenue 3", City = "Berlin", Email = "compinc@gmail.com", Phone = "033/333-333", Active = false }
                );

            modBuilder.Entity<Task>().HasData(
                new Task { Id = 1, SequentialNumber = 1, Name = "Timer Project", CustomerId = 1, CategoryId = 3, IdentityUserId = "625605f6-078a-4814-80ef-b304afb34d32", EstimatedHours = 60, StartDate = DateTime.Today, EndDate = DateTime.Now, Priority = 7 },
                new Task { Id = 2, SequentialNumber = 4, Name = "Connection Error", CustomerId = 1, CategoryId = 1, IdentityUserId = "625605f6-078a-4814-80ef-b304afb34d32", EstimatedHours = 2, StartDate = DateTime.Now, EndDate = DateTime.Now + TimeSpan.FromHours(2), Priority = 9 }
            );

            modBuilder.Entity<TimeLog>().HasData(
                new TimeLog {Id = 1, CustomerId = 1, CategoryId = 3, TaskId = 1, StartTime = DateTime.Now.TimeOfDay, EndTime = DateTime.Now.TimeOfDay + TimeSpan.FromHours(8),  Duration = TimeSpan.FromHours(8), Billable = true, Notes = "Worked on the database for the project", UserId = "625605f6-078a-4814-80ef-b304afb34d32" },
                new TimeLog { Id = 2, CustomerId = 1, CategoryId = 3, TaskId = 1, StartTime = DateTime.Now.TimeOfDay + TimeSpan.FromDays(1), EndTime = DateTime.Now.TimeOfDay + TimeSpan.FromHours(8) + TimeSpan.FromDays(1), Duration = TimeSpan.FromHours(8), Billable = true, Notes = "Worked on the database for the project", UserId = "625605f6-078a-4814-80ef-b304afb34d32" }
            );
        }

    }
}