using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Bit.Icons.Models;
using Bit.Icons.Services;
using Microsoft.Extensions.Logging.Abstractions;

namespace Bit.Icons.Benchmarks.Services
{
    [MemoryDiagnoser(true)]
    public class IconFetchingServiceBenchmarks
    {
        private static readonly IHttpClientFactory _httpClientFactory;

        static IconFetchingServiceBenchmarks()
        {
            var httpClient = new HttpClient(new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                })
            {
                Timeout = TimeSpan.FromSeconds(20),
                MaxResponseContentBufferSize = 5000000,
            };

            _httpClientFactory = new TestingHttpClientFactory(httpClient);
        }

        private readonly IconFetchingService _iconFetchingService;


        public IconFetchingServiceBenchmarks()
        {
            _iconFetchingService = new IconFetchingService(_httpClientFactory, NullLogger<IIconFetchingService>.Instance);
        }

        [Benchmark]
        public async Task<IconResult> Run()
        {
            return await _iconFetchingService.GetIconAsync("google.com");
        }
    }

    public class TestingHttpClientFactory : IHttpClientFactory
    {
        private readonly HttpClient _httpClient;

        public TestingHttpClientFactory(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpClient CreateClient(string name) => _httpClient;
    }

}
