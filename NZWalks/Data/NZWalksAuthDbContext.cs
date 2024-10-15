using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {

        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "f62ad003-7fcc-4bb2-aaad-3851bdc06b59";
            var writerRoleId = "e038f3d2-0a5f-46c9-90d7-f5eaec244e3d";


            var roles = new List<IdentityRole>

            { new IdentityRole

            {
                Id = readerRoleId,
                ConcurrencyStamp = readerRoleId,
                Name = "Reader",
                NormalizedName = "Reader".ToUpper()

            },

                new IdentityRole

                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()

                },


            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}