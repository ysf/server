using System;
using System.Net.Http.Headers;

namespace Bit.Icons.Utilities
{
    public static class IconFetchingConstants
    {
        // Source: https://docs.fileformat.com/image/png/#png-file-header
        public static ReadOnlySpan<byte> PngHeader => new byte[] { 0x89, (byte)'P', (byte)'N', (byte)'G' };

        // Source: https://docs.fileformat.com/image/webp/#webp-header
        public static ReadOnlySpan<byte> WebPHeader => new byte[] { (byte)'R', (byte)'I', (byte)'F', (byte)'F' };

        // Source: https://docs.fileformat.com/image/ico/#header
        public static ReadOnlySpan<byte> IcoHeader => new byte[] { 0x00, 0x00, 0x01, 0x00 };

        // Source: https://docs.fileformat.com/image/jpeg/#file-structure
        public static ReadOnlySpan<byte> JpegHeader => new byte[] { 0xFF, 0xD8, 0xFF };

        public const int MaxResponseContentBufferSize = 5_000_000; // 5 MB

        public const string PngMediaType = "image/png";
        public const string IcoMediaType = "image/x-icon";
        public const string IcoAltMediaType = "image/vnd.microsoft.icon";
        public const string JpegMediaType = "image/jpeg";

        public static ReadOnlySpan<char> PngMediaTypeSpan => PngMediaType.AsSpan();
        public static ReadOnlySpan<char> IcoMediaTypeSpan => IcoMediaType.AsSpan();
        public static ReadOnlySpan<char> IcoAltMediaTypeSpan => IcoAltMediaType.AsSpan();
        public static ReadOnlySpan<char> JpegMediaTypeSpan => JpegMediaType.AsSpan();
    }
}
