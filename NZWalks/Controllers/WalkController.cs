using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.CustomActionFilters;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        public WalkController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        //CREATE walk
        //POST: /api/walks

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
           
            
                //map DTO to Domain model

                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                await walkRepository.CreateAsync(walkDomainModel);

                //map Domain Model back to DTO

                var walkDto = mapper.Map<WalkDTO>(walkDomainModel);

                return Ok(walkDto);
            
          
        }

        //GET Walks
        // GET: /api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var walksDomainModel = await walkRepository.GetAllAsync();

            //Map Domain odel to DTO


            var walkDto = mapper.Map<List<WalkDTO>>(walksDomainModel);


            //create  an exception

            throw new Exception("This is a new exception");

            return Ok(walkDto);
        }

        //GET Walk by Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if(walkDomainModel == null)
            {
                return NotFound();
            }

            var walkDto = mapper.Map<WalkDTO>(walkDomainModel);

            return Ok(walkDto);


        }

        //PUT - update by Id
        [HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)

        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            var walkDto = mapper.Map<WalkDTO>(walkDomainModel);

            return Ok(walkDto);
        }

        //DELETE 
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteByIdAsync(id);

            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }
            //map domain to dto

            var deletedWalkDto = mapper.Map<WalkDTO>(deletedWalkDomainModel);

            return Ok(deletedWalkDto);
        }
    }
}
;