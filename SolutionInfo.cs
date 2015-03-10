using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyDescription(@"© <your company> <year-year>.
<your copyright notice>.")]

[assembly: AssemblyCompany("<your company>")]
[assembly: AssemblyProduct("<your product>")]
[assembly: AssemblyCopyright("Copyright © <your company> <year>")]
[assembly: AssemblyTrademark("<your trademark>")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]

[assembly: NeutralResourcesLanguage("en", UltimateResourceFallbackLocation.MainAssembly)]
