using Gallery.Server.Application.Endpoints.Model;
using Gallery.Server.Domain;
using Gallery.Server.Options;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using System.IO.Abstractions;

namespace Gallery.Server.Application.Endpoints
{
    /// <summary>
    /// Endpoint to query all existing galleries.
    /// </summary>
    public static class GalleriesEndpoint
    {
        public static Results<Ok<List<GallerySummary>>, NotFound> GetGalleries(
            IFileSystem fileSystem,
            IOptions<GalleryOptions> options)
        {
            var dirs = fileSystem.Directory.EnumerateDirectories(options.Value.GalleryPath)
                        .Where(dir => Id.IsValid(fileSystem.Path.GetFileName(dir)))
                        .Select(dir => GetGalleryInfo(fileSystem, dir))
                        .Where(gal => gal.ImageCount >= 1)
                        .ToList();

            return TypedResults.Ok(dirs);
        }

        private static GallerySummary GetGalleryInfo(IFileSystem fileSystem, string path)
        {
            var galleryName = Path.GetFileName(path);

            var files = fileSystem.Directory.EnumerateFiles(path)
                            .Select(file => fileSystem.Path.GetFileName(file))
                            .Where(file => Image.IsValidImage(file))
                            .ToArray();

            return new GallerySummary
            {
                Id = galleryName,
                ImageCount = files.Length,
                PreviewImageUrl = Image.BuildUrl(galleryName, files.FirstOrDefault() ?? string.Empty)
            };
        }
    }
}
