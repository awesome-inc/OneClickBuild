using NUnit.Framework;

namespace MyLib.Tests
{
    [TestFixture]
    class Class1Tests
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

        [Test(Description="Ignore to fix test build")]
        public void SomeFailingTest()
        {
            Assert.Fail();
        }

        [Test]
        public void SomeInconclusiveTest()
        {
            Assert.Inconclusive("TODO");
        }
    }
}
