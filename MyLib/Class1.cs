using System.Diagnostics;

namespace MyLib
{
    public class Class1
    {
        public void SomeCoveredMethod()
        {
            Trace.TraceInformation("SomeCoveredMethod called");
        }

        public void SomeUncoveredMethod()
        {
            Trace.TraceInformation("SomeUncoveredMethod called");
        }

    }
}