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
        public DbSet<Web_Article> Db_Articles { get; set; }
        
        public DbSet<Web_Article_Comment> Db_Article_Comments { get; set; }

        public DbSet<Web_User> Db_Web_User { get; set; }

        public DbSet<Web_Visit> Db_Web_Visit { get; set; }

        public DbSet<Web_User_Role> Db_Blog_User_Role { get; set; }

       
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

            modelBuilder.Entity<Web_User_Role>(entity =>
            {
                entity.Property( e => e.Name )
                    .ValueGeneratedNever();
                
                entity.HasKey(e => e.Name)
                    .HasName("PK_Blog_User_Role");
                
                entity.Property(e => e.Name)
                    .IsRequired();
            });

            modelBuilder.Entity<Web_Visit>(entity =>
            {
                entity.HasKey(e => new {e.Ip_Addr, e.Visit_Date})
                    .HasName("PK_Blog_Visit");
                
                entity.Property(e => e.Visit_Date)
                    .IsRequired();
                entity.Property(e => e.Ip_Addr)
                    .IsRequired();
            });

            modelBuilder.Entity<Web_User>(entity =>
            {
                // modelBuilder.Entity<Web_User>()
                //     .Property( e => e.Email)
                //     .ValueGeneratedNever();
                
                entity.HasKey(e => e.Email)
                    .HasName("PK_Blog_User");
                
                entity.HasOne(e => e.Web)
                    .WithMany(e => e.users)
                    .HasForeignKey(e => e.Site_Link)
                    .HasPrincipalKey(e => e.Site_Link);
                
                entity.Property(e => e.First_Name)
                    .IsRequired();
                
                entity.Property(e => e.First_Last_Name)
                    .IsRequired();
                
                entity.Property(e => e.Blog_User_Role_Id)
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
        
            modelBuilder.Entity<Web_Article>(entity =>
            {
                modelBuilder.Entity<Web_Article>()
                    .Property( e =>e.Article_Link)
                    .ValueGeneratedNever();
                
                entity.HasOne(p => p.User)
                    .WithMany(b => b.Articles)
                    .HasForeignKey(p => p.Blog_User_Email)
                    .HasPrincipalKey(b => b.Email);

                entity.ToTable("Blog_Articles");
                entity.HasKey(e => e.Article_Link)
                    .HasName("PK_Blog_Article");
                
                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(e => e.Blog_User_Email)
                    .IsRequired();
                
                entity.Property(e => e.Article_Link)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasIndex(e => e.Article_Link)
                    .IsUnique();
                
                entity.Property(ba => ba.Tittle)
                    .IsRequired();
            });
        
            modelBuilder.Entity<Web_Article_Comment>(entity =>
            {
                entity.ToTable("Blog_Article_Comments");
                entity.HasKey(e => new {e.User_Email, e.Published})
                    .HasName("PK_Blog_Article_Comment");
                
                entity.HasOne(p => p.User)
                    .WithMany(b => b.Comments)
                    .HasForeignKey(p => p.User_Email)
                    .HasPrincipalKey(b => b.Email);

                entity.Property(e => e.Comment)
                    .IsRequired();
                entity.Property(e => e.Ip_Address)
                    .IsRequired();
                entity.Property(e => e.Published)
                    .IsRequired();
                entity.Property(e => e.Ip_Address)
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