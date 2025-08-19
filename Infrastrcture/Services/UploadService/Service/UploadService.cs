using Infrastrcture.Services.UploadService.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastrcture.Services.UploadService.Service;

public class UploadService:IImageService
{
    private readonly string _basePath;
    public UploadService()
    {
        _basePath = Path.Combine(Directory.GetCurrentDirectory(), "Storage", "images");
        if(!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }
    }
    public async Task<List<string>> UploadImagesAsync(int id,IFormFileCollection? images)
    {
        var files = new List<string>();
        try
        {
           

            string productDirectory = GetProductDirectory(id);
            if (!Directory.Exists(productDirectory))
            {
                Directory.CreateDirectory(productDirectory);
                
            }

            foreach (IFormFile image in images)
            { 
                if (image.Length > 0&&IsValidImage(image))
                { 

                    string fileName = GenerateFileName(image.FileName);
                    string filePath = Path.Combine(productDirectory, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                 files.Add(fileName);
                }
            }
            return files;
        }
        catch (Exception e)
        {
            return files;
        }
        
    }

    private string GetProductDirectory(int id)
    {
        return Path.Combine(_basePath, id.ToString());
    }
    private string GenerateFileName(string fileName)
    {
        return $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
    }

    private bool IsValidImage(IFormFile file)
    {
        var allowedExtensions = new[] { ".jpg",".jpeg", ".png", ".gif"};
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(extension))
        {
            return false;
        }

        if (file.Length>10*1024*1024)
        {
            return false;
        }

        if (!file.ContentType.StartsWith("image/"))
            return false;
        return true;
    }


   
}