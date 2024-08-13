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
                    Id = Guid.Parse("176f2d2e-b451-4765-a0ad-ee1136a8d6c9"),
                    Name = "Easy"

                },
                new Difficulty()
                {
                    Id = Guid.Parse("334d59cc-fe13-4d37-87b4-20888e9f7d59"),
                    Name = "Medium"

                },
                new Difficulty()
                {
                    Id = Guid.Parse("a615a068-f66c-42f5-ad73-579e59acb867"),
                    Name = "Hard"

                }


            };
            //seed difficulties  to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);




            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("d2e50bf4-b711-4740-9621-e15eb0de3096"),
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
                }
            };
            //seed difficulties  to the database
            modelBuilder.Entity<Region>().HasData(regions);


        }
    }
}
