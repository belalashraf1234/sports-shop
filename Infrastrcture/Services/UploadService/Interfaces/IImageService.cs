
using Microsoft.AspNetCore.Http;

namespace Infrastrcture.Services.UploadService.Interfaces;

public interface IImageService
{
    Task<List<string>> UploadImagesAsync(int id,IFormFileCollection   images);
}