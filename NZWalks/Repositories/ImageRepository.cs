using Microsoft.AspNetCore.Http;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NZWalksDbContext _dbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor, NZWalksDbContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;

        }
        public async Task<Image> Upload(Image image)
        {
            
                // Define the local file path
                var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, 
                    "Image", $"{image.FileName}{image.FileExtension}");

                // Upload the image to the local path
                using (var stream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write))
                {
                    await image.File.CopyToAsync(stream);
                }

            // Define the URL file path
                var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}" +
                    $"{_httpContextAccessor.HttpContext.Request.PathBase}/Image/{image.FileName}{image.FileExtension}";

                // Set the file path in the image object
                image.FilePath = urlFilePath;

                // Add the image to the database context
                await _dbContext.Images.AddAsync(image);
                await _dbContext.SaveChangesAsync();

                return image;
            }
           
        }

    }

