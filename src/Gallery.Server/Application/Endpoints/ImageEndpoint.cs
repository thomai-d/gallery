using Gallery.Server.Domain;
using Gallery.Server.Options;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using System.IO.Abstractions;

namespace Gallery.Server.Application.Endpoints
{
    /// <summary>
    /// Endpoint for image retrieval.
    /// </summary>
    public static class ImageEndpoint
    {
        public static Results<PhysicalFileHttpResult, NotFound> GetImage(
            HttpContext http,
            string safeGalleryId,
            string imageName,
            IFileSystem fs,
            IOptions<GalleryOptions> options)
        {
            var safeImageName = fs.Path.GetFileName(imageName);

            if (!Image.IsValidImage(safeImageName))
                return TypedResults.NotFound();

            var path = Path.Combine(
                options.Value.GalleryPath,
                safeGalleryId,
                safeImageName);

            if (!fs.File.Exists(path))
                return TypedResults.NotFound();

            http.Response.Headers.CacheControl = $"public,max-age={TimeSpan.FromHours(24).TotalSeconds}";

            var mimeType = Image.GetMimeType(path);
            return TypedResults.PhysicalFile(path, mimeType);
        }
    }
}
