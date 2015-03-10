using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyTrademark("<trademark name>")]
[assembly: AssemblyProduct("<product name>")]

[assembly: AssemblyDescription(@"© <company name>
<your description1>")]
[assembly: AssemblyCompany("<your company>")]
[assembly: AssemblyCopyright("Copyright © <company name> <year-year>")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: NeutralResourcesLanguage("en", UltimateResourceFallbackLocation.MainAssembly)]
