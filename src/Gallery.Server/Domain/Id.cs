using System.Buffers;

namespace Gallery.Server.Domain
{
    public class Id
    {
        private static readonly SearchValues<char> _idSearchValues = SearchValues.Create("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-");

        public static bool IsValid(string id)
        {
            var idIncludesForbiddenChars = id.AsSpan().ContainsAnyExcept(_idSearchValues);
            return !idIncludesForbiddenChars;
        }
    }
}
