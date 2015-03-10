using NUnit.Framework;

namespace MyApp.Tests
{
    [TestFixture]
    class ProgramTests
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

        [Test]
        public void Some_Failing_Test()
        {
            Assert.Fail();
        }
        // ReSharper restore InconsistentNaming
    }
}