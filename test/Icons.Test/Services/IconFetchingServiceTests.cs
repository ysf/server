using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Bit.Icons.Services;
using Bit.Test.Common.AutoFixture;
using Bit.Test.Common.AutoFixture.Attributes;
using NSubstitute;
using Xunit;

namespace Bit.Icons.Test.Services
{
    public class IconFetchingServiceTests
    {
        [Theory]
        [InlineSutAutoData("www.google.com")] // https site
        [InlineSutAutoData("neverssl.com")] // http site
        [InlineSutAutoData("ameritrade.com")]
        [InlineSutAutoData("icloud.com")]
        [InlineSutAutoData("bofa.com")]
        public async Task GetIconAsync_Success(string domain, SutProvider<IconFetchingService> sutProvider)
        {
            SetHttpClientFactory(sutProvider);

            var result = await sutProvider.Sut.GetIconAsync(domain);

            Assert.NotNull(result);
            Assert.NotNull(result.Icon);
        }

        [Theory]
        [InlineSutAutoData("1.1.1.1")]
        [InlineSutAutoData("")]
        [InlineSutAutoData("localhost")]
        public async Task GetIconAsync_ReturnsNull(string domain, SutProvider<IconFetchingService> sutProvider)
        {
            SetHttpClientFactory(sutProvider);

            var result = await sutProvider.Sut.GetIconAsync(domain);

            Assert.Null(result);
        }

        private static void SetHttpClientFactory(SutProvider<IconFetchingService> sutProvider)
        {
            var httpClientFactory = Substitute.For<IHttpClientFactory>();
            httpClientFactory.CreateClient()
                .Returns(new HttpClient(new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                })
                {
                    Timeout = TimeSpan.FromSeconds(20),
                    MaxResponseContentBufferSize = 5_000_000,
                });

            sutProvider.SetDependency(httpClientFactory);
        }
    }
}
