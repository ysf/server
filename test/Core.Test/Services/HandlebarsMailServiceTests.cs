using System;
using Bit.Core.Services;
using Bit.Core.Settings;
using Microsoft.AspNetCore.DataProtection;
using NSubstitute;
using Xunit;

namespace Bit.Core.Test.Services
{
    public class HandlebarsMailServiceTests
    {
        private readonly HandlebarsMailService _sut;

        private readonly GlobalSettings _globalSettings;
        private readonly IMailDeliveryService _mailDeliveryService;
        private readonly IMailEnqueuingService _mailEnqueuingService;
        private readonly IDataProtector _dataProtector;

        public HandlebarsMailServiceTests()
        {
            _globalSettings = new GlobalSettings();
            _mailDeliveryService = Substitute.For<IMailDeliveryService>();
            _mailEnqueuingService = Substitute.For<IMailEnqueuingService>();
            _dataProtector = Substitute.For<IDataProtector>();

            _sut = new HandlebarsMailService(
                _globalSettings,
                _mailDeliveryService,
                _mailEnqueuingService,
                _dataProtector
            );
        }

        // Remove this test when we add actual tests. It only proves that
        // we've properly constructed the system under test.
        [Fact]
        public void ServiceExists()
        {
            Assert.NotNull(_sut);
        }
    }
}
