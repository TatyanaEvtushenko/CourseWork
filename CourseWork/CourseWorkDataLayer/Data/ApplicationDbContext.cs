using CourseWork.DataLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.DataLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FinancialPurpose> FinancialPurposes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectSubscriber> ProjectSubscribers { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>().HasKey(rating => new { rating.UserName, rating.ProjectId });
            modelBuilder.Entity<Tag>().HasKey(tag => new { tag.Name, tag.ProjectId });
            modelBuilder.Entity<ProjectSubscriber>().HasKey(subscriber => new { subscriber.UserName, subscriber.ProjectId });
            modelBuilder.Entity<ApplicationUser>().HasIndex(user => user.UserName);
            base.OnModelCreating(modelBuilder);
        }
    }
}
