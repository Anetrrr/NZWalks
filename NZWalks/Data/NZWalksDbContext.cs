using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domain;
using System.Xml.Linq;

namespace NZWalks.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for difficulties: Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("2d77df5b-a611-490a-a73e-f4ffaecdc076"),
                    Name = "Easy"

                },
                new Difficulty()
                {
                    Id = Guid.Parse("23d2c2ed-5f5f-406b-babf-aff01be60e17"),
                    Name = "Medium"

                },
                new Difficulty()
                {
                    Id = Guid.Parse("f4d29e5e-c84f-4aaf-92cf-3a7f072eaead"),
                    Name = "Hard"

                }


            };
            //seed difficulties  to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);




            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("   "),
                    Name = "Adamanton",
                    Code = "ADM",
                    RegionImageUrl = "adm.nz"

                },
                 new Region()
                {
                    Id = Guid.Parse("1bdbcb9e-c9fc-4744-8af3-7af2c9052cd5"),
                    Name = "Felix Highway",
                    Code = "FLX",
                    RegionImageUrl = "felix.nz"

                },
                  new Region()
                {
                    Id = Guid.Parse("4ad9a282-fbd4-48d5-993a-da7e9e0a500f"),
                    Name = "Rosantine Hills",
                    Code = "RST",
                    RegionImageUrl = "rosant.nz"
                },
                   new Region()
                {
                    Id = Guid.Parse("3cf0769a-7b41-4cd9-b9f7-203f8fd84aec"),
                    Name = "Hollow Valleys",
                    Code = "HVAL",
                    RegionImageUrl = "scary1.img"
                },
                    new Region()
                {
                    Id = Guid.Parse("608c6f34-453c-4891-b332-1499808438f9"),
                    Name = "Icy Mountain hills",
                    Code = "IMH",
                    RegionImageUrl = "icecaps.img"
                }
            };
            //seed difficulties  to the database
            modelBuilder.Entity<Region>().HasData(regions);


        }
    }
}
