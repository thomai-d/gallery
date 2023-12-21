using Gallery.Server.Application.Endpoints.Model;
using Gallery.Server.Domain;
using Gallery.Server.Options;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using System.Buffers;
using System.IO.Abstractions;
using System.Security;

namespace Gallery.Server.Application.Endpoints
{
    /// <summary>
    /// Endpoint to query information about a single gallery.
    /// </summary>
    public static class GalleryEndpoint
    {
        public static Results<Ok<GalleryDetail>, NotFound> GetGallery(
            string safeGalleryId,
            IFileSystem fileSystem,
            IOptions<GalleryOptions> options)
        {
            var fullPath = fileSystem.Path.Combine(options.Value.GalleryPath, safeGalleryId);

            if (!fileSystem.Directory.Exists(fullPath))
                return TypedResults.NotFound();

            var gallery = new GalleryDetail
            {
                Id = safeGalleryId,
                ImageUrls = fileSystem.Directory
                            .EnumerateFiles(fullPath)
                            .Where(Image.IsValidImage)
                            .OrderBy(i => i)
                            .Select(f => Image.BuildUrl(safeGalleryId, fileSystem.Path.GetFileName(f)))
                            .ToList()
            };

            return TypedResults.Ok(gallery);
        }
    }
}
