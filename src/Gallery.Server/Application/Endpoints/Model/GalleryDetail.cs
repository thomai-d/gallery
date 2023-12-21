namespace Gallery.Server.Application.Endpoints.Model
{
    /// <summary>
    /// Detail information about a gallery.
    /// </summary>
    public class GalleryDetail
    {
        public required string Id { get; init; }

        public required List<string> ImageUrls { get; init; }
    }
}
