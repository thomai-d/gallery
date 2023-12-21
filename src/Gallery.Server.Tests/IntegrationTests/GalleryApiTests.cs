using FluentAssertions;
using Gallery.Server.Application.Endpoints.Model;
using System.Net;
using System.Net.Http.Json;

namespace Gallery.Server.Tests.IntegrationTests
{
    public class GalleryApiTests
    {
        [Fact]
        public async Task Valid_Gallery_Should_Return_Ok()
        {
            await using var app = new WebApp();

            var client = app.CreateClient();

            var result = await client.GetAsync("/api/galleries/gal-1");
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var gallery = await result.Content.ReadFromJsonAsync<GalleryDetail>();
            gallery.Should().BeEquivalentTo(new GalleryDetail
            {
                Id = "gal-1",
                ImageUrls = ["/api/galleries/gal-1/images/a.jpg"]
            });
        }

        [Theory]
        [InlineData("..%5C")]
        [InlineData("..%5C..%5Csecret")]
        public async Task Directory_Traversal_Attacks_Should_Be_Prevented(string relPath)
        {
            await using var app = new WebApp();

            var client = app.CreateClient();

            var result = await client.GetAsync("/api/galleries/" + relPath);
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
