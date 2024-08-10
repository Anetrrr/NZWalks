using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase

    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }


        //GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regions


        [HttpGet]
        public IActionResult GetAll()
        {
            //Get Data from Database - Domain Models.
           var regionsDomain = dbContext.Regions.ToList();

            //Map Domain Models to DTOs - we return DTOs back to the client, not the Domain Models
            var regionsDto = new List<RegionDTO>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDTO()

                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            // Return DTOs
            return Ok(regionsDto);
        }

        //GET SINGLE REGION
        // GET: https://localhost:portnumber/api/regions/{id}

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // Find method: used for searching using only id
            //  var region = dbContext.Regions.Find(id);


            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            

            if(regionDomain == null)
            {
                return NotFound();
            }


            var regionDto = new RegionDTO
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);

        }

    }
}
