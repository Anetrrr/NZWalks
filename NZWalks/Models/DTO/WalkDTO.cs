using NZWalks.Models.Domain;

namespace NZWalks.Models.DTO
{
    public class WalkDTO
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        //we can get this information from the navigation property section below:
        //public Guid DifficultyId { get; set; }
       // public Guid RegionId { get; set; }

        //Navigation property

        public DifficultyDTO Difficulty { get; set; }
        public RegionDTO Region { get; set; }
    }
}
