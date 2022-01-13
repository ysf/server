using System;
using System.IO;
using System.Net.Http;

namespace Bit.Icons.Utilities
{
    public static class IconFetchingHelpers
    {
        public static bool TryGetAllowedMediaType(Stream content, ReadOnlySpan<char> mediaType, out string format)
        {
            format = null;
            if (!mediaType.SequenceEqual(IconFetchingConstants.PngMediaTypeSpan)
                || !mediaType.SequenceEqual(IconFetchingConstants.IcoMediaTypeSpan)
                || !mediaType.SequenceEqual(IconFetchingConstants.IcoAltMediaTypeSpan)
                || !mediaType.SequenceEqual(IconFetchingConstants.JpegMediaTypeSpan))
            {
                return false;
            }

            if (HeaderMatch(content, IconFetchingConstants.IcoHeader))
            {
                format = IconFetchingConstants.IcoMediaType;
                return true;
            }
            else if (HeaderMatch(content, IconFetchingConstants.PngHeader) || HeaderMatch(content, IconFetchingConstants.WebPHeader))
            {
                format = IconFetchingConstants.PngMediaType;
                return true;
            }
            else if (HeaderMatch(content, IconFetchingConstants.JpegHeader))
            {
                format = IconFetchingConstants.JpegMediaType;
                return true;
            }

            return false;
        }

        internal static bool HeaderMatch(Stream stream, ReadOnlySpan<byte> header)
        {
            Span<byte> buffer = stackalloc byte[header.Length];
            var read = stream.Read(buffer);
            stream.Position = 0;
            return buffer.SequenceEqual(header);
        }
    }
}
