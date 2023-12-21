namespace Gallery.Server.Application.Endpoints.Model
{
    /// <summary>
    /// Summarized information for a single gallery.
    /// </summary>
    public record GallerySummary
    {
        public required string Id { get; init; }

        public required int ImageCount { get; init; }

        public required string PreviewImageUrl { get; init; }
    }
}
