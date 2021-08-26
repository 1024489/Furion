using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Furion.IntegrationTests
{
    /// <summary>
    /// ���ݼӽ��ܼ��ɲ���
    /// </summary>
    public class DataEncryptionTests : IClassFixture<WebApplicationFactory<TestProject.Startup>>
    {
        private readonly ITestOutputHelper Output;
        private readonly WebApplicationFactory<TestProject.Startup> _factory;
        public DataEncryptionTests(ITestOutputHelper tempOutput,
            WebApplicationFactory<TestProject.Startup> factory)
        {
            Output = tempOutput;
            _factory = factory;
        }

        /// <summary>
        /// ���� MD5 ���ܼ���Сд����
        /// </summary>
        /// <param name="url"></param>
        /// <param name="text"></param>
        /// <param name="uppercase"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("/api/data-encryption-tests/test-md5-encrypt", "Furion", true)]
        [InlineData("/api/data-encryption-tests/test-md5-encrypt", "Furion", false)]
        public async Task Test_RunCompile_Or_FromCached(string url, string text, bool uppercase)
        {
            using var httpClient = _factory.CreateClient();
            using var response = await httpClient.PostAsync($"{url}/{text}/{uppercase}", default);

            var content = await response.Content.ReadAsStringAsync();
            Output.WriteLine(content);
            response.EnsureSuccessStatusCode();

            Assert.Equal(uppercase ? "08C5318BD8A478A88E6D594A0142AE6C" : "08c5318bd8a478a88e6d594a0142ae6c", content);
        }
    }
}
