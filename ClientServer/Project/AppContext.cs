using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace Project
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Camera> Cameras { get; set; } = null!;

        public DbSet<Snapshot> Snapshots { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .Build();
            optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseLazyLoadingProxies().UseSqlite(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Camera>()
                .HasMany(c => c.Snapshots);

            modelBuilder.Entity<Snapshot>()
                .HasOne(s => s.Camera)
                .WithMany(c => c.Snapshots)
                .HasForeignKey(s => s.CameraId);
        }

        public void SeedData()
        {
            if (!Cameras.Any() && !Snapshots.Any())
            {
                Camera camera1 = new Camera { Address = "rtsp://100.200.0.1", CreatedAt = DateTime.Now };
                Camera camera2 = new Camera { Address = "rtsp://192.168.0.10", CreatedAt = DateTime.Now };

                Cameras.Add(camera1);
                Cameras.Add(camera2);

                Snapshot ss1 = new Snapshot { CameraId = 1, Label = 0, CreatedAt = DateTime.Now };
                Snapshot ss2 = new Snapshot { CameraId = 2, Label = 1, CreatedAt = DateTime.Now };
                Snapshot ss3 = new Snapshot { CameraId = 1, Label = 3, CreatedAt = DateTime.Now };
                Snapshot ss4 = new Snapshot { CameraId = 2, Label = 2, CreatedAt = DateTime.Now };

                Snapshots.Add(ss1);
                Snapshots.Add(ss2);
                Snapshots.Add(ss3);
                Snapshots.Add(ss4);

                SaveChanges();
            }
        }
    }
}
