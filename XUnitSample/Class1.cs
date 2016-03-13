using System.Diagnostics;
using System.Threading.Tasks;

namespace XUnitSample
{
    public class Class1
    {
        // ReSharper disable MemberCanBeMadeStatic.Global
        public void SomeCoveredMethod()
        {
            Trace.TraceInformation("SomeCoveredMethod called");
        }

        public Task<int> SomeTask()
        {
            return Task.FromResult(42);
        }
    }
}
