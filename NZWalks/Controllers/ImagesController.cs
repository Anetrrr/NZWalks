using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {

            //try
            //{ // Ensure the request object is not null
                //if (request == null)
                //{
                //    ModelState.AddModelError("Request", "The request is null.");

                //    return BadRequest(ModelState);
                //}

                ValidateFileUpload(request);

                //if (request.File == null || request.File.Length == 0)

                //{
                //    ModelState.AddModelError("File",
                //    "The file is required and cannot be empty.");

                //    return BadRequest(ModelState);
                //}


                if (ModelState.IsValid)

                {
                    // convert DTO to domain model

                    var imageDomainModel = new Image
                    {
                        File = request.File,
                        FileExtension = Path.GetExtension(request.File.FileName),
                        FileName = request.FileName,
                       FileSizeInBytes = request.File.Length,
                       FileDescription = request.FileDescription,
                    };
                
                    //if (imageRepository == null)
                    //{
                    //    ModelState.AddModelError("Repository",
                    //    "The image repository is not available.");

                    //    return StatusCode(500, "Internal server error here.");
                    //}
                    await _imageRepository.Upload(imageDomainModel);

                    return Ok(imageDomainModel);
                }

                return BadRequest(ModelState);

            
            //catch (Exception ex)
            //{

            //    return StatusCode(500, "Internal server error at the end.");

            //}
        }

            private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".PNG" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");

            }

            if(request.File.Length > 10485760) 
            {
                ModelState.AddModelError("file", "File is larger that 10MB. " +
                    "Upload a smaller file.");
            }
        }

    }
}