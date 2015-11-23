using System;
using System.Numerics;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MyApp
{
    [TestFixture]
    internal class ProgramTests
    {
        // ReSharper disable InconsistentNaming
        [Test]
        public void Test_PrintArgs()
        {
            Program.PrintArgs("Hello","World");
            Assert.Pass();
        }

        [Test]
        public void Test_arrange_and_act()
        {
            var actual = Program.Arrange();
            Assert.That(actual, Is.Not.Null);

            Program.Act(actual);
            Assert.Pass();
        }

        [Test, Ignore("make build pass")]
        public void Some_Failing_Test()
        {
            Assert.Fail();
        }

        #region NUnit .NET framework selection tests 

        [Test(Description = "Test NUnit test framework selection by using async/await")]
        public async Task AnAsyncPassingTest()
        {
            var actual = await Task.Run(() => 42);
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


        #endregion

    }
}