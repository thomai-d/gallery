using Gallery.Server.Options;
using Gallery.Server.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.IO.Abstractions;

namespace Gallery.Server.Tests.IntegrationTests;

/// <summary>
/// WebApp Host for Integration tests with virtual file system.
/// </summary>
internal class WebApp : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var fs = TestDefaults.GetFileSystem();

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<IFileSystem>();
            services.AddSingleton(fs);

            services.Configure<GalleryOptions>(TestDefaults.ConfigureGalleryOptions);
        });
    }
}
