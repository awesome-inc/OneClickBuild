using System;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MyLib
{
    [TestFixture]
    internal class Class1Tests
    {
        [Test]
        public void SomePassingTest()
        {
            var sut = new Class1();
            sut.SomeCoveredMethod();
            Assert.Pass();
        }

        [Test]
        public void AnotherPassingTest()
        {
            var sut = new Class1();
            //sut.SomeUncoveredMethod();
            Assert.Pass();
        }

        [Test(Description = "Ignore to fix test build"), Ignore("make build run")]
        public void SomeFailingTest()
        {
            Assert.Fail();
        }

        [Test]
        public void SomeInconclusiveTest()
        {
            Assert.Inconclusive("TODO");
        }

        #region NUnit .NET framework selection tests 

        [Test(Description = "Test NUnit test framework selection by using async/await")]
        public async Task AnAsyncPassingTest()
        {
            var sut = new Class1();
            var actual = await sut.SomeTask();
            Assert.AreEqual(actual, 42);
        }

        // cf.: .NET 4.0 new features https://msdn.microsoft.com/library/ms171868(v=vs.100).aspx#core_new_features_and_improvements            
        [Test(Description = "Test NUnit test framework selection by using dynamic")]
        public void DynamicTest()
        {
            dynamic d = 1;
            Assert.AreEqual(d + 1, 2);
        }

        [Test]
        public void SomeBigIntTest()
        {
            var actual = new BigInteger(2);
            Assert.IsTrue(actual.IsEven);
        }

        [Test]
        public void SomeTupleTest()
        {
            var actual = new Tuple<int, double>(1, 0.5);
            Assert.AreEqual(actual.Item1, 1);
            Assert.AreEqual(actual.Item2, 0.5);
        }

        // net 4.5+, cf.: https://msdn.microsoft.com/en-us/library/ms171868(v=vs.110).aspx

        [Test]
        public void RegEx_Timeout_test()
        {
            var expected = TimeSpan.FromSeconds(2);
            var r = new Regex("pattern", RegexOptions.Compiled, expected);
            Assert.AreEqual(expected, r.MatchTimeout);
        }

        #endregion
    }
}
