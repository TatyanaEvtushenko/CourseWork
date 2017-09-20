using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CourseWork.DataLayer.Data;
using CourseWork.DataLayer.Enums;

namespace CourseWork.DataLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170920083807_TotalTotalRestructuring5")]
    partial class TotalTotalRestructuring5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CourseWork.DataLayer.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.Comment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProjectId");

                    b.Property<string>("Text");

                    b.Property<DateTime>("Time");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserName");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.FinancialPurpose", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<decimal>("NecessaryPaymentAmount");

                    b.Property<string>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("FinancialPurposes");
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.Message", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsSeen");

                    b.Property<string>("RecipientUserName");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("RecipientUserName");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.News", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProjectId");

                    b.Property<string>("Subject");

                    b.Property<string>("Text");

                    b.Property<DateTime>("Time");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.Payment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("PaidAmount");

                    b.Property<string>("ProjectId");

                    b.Property<DateTime>("Time");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserName");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.Project", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountNumber");

                    b.Property<DateTime>("CreatingTime");

                    b.Property<string>("Description");

                    b.Property<DateTime>("FundRaisingEnd");

                    b.Property<string>("ImageUrl");

                    b.Property<decimal>("MaxPayment");

                    b.Property<decimal>("MinPayment");

                    b.Property<string>("Name");

                    b.Property<string>("OwnerUserName");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("OwnerUserName");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.ProjectSubscriber", b =>
                {
                    b.Property<string>("UserName");

                    b.Property<string>("ProjectId");

                    b.HasKey("UserName", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectSubscribers");
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.Rating", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProjectId");

                    b.Property<int>("RatingResult");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserName");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.Tag", b =>
                {
                    b.Property<string>("Name");

                    b.Property<string>("ProjectId");

                    b.HasKey("Name", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.UserInfo", b =>
                {
                    b.Property<string>("UserName");

                    b.Property<string>("About");

                    b.Property<string>("Avatar");

                    b.Property<string>("Contacts");

                    b.Property<string>("Description");

                    b.Property<bool>("IsBlocked");

                    b.Property<string>("LastAccountNumber");

                    b.Property<DateTime>("LastLoginTime");

                    b.Property<string>("Name");

                    b.Property<string>("PassportScan");

                    b.Property<DateTime>("RegistrationTime");

                    b.Property<int>("Status");

                    b.Property<string>("Surname");

                    b.HasKey("UserName");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.Comment", b =>
                {
                    b.HasOne("CourseWork.DataLayer.Models.Project", "Project")
                        .WithMany("Comments")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CourseWork.DataLayer.Models.UserInfo", "UserInfo")
                        .WithMany("Comments")
                        .HasForeignKey("UserName")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.FinancialPurpose", b =>
                {
                    b.HasOne("CourseWork.DataLayer.Models.Project", "Project")
                        .WithMany("FinancialPurposes")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.Message", b =>
                {
                    b.HasOne("CourseWork.DataLayer.Models.UserInfo", "RecipientInfo")
                        .WithMany("Messages")
                        .HasForeignKey("RecipientUserName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.News", b =>
                {
                    b.HasOne("CourseWork.DataLayer.Models.Project", "Project")
                        .WithMany("News")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.Payment", b =>
                {
                    b.HasOne("CourseWork.DataLayer.Models.Project", "Project")
                        .WithMany("Payments")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("CourseWork.DataLayer.Models.UserInfo", "UserInfo")
                        .WithMany("Payments")
                        .HasForeignKey("UserName")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.Project", b =>
                {
                    b.HasOne("CourseWork.DataLayer.Models.UserInfo", "UserInfo")
                        .WithMany("Projects")
                        .HasForeignKey("OwnerUserName");
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.ProjectSubscriber", b =>
                {
                    b.HasOne("CourseWork.DataLayer.Models.Project", "Project")
                        .WithMany("Subscribers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CourseWork.DataLayer.Models.UserInfo", "UserInfo")
                        .WithMany("Subscriptions")
                        .HasForeignKey("UserName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.Rating", b =>
                {
                    b.HasOne("CourseWork.DataLayer.Models.Project", "Project")
                        .WithMany("Ratings")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CourseWork.DataLayer.Models.UserInfo", "UserInfo")
                        .WithMany("Ratings")
                        .HasForeignKey("UserName")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.Tag", b =>
                {
                    b.HasOne("CourseWork.DataLayer.Models.Project", "Project")
                        .WithMany("Tags")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CourseWork.DataLayer.Models.UserInfo", b =>
                {
                    b.HasOne("CourseWork.DataLayer.Models.ApplicationUser", "ApplicationUser")
                        .WithOne("Info")
                        .HasForeignKey("CourseWork.DataLayer.Models.UserInfo", "UserName")
                        .HasPrincipalKey("CourseWork.DataLayer.Models.ApplicationUser", "UserName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CourseWork.DataLayer.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CourseWork.DataLayer.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CourseWork.DataLayer.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
