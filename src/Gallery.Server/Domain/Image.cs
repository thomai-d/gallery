using Microsoft.AspNetCore.StaticFiles;

namespace Gallery.Server.Domain
{
    public static class Image
    {
        public static readonly string[] ValidImageExtensions = [ ".png", ".jpg", ".jpeg" ];
        
        private static FileExtensionContentTypeProvider _contentTypeProvider = new();

        /// <summary>
        /// Returns true if the given image is a supported image.
        /// </summary>
        public static bool IsValidImage(string image)
        {
            var ext = Path.GetExtension(image);
            return ValidImageExtensions.Contains(ext);
        }

        public static string GetMimeType(string extension)
        {
            if (_contentTypeProvider.TryGetContentType(extension, out var contentType))
                return contentType;

            throw new ArgumentException($"Can't determine mime type for '{extension}'");
        }

        public static string BuildUrl(string gallery, string imageName)
        {
            return $"/api/galleries/{gallery}/images/{imageName}";
        }
    }
}
