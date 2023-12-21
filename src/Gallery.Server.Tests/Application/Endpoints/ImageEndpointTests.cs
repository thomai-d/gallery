using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Gallery.Server.Application.Endpoints;
using NSubstitute;
using Microsoft.AspNetCore.Http;

namespace Gallery.Server.Tests.Application.Endpoints
{
    public class ImageEndpointTests
    {
        [Fact]
        public void Get_Image_Should_Provide_404_If_Not_Found()
        {
            var fs = TestDefaults.GetFileSystem();
            var opt = TestDefaults.GetGalleryOptions();
            var http = Substitute.For<HttpContext>();

            var result = ImageEndpoint.GetImage(http, "gal-1", "nonexisting.jpg", fs, opt);

            result.Result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void Directory_Traversal_Should_Not_Be_Possible()
        {
            var fs = TestDefaults.GetFileSystem();
            var opt = TestDefaults.GetGalleryOptions();
            var http = Substitute.For<HttpContext>();

            var result = ImageEndpoint.GetImage(http, "gal-1", "../../secret/image.png", fs, opt);

            result.Result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void Get_Image_Should_Provide_A_Correct_MimeType()
        {
            var fs = TestDefaults.GetFileSystem();
            var opt = TestDefaults.GetGalleryOptions();
            var http = Substitute.For<HttpContext>();

            var result = ImageEndpoint.GetImage(http, "gal-1", "a.jpg", fs, opt);

            result.Result.Should().BeOfType<PhysicalFileHttpResult>();

            var data = (PhysicalFileHttpResult)result.Result;
            data.ContentType.Should().Be("image/jpeg");
        }
    }
}
