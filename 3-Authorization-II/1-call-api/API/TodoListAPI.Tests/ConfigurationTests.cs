using System;
using Xunit;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace TodoListAPI.Tests
{
    public class ConfigurationTests
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            return config;
        }

        [Fact]
        public void ShouldNotContainClientId()
        {
            var myConfiguration = ConfigurationTests.InitConfiguration();
            string clientId = myConfiguration.GetSection("AzureAd")["ClientId"];

            string pattern = @"(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}";
            var regex = new Regex(pattern);
            Assert.DoesNotMatch(regex, clientId);
        }

        [Fact]
        public void ShouldNotContainTenantId()
        {
            var myConfiguration = ConfigurationTests.InitConfiguration();
            string tenantId = myConfiguration.GetSection("AzureAd")["TenantId"];

            string pattern = @"(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}";
            var regex = new Regex(pattern);
            Assert.DoesNotMatch(regex, tenantId);
        }

        [Fact]
        public void ShouldNotContainDomain()
        {
            var myConfiguration = ConfigurationTests.InitConfiguration();
            string domain = myConfiguration.GetSection("AzureAd")["Domain"];

            string pattern = @"(^http[s]?:\/\/|[a-z]*\.[a-z]{3}\.[a-z]{2})|([a-z]*\.[a-z]{3}$)";
            var regex = new Regex(pattern);

            Assert.DoesNotMatch(regex, domain);
        }
    }
}
