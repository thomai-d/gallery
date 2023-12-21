using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Gallery.Server.Application.Endpoints;
using Gallery.Server.Application.Endpoints.Model;

namespace Gallery.Server.Tests.Application.Endpoints
{
    public class GalleryEndpointTests
    {
        [Fact]
        public void Get_Single_Gallery_Should_Return_All_Images()
        {
            var fs = TestDefaults.GetFileSystem();
            var opt = TestDefaults.GetGalleryOptions();

            var result = GalleryEndpoint.GetGallery("gal-2", fs, opt);

            result.Result.Should().BeOfType<Ok<GalleryDetail>>();

            var data = (Ok<GalleryDetail>)result.Result;
            data.Value.Should().BeEquivalentTo(
                new GalleryDetail
                {
                    Id = "gal-2",
                    ImageUrls =
                    [
                        "/api/galleries/gal-2/images/1.jpg",
                        "/api/galleries/gal-2/images/2.jpg"
                    ]
                });
        }

        [Fact]
        public void Get_Nonexisting_Gallery_Should_Return_404()
        {
            var fs = TestDefaults.GetFileSystem();
            var opt = TestDefaults.GetGalleryOptions();

            var result = GalleryEndpoint.GetGallery("404", fs, opt);

            result.Result.Should().BeOfType<NotFound>();
        }
    }
}
