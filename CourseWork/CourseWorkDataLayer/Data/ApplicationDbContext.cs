using CourseWork.DataLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
            SetCommentOptions(modelBuilder);
            SetFinancialPurposeOptions(modelBuilder);
            SetMessageOptions(modelBuilder);
            SetNewsOptions(modelBuilder);
            SetPaymentsOptions(modelBuilder);
            SetProjectSubscriberOptions(modelBuilder);
            SetRatingOptions(modelBuilder);
            SetTagOptions(modelBuilder);
            SetProjectOptions(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SetCommentOptions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasOne(comment => comment.Project).WithMany(project => project.Comments)
                .HasForeignKey(comment => comment.ProjectId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Comment>().HasOne(comment => comment.UserInfo).WithMany(userInfo => userInfo.Comments)
                .HasForeignKey(comment => comment.UserName).OnDelete(DeleteBehavior.SetNull);
        }

        private void SetFinancialPurposeOptions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FinancialPurpose>().HasOne(purpose => purpose.Project).WithMany(project => project.FinancialPurposes)
                .HasForeignKey(purpose => purpose.ProjectId).OnDelete(DeleteBehavior.Cascade);
        }

        private void SetMessageOptions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().HasOne(message => message.RecipientInfo).WithMany(info => info.Messages)
                .HasForeignKey(message => message.RecipientUserName).OnDelete(DeleteBehavior.Cascade);
        }

        private void SetNewsOptions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>().HasOne(news => news.Project).WithMany(project => project.News)
                .HasForeignKey(news => news.ProjectId).OnDelete(DeleteBehavior.Cascade);
        }

        private void SetPaymentsOptions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>().HasOne(payment => payment.Project).WithMany(project => project.Payments)
                .HasForeignKey(payment => payment.ProjectId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Payment>().HasOne(payment => payment.UserInfo).WithMany(info => info.Payments)
                .HasForeignKey(payment => payment.UserName).OnDelete(DeleteBehavior.SetNull);
        }

        private void SetProjectSubscriberOptions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectSubscriber>().HasKey(subscriber => new { subscriber.UserName, subscriber.ProjectId });
            modelBuilder.Entity<ProjectSubscriber>().HasOne(subscriber => subscriber.Project).WithMany(project => project.Subscribers)
                .HasForeignKey(subscriber => subscriber.ProjectId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProjectSubscriber>().HasOne(subscriber => subscriber.UserInfo).WithMany(info => info.Subscriptions)
                .HasForeignKey(subscriber => subscriber.UserName).OnDelete(DeleteBehavior.Cascade);
        }

        private void SetRatingOptions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>().HasOne(rating => rating.Project).WithMany(project => project.Ratings)
                .HasForeignKey(rating => rating.ProjectId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Rating>().HasOne(rating => rating.UserInfo).WithMany(info => info.Ratings)
                .HasForeignKey(rating => rating.UserName).OnDelete(DeleteBehavior.SetNull);
        }

        private void SetTagOptions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().HasKey(tag => new { tag.Name, tag.ProjectId });
            modelBuilder.Entity<Tag>().HasOne(tag => tag.Project).WithMany(project => project.Tags)
                .HasForeignKey(tag => tag.ProjectId).OnDelete(DeleteBehavior.Cascade);
        }

        private void SetProjectOptions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasOne(project => project.UserInfo).WithMany(info => info.Projects)
                .HasForeignKey(project => project.OwnerUserName).OnDelete(DeleteBehavior.Restrict);
        }

        //private void SetUserInfoOptions(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UserInfo>().HasOne(info => info.ApplicationUser).WithOne(user => user.Info)
        //        .HasForeignKey<UserInfo>(info => info.UserName).HasPrincipalKey<ApplicationUser>(user => user.UserName)
        //        .OnDelete(DeleteBehavior.Cascade);
        //}
    }
}
