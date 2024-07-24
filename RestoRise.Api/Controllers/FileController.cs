using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestoRise.Application.Interfaces.Services;
using RestoRise.Domain.Common;

namespace RestoRise.Api.Controllers;
[ApiController]
[Route("file") ]
[Authorize(AuthenticationSchemes = "Bearer")]
public class FileController:ControllerBase
{
    private readonly IFileService _fileService;
   
    private const string bucketName = "foods";
    private const string serviceURL = "https://object.pscloud.io";
    private const string accessKey = "6H1AP5R8B3S5U1E42RA7";
    private const string secretKey = "wy8gb4VRTxTubejv08KgZBBOIVASzzxdVlkcRQkH";
    
    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile image)
    {
        if (image == null || image.Length == 0)
        {
            return BadRequest("Файл не был загружен.");
        }

        var config = new AmazonS3Config
        {
            ServiceURL = serviceURL
        };

        using var client = new AmazonS3Client(accessKey, secretKey, config);
        
        // Генерируем уникальное имя для файла
        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        string keyName = "foods/" + uniqueFileName;

        try
        {
            using (var newMemoryStream = new MemoryStream())
            {
                await image.CopyToAsync(newMemoryStream);

                var fileTransferUtility = new TransferUtility(client);
                await fileTransferUtility.UploadAsync(newMemoryStream, bucketName, keyName);
            }

            // Генерируем URL для загруженного изображения
            string imageUrl = $"https://foods.object.pscloud.io/{bucketName}/{uniqueFileName}";

            return Ok(new { Url = imageUrl });
        }
        catch (AmazonS3Exception e)
        {
            return StatusCode(500, $"Ошибка Amazon S3: {e.Message}");
        }
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteImage(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return BadRequest("Имя файла не указано.");
        }

        // Имя бакета S3, откуда будет удален файл
        string bucketName = "имя_вашего_бакета";

        // Создание клиента Amazon S3
        var config = new AmazonS3Config
        {
            ServiceURL = "URL_вашего_стороннего_S3_сервиса"
        };
        using var client = new AmazonS3Client("ваш_ключ_доступа", "ваш_секретный_ключ", config);

        // Путь к файлу в S3, который нужно удалить
        string keyName = "путь_к_файлу_в_s3/" + fileName;

        // Попытка удаления файла
        try
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = keyName
            };

            await client.DeleteObjectAsync(deleteRequest);

            return Ok($"Файл {fileName} успешно удален.");
        }
        catch (AmazonS3Exception e)
        {
            return StatusCode(500, $"Ошибка Amazon S3: {e.Message}");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Ошибка: {e.Message}");
        }
    }
}