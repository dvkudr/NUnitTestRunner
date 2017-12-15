using System;
using NUnit;
using NUnit.Framework;

namespace TestSample
{
    [TestFixture]
    public class TestMulClass
    {
        [SetUp]
        public void Setup()
        {
            Console.WriteLine($"{nameof(TestMulClass)} setup");
        }

        [TearDown]
        public void Teardown()
        {
            Console.WriteLine($"{nameof(TestMulClass)} teardown");
        }

        [Test]
        public void TestMultiplicationSuccess()
        {
            var x1 = 2;
            var x2 = 3;
            var expectedResult = 6;

            var actualResult = x1 * x2;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestMultiplicationFail()
        {
            var x1 = 2;
            var x2 = 2;
            var expectedResult = 5;

            var actualResult = x1 * x2;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(0, 1, 0)]
        [TestCase(1, 0, 0)]
        [TestCase(1, 2, 2)]
        [TestCase(2, 1, 2)]
        [TestCase(2, 2, 5)]
        [TestCase(2, 3, 6)]
        public void TestMultiplicationCases(int x1, int x2, int expectedResult)
        {
            var actualResult = x1 * x2;

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}