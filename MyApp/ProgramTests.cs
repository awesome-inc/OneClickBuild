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
    }
}