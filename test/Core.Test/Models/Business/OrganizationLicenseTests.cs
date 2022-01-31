using System;
using Bit.Core.Entities;
using Bit.Core.Models.Business;
using Bit.Core.Settings;
using Bit.Test.Common.AutoFixture;
using Bit.Test.Common.AutoFixture.Attributes;
using Xunit;

namespace Bit.Core.Test.Models.Business
{
    public class OrganizationLicenseTests
    {
        
        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_IssuedAfter_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            var now = DateTime.UtcNow;
            sutProvider.Sut.Issued = now.AddDays(1);
            sutProvider.Sut.Expires = now.AddDays(-1);
            var gs = new Settings.GlobalSettings();
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_Expired_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            var now = DateTime.UtcNow;
            sutProvider.Sut.Issued = now.AddDays(-1);
            sutProvider.Sut.Expires = now.AddDays(-1);

            var gs = new Settings.GlobalSettings();
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_OnlyInstallationInvalid_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            gs.Installation.Id = Guid.NewGuid();
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_OrgLicenseKeyNull_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            organization.LicenseKey = null;
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_OrgLicenseKeyNotEqual_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            organization.LicenseKey = "Some Value";
            sutProvider.Sut.LicenseKey = "Some Other Value";
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_EnabledNotEqual_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            organization.Enabled = true;
            sutProvider.Sut.Enabled = false;
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_PlayTypeNotEqual_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            organization.PlanType = Enums.PlanType.EnterpriseAnnually;
            sutProvider.Sut.PlanType = Enums.PlanType.EnterpriseAnnually2019;
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_SeatsNotEqual_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            organization.Seats = 4;
            sutProvider.Sut.Seats = 5;
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_MaxCollectionsNotEqual_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            organization.MaxCollections = 4;
            sutProvider.Sut.MaxCollections = 5;
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_UseGroupsNotEqual_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            organization.UseGroups = true;
            sutProvider.Sut.UseGroups = false;
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_UseDirectoryNotEqual_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            organization.UseDirectory = true;
            sutProvider.Sut.UseDirectory = false;
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_UseTotpNotEqual_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            organization.UseTotp = true;
            sutProvider.Sut.UseTotp = false;
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_SelfHostNotEqual_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            organization.SelfHost = true;
            sutProvider.Sut.SelfHost = false;
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_NameNotEqual_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            organization.Name = "Some Value";
            sutProvider.Sut.Name = "Some Other Value";
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_ValidVersion1_ReturnsTrue(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            sutProvider.Sut.Version = 1;

            Assert.True(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_ValidVersion2_ReturnsTrue(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            sutProvider.Sut.Version = 2;
            organization.UsersGetPremium = sutProvider.Sut.UsersGetPremium;
            Assert.True(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_InvalidVersion2_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            sutProvider.Sut.Version = 2;
            organization.UsersGetPremium = true;
            sutProvider.Sut.UsersGetPremium = false;
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_ValidVersion3_ReturnsTrue(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            sutProvider.Sut.Version = 3;
            organization.UseEvents = sutProvider.Sut.UseEvents;
            Assert.True(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_InvalidVersion3_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            sutProvider.Sut.Version = 3;
            organization.UseEvents = true;
            sutProvider.Sut.UseEvents = false;
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_ValidVersion4_ReturnsTrue(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            sutProvider.Sut.Version = 4;
            organization.Use2fa = sutProvider.Sut.Use2fa;
            Assert.True(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_InvalidVersion4_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            sutProvider.Sut.Version = 4;
            organization.Use2fa = true;
            sutProvider.Sut.Use2fa = false;
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_ValidVersion5_ReturnsTrue(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            sutProvider.Sut.Version = 5;
            organization.UseApi = sutProvider.Sut.UseApi;
            Assert.True(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_InvalidVersion5_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            sutProvider.Sut.Version = 5;
            organization.UseApi = true;
            sutProvider.Sut.UseApi = false;
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_ValidVersion6_ReturnsTrue(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            sutProvider.Sut.Version = 6;
            organization.UsePolicies = sutProvider.Sut.UsePolicies;
            Assert.True(sutProvider.Sut.VerifyData(organization, gs));
        }

        [Theory, CustomAutoData(typeof(SutProviderCustomization))]
        public void VerifyData_InvalidVersion6_ReturnsFalse(Organization organization,
            SutProvider<OrganizationLicense> sutProvider)
        {
            SetValidTime(sutProvider);
            var gs = SetMainValues(sutProvider, organization);
            sutProvider.Sut.Version = 6;
            organization.UsePolicies = true;
            sutProvider.Sut.UsePolicies = false;
            Assert.False(sutProvider.Sut.VerifyData(organization, gs));
        }

        private static void SetValidTime(SutProvider<OrganizationLicense> sutProvider)
        {
            var now = DateTime.UtcNow;
            sutProvider.Sut.Issued = now.AddDays(-2);
            sutProvider.Sut.Expires = now.AddDays(2);
        }

        private static Settings.GlobalSettings SetMainValues(SutProvider<OrganizationLicense> sutProvider,
            Organization organization)
        {
            var gs = new Settings.GlobalSettings();
            gs.Installation.Id = sutProvider.Sut.InstallationId;
            organization.LicenseKey = sutProvider.Sut.LicenseKey;
            organization.Enabled = sutProvider.Sut.Enabled;
            organization.PlanType = sutProvider.Sut.PlanType;
            organization.Seats = sutProvider.Sut.Seats;
            organization.MaxCollections = sutProvider.Sut.MaxCollections;
            organization.UseGroups = sutProvider.Sut.UseGroups;
            organization.UseDirectory = sutProvider.Sut.UseDirectory;
            organization.UseTotp = sutProvider.Sut.UseTotp;
            organization.SelfHost = sutProvider.Sut.SelfHost;
            organization.Name = sutProvider.Sut.Name;

            sutProvider.Sut.Version = 1;

            return gs;
        }
    }
}
