using System.Diagnostics;
using System.Threading.Tasks;

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

        public Task<int> SomeTask()
        {
            return Task.FromResult(42);
        }
    }
}