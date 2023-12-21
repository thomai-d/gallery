using FluentAssertions;
using Gallery.Server.Application.Endpoints;
using Gallery.Server.Application.Endpoints.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gallery.Server.Tests.Application.Endpoints
{
    public class GalleriesEndpointTests
    {
        [Fact]
        public void Valid_galleries_should_be_returned()
        {
            var fs = TestDefaults.GetFileSystem();
            var opt = TestDefaults.GetGalleryOptions();

            var result = GalleriesEndpoint.GetGalleries(fs, opt);

            result.Result.Should().BeOfType<Ok<List<GallerySummary>>>();

            var data = (Ok<List<GallerySummary>>)result.Result;
            var galleries = data.Value!.Select(g => g.Id);

            galleries.Should().Contain(["gal-1", "gal-2"]);
        }

        [Fact]
        public void Correct_Image_Count_Should_Be_Returned()
        {
            var fs = TestDefaults.GetFileSystem();
            var opt = TestDefaults.GetGalleryOptions();

            var result = GalleriesEndpoint.GetGalleries(fs, opt);

            result.Result.Should().BeOfType<Ok<List<GallerySummary>>>();

            var data = (Ok<List<GallerySummary>>)result.Result;
            var gallery = data.Value!.Single(i => i.Id == "gal-2");

            gallery.ImageCount.Should().Be(2, "there are two images and a hidden file that should not count");
        }

        [Fact]
        public void Empty_Galleries_Should_Not_Be_Returned()
        {
            var fs = TestDefaults.GetFileSystem();
            var opt = TestDefaults.GetGalleryOptions();

            var result = GalleriesEndpoint.GetGalleries(fs, opt);

            result.Result.Should().BeOfType<Ok<List<GallerySummary>>>();

            var data = (Ok<List<GallerySummary>>)result.Result;
            var galleries = data.Value!.Select(g => g.Id);

            galleries.Should().NotContain(["empty", "non-image"]);
        }
    }
}
