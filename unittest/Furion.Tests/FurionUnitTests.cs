using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Extensions.Contracts;

namespace Furion.Tests
{
    [TestClass]
    public class FurionUnitTests
    {
        [ContractTestCase]
        public void TestCases()
        {
            "���� Furion ���ʾ��".Test(() =>
            {
                Assert.AreEqual("Fur", "Furion");
            });
        }
    }
}