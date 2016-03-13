using Xunit;

namespace XUnitSample
{
    // NOTE: xunit facts must be PUBLIC!
    public class Class1Facts
    {
        [Fact]
        public void SomePassingTest()
        {
            var sut = new Class1();
            sut.SomeCoveredMethod();
        }

        [Fact]
        public void AnotherPassingTest()
        {
            var sut = new Class1();
            //sut.SomeUncoveredMethod();
        }

        [Fact(Skip = "Ignore to fix test build")]
        public void SomeFailingTest()
        {
            Assert.True(false);
        }
    }
}
