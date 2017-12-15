using System;
using NUnit.Framework;

namespace TestSample
{
    [TestFixture]
    public class TestAddClass
    {
        [TestCase(0, 1, 1)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 2, 3)]
        [TestCase(2, 1, 3)]
        [TestCase(2, 2, 5)]
        [TestCase(2, 3, 5)]
        public void TestAdditionCases(int x1, int x2, int expectedResult)
        {
            var actualResult = x1 + x2;

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
