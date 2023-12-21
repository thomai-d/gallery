using Gallery.Server.Application.Endpoints;
using Gallery.Server.Application.Routing;
using Gallery.Server.Options;
using System.IO.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RouteOptions>(opt =>
{
    opt.ConstraintMap.Add("id", typeof(IdRouteConstraint));
});

builder.Services.AddOptions<GalleryOptions>()
                .BindConfiguration(nameof(GalleryOptions))
                .ValidateDataAnnotations()
                .ValidateOnStart();

builder.Services.AddSingleton<IFileSystem, FileSystem>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/galleries", GalleriesEndpoint.GetGalleries)
   .WithName("Get Galleries")
   .WithOpenApi();

app.MapGet("/api/galleries/{safeGalleryId:id}", GalleryEndpoint.GetGallery)
   .WithName("Get Gallery")
   .WithOpenApi();

app.MapGet("/api/galleries/{safeGalleryId:id}/images/{imageName}", ImageEndpoint.GetImage)
   .WithName("Get Image")
   .WithOpenApi();

app.MapGet("/api/test", () => "OK");

app.MapFallbackToFile("/index.html");

app.UseStaticFiles();

app.Run();
