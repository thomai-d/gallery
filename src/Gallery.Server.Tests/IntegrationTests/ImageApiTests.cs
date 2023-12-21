using FluentAssertions;
using System.Net;

namespace Gallery.Server.Tests.IntegrationTests
{
    public class ImageApiTests
    {
        [Fact]
        public async Task Image_Endpoint_Should_Be_Wired_Up()
        {
            // Note: using PhysicalFile for implementation.
            // This cannot be mocked. But we can check that the result is 500 (and not 404).

            await using var app = new WebApp();

            var client = app.CreateClient();

            var result = await client.GetAsync("/api/galleries/gal-1/images/a.jpg");
            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
        
        [Fact]
        public async Task Image_Endpoint_Should_Return_404_For_Non_Images()
        {
            await using var app = new WebApp();

            var client = app.CreateClient();

            var result = await client.GetAsync("/api/galleries/gal-2/images/secret.txt");
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async Task Image_Endpoint_Should_Return_404_For_Images_That_Are_Not_Present()
        {
            await using var app = new WebApp();

            var client = app.CreateClient();

            var result = await client.GetAsync("/api/galleries/gal-1/images/nonexistant.jpg");
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async Task Invalid_Request_Should_Return_404()
        {
            await using var app = new WebApp();

            var client = app.CreateClient();

            var result = await client.GetAsync("/api/galleries/gal-1/images/*.jpg");
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async Task Directory_Traversal_Attacks_Should_Not_Be_Possible()
        {
            await using var app = new WebApp();

            var client = app.CreateClient();

            var result = await client.GetAsync("/api/galleries/gal-1/images/..%5C..%5Csecret%5Cimage.png");
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
