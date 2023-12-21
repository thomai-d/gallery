using System.ComponentModel.DataAnnotations;

namespace Gallery.Server.Options
{
    public class GalleryOptions
    {
        /// <summary>
        /// Gallery base path.
        /// </summary>
        [Required, MinLength(3)]
        public string GalleryPath { get; set; } = string.Empty;
    }
}
