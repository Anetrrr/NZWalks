using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAll()
        {
            //Get Data from Database - Domain Models.
            var regionsDomain =  await dbContext.Regions.ToListAsync();

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
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Find method: used for searching using only id
            //  var region = dbContext.Regions.Find(id);


            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);


            if (regionDomain == null)
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

        //Post to Create New region
        //POST: https://localhost:portnumber/api/regions

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            // Map or convert dto to domain model

            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl

            };

            await dbContext.Regions.AddAsync(regionDomainModel);
            dbContext.SaveChanges();

            //Map domain model back to dto
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };


            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }

        //Update Region
        //PUT:https://localhost:portnumber/api/regions/{id}

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();

            }

            regionDomain.Name = updateRegionRequestDto.Name;
            regionDomain.Code = updateRegionRequestDto.Code;
            regionDomain.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            //mapping back to dto
            var regionDto = new RegionDTO
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl

            };

            return Ok(regionDto);

        }

        //Deleting a region

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();

            }
            //remove method is still synchronous, no await for Remove.
            dbContext.Regions.Remove(regionDomain);
            await dbContext.SaveChangesAsync();

            return Ok("Successfully deleted");
        }
    }

}
