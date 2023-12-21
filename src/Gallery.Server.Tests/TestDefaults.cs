using Gallery.Server.Options;
using Microsoft.Extensions.Options;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;

namespace Gallery.Server.Tests
{
    /// <summary>
    /// Defaults for automated tests (Mocks, constants, ...)
    /// </summary>
    public static class TestDefaults
    {
        public static IFileSystem GetFileSystem()
        {
            var fs = new MockFileSystem();
            fs.AddFile("/gal/gal-1/a.jpg", new MockFileData("gal-1/a.jpg"));
            fs.AddFile("/gal/gal-2/1.jpg", new MockFileData("gal-1/1.jpg"));
            fs.AddFile("/gal/gal-2/2.jpg", new MockFileData("gal-2/2.jpg"));
            
            // test file to verify that dirs with forbidden chars cannot be found
            fs.AddFile("/gal/gal$/a.jpg", new MockFileData("forbidden"));

            // test file for exclusion of non-images
            fs.AddFile("/gal/gal-2/secret.txt", new MockFileData("secret"));

            // test file for directory traversal attacks
            fs.AddFile("/etc/passwd", new MockFileData("passwd"));
            
            // test file for non-gallery contents
            fs.AddFile("/gal/non-image/secret.txt", new MockFileData("secret"));
            
            // test file for directory traversal attacks
            fs.AddFile("/secret/image.png", new MockFileData("secret"));

            // test dir for empty directories
            fs.AddDirectory("/gal/empty");

            return fs;
        }

        public static IOptions<GalleryOptions> GetGalleryOptions()
        {
            var options = new GalleryOptions();
            ConfigureGalleryOptions(options);
            return new OptionsWrapper<GalleryOptions>(options);
        }

        public static void ConfigureGalleryOptions(GalleryOptions options)
        {
            options.GalleryPath = "/gal";
        }
    }
}
