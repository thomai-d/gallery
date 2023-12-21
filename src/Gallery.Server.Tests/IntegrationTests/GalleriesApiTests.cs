using FluentAssertions;
using Gallery.Server.Application.Endpoints.Model;
using System.Net.Http.Json;

namespace Gallery.Server.Tests.IntegrationTests
{
    public class GalleriesApiTests
    {
        [Fact]
        public async Task Ensure_Galleries_Are_Returned_Correctly()
        {
            await using var app = new WebApp();

            var client = app.CreateClient();

            var result = await client.GetFromJsonAsync<List<GallerySummary>>("/api/galleries");
            result.Should().BeEquivalentTo(new List<GallerySummary>
            {
                new() {
                    Id = "gal-1",
                    ImageCount = 1,
                    PreviewImageUrl = "/api/galleries/gal-1/images/a.jpg"
                },
                new() {
                    Id = "gal-2",
                    ImageCount = 2,
                    PreviewImageUrl = "/api/galleries/gal-2/images/1.jpg"
                }
            });
        }
        
        [Fact]
        public async Task Ensure_Galleries_Do_Not_Return_Galleries_With_Forbidden_Chars()
        {
            await using var app = new WebApp();

            var client = app.CreateClient();

            var result = await client.GetFromJsonAsync<List<GallerySummary>>("/api/galleries");
            result.Should().NotContain(g => g.Id == "gal$");
        }
    }
}
