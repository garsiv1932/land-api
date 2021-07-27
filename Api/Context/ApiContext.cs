using System.Collections.Generic;
using System.Linq;
using Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace Api.Context
{
    public class ApiContext:DbContext
    {
        public DbSet<Blog> Db_Blogs { get; set; }
        public DbSet<Blog_Article> Db_Articles { get; set; }
        
        public DbSet<Blog_Article_Comment> Db_Article_Comments { get; set; }

        public DbSet<Blog_User> Db_Blog_User { get; set; }

        public DbSet<Blog_Visit> Db_Blog_Visit { get; set; }

        public DbSet<Blog_User_Role> Db_Blog_User_Role { get; set; }
        
        

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

            modelBuilder.Entity<Blog_User_Role>(entity =>
            {
                modelBuilder.Entity<Blog_User_Role>()
                    .Property( e => e.Blog_User_Role_Id )
                    .ValueGeneratedNever();
                
                entity.HasKey(e => e.Blog_User_Role_Id)
                    .HasName("PK_Blog_User_Role");
                
                entity.Property(e => e.Name)
                    .IsRequired();
            });

            modelBuilder.Entity<Blog_Visit>(entity =>
            {
                modelBuilder.Entity<Blog_Visit>()
                    .Property( e => e.Blog_Visit_ID )
                    .ValueGeneratedNever();
                
                entity.HasKey(e => e.Blog_Visit_ID)
                    .HasName("PK_Blog_Visit");
                
                entity.Property(e => e.Visit_Date)
                    .IsRequired();
                entity.Property(e => e.Ip_Addr)
                    .IsRequired();
            });

            modelBuilder.Entity<Blog_User>(entity =>
            {
                modelBuilder.Entity<Blog_User>()
                    .Property( e => e.Blog_User_ID)
                    .ValueGeneratedNever();
                
                entity.HasKey(e => e.Blog_User_ID)
                    .HasName("PK_Blog_User");
                
                entity.Property(e => e.Email)
                    .IsRequired();
                entity.HasIndex(e => e.Email)
                    .IsUnique();
                
                entity.Property(e => e.First_Name)
                    .IsRequired();
                entity.Property(e => e.First_Last_Name)
                    .IsRequired();
                entity.Property(e => e.Blog_User_Role_Id)
                    .IsRequired();
            });
            
            modelBuilder.Entity<Blog>(entity =>
            {
                modelBuilder.Entity<Blog>()
                    .Property( e => e.Blog_Id )
                    .ValueGeneratedNever();

                entity.ToTable("Blogs");
                entity.HasKey(e => e.Blog_Id)
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
                    .HasMaxLength(100);
            });
        
            modelBuilder.Entity<Blog_Article>(entity =>
            {
                modelBuilder.Entity<Blog_Article>()
                    .Property( e =>e.Blog_Article_Id)
                    .ValueGeneratedNever();
                
                entity.ToTable("Blog_Articles");
                entity.HasKey(e => e.Blog_Article_Id)
                    .HasName("PK_Blog_Article");
                
                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(e => e.Blog_User_ID)
                    .IsRequired();
                
                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasIndex(e => e.Link)
                    .IsUnique();
                
                entity.Property(ba => ba.Tittle)
                    .IsRequired();
            });
        
            modelBuilder.Entity<Blog_Article_Comment>(entity =>
            {
                entity.ToTable("Blog_Article_Comments");
                entity.HasKey(e => e.Blog_Article_Comment_Id)
                    .HasName("PK_Blog_Article_Comment");
                entity.Property(e => e.Article_Id)
                    .IsRequired();
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