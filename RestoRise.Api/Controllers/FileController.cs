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
   
    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile image)
    {
        if (image == null || image.Length == 0)
        {
            return BadRequest("Файл не был загружен.");
        }
        string bucketName = "foods";
        var config = new AmazonS3Config
        {
            ServiceURL = "https://object.pscloud.io"
        };
        using var client = new AmazonS3Client("6H1AP5R8B3S5U1E42RA7", "wy8gb4VRTxTubejv08KgZBBOIVASzzxdVlkcRQkH", config);
        string keyName = "foods/" + image.FileName;
        try
        {
            using (var newMemoryStream = new MemoryStream())
            {
                await image.CopyToAsync(newMemoryStream);
                var fileTransferUtility = new TransferUtility(client);
                await fileTransferUtility.UploadAsync(newMemoryStream, bucketName, keyName);
            }
            return Ok("Файл успешно загружен.");
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