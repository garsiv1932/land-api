using System.Collections.Generic;
using System.Linq;
using Api.Model;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace Api.Context
{
    public class ApiContext:DbContext
    {
        public DbSet<Web> Db_Webs { get; set; }
        public DbSet<WebArticle> DbArticles { get; set; }
        
        public DbSet<WebArticleComment> Db_Article_Comments { get; set; }

        public DbSet<WebUser> DbWebUser { get; set; }

        public DbSet<WebVisit> Db_Web_Visit { get; set; }

        public DbSet<WebUserRole> DbUserRole { get; set; }
        public DbSet<Logs> DbLogs { get; set; }


        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseSnakeCaseNamingConvention();
        

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //     => optionsBuilder.UseNpgsql("Host=localhost;Database=blog_api;Username=blog_api_admin;Password=13aBr2009");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.ToTable("Log");
                entity.HasKey(e => e.LogId)
                    .HasName("PK_Log");
                entity.HasOne(e => e.User)
                    .WithMany(e => e.Logs)
                    .HasForeignKey(e => e.UserId)
                    .HasPrincipalKey(e => e.Id);
                entity.Property(e => e.UserId)
                    .IsRequired();
                entity.Property(e => e.DateCreated)
                    .IsRequired();
            });

            modelBuilder.Entity<WebUserRole>(entity =>
            {
                entity.Property( e => e.Name )
                    .ValueGeneratedNever();
                entity.HasKey(e => e.Name)
                    .HasName("PK_Blog_User_Role");
                entity.Property(e => e.Name)
                    .IsRequired();
            });

            modelBuilder.Entity<WebVisit>(entity =>
            {
                entity.HasKey(e => new { Ip_Addr = e.IpAddress, Visit_Date = e.VisitDate})
                    .HasName("PK_Blog_Visit");
                entity.Property(e => e.VisitDate)
                    .IsRequired();
                entity.Property(e => e.IpAddress)
                    .IsRequired();
            });

            modelBuilder.Entity<WebUser>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_Blog_User");
                entity.HasOne(e => e.Web)
                    .WithMany(e => e.users)
                    .HasForeignKey(e => e.SiteLink)
                    .HasPrincipalKey(e => e.Site_Link);
                entity.Property(e => e.FirstName)
                    .IsRequired();
                entity.Property(e => e.FirstLastName)
                    .IsRequired();
                entity.Property(e => e.RoleId)
                    .IsRequired();
            });
            
            modelBuilder.Entity<Web>(entity =>
            {
                modelBuilder.Entity<Web>()
                    .Property( e => e.Site_Link )
                    .ValueGeneratedNever();
                entity.ToTable("Blogs");
                entity.HasKey(e => e.Site_Link)
                    .HasName("PK_Blog");
                entity.Property(e => e.Site_Link)
                    .IsRequired()
                    .HasMaxLength(200);;
                entity.HasIndex(e => e.Site_Link)
                    .IsUnique();
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(b => b.Secret)
                    .HasMaxLength(150);
            });
        
            modelBuilder.Entity<WebArticle>(entity =>
            {
                modelBuilder.Entity<WebArticle>()
                    .Property( e =>e.ArticleLink)
                    .ValueGeneratedNever();
                entity.HasOne(p => p.User)
                    .WithMany(b => b.Articles)
                    .HasForeignKey(p => p.UserEmail)
                    .HasPrincipalKey(b => b.Email);
                entity.ToTable("Blog_Articles");
                entity.HasKey(e => e.ArticleLink)
                    .HasName("PK_Blog_Article");
                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.UserEmail)
                    .IsRequired();
                entity.Property(e => e.ArticleLink)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.HasIndex(e => e.ArticleLink)
                    .IsUnique();
                entity.Property(ba => ba.Tittle)
                    .IsRequired();
            });
        
            modelBuilder.Entity<WebArticleComment>(entity =>
            {
                entity.ToTable("Blog_Article_Comments");
                entity.HasKey(e => new { User_Email = e.UserEmail, e.Published})
                    .HasName("PK_Blog_Article_Comment");
                entity.HasOne(p => p.User)
                    .WithMany(b => b.Comments)
                    .HasForeignKey(p => p.UserEmail)
                    .HasPrincipalKey(b => b.Email);
                entity.Property(e => e.Comment)
                    .IsRequired();
                entity.Property(e => e.IpAddress)
                    .IsRequired();
                entity.Property(e => e.Published)
                    .IsRequired();
                entity.Property(e => e.IpAddress)
                    .HasMaxLength(15);
            });
            
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var currentTableName = modelBuilder.Entity(entity.Name).Metadata.GetDefaultTableName();
                modelBuilder.Entity(entity.Name).ToTable(currentTableName.ToLower());
            }
            
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Use the entity name instead of the Context.DbSet<T> name
                // refs https://docs.microsoft.com/en-us/ef/core/modeling/entity-types?tabs=fluent-api#table-name
        
                IEnumerable<IMutableProperty> propeties = entityType.GetProperties();
                foreach (var VARIABLE in propeties)
                {
                    string typename = VARIABLE.ClrType.Name;
                    if (typename == "String" )
                    {
                        VARIABLE.SetColumnType("varchar");
                    }
                }
            }
            
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e=>e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}