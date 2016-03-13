using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyDescription(@"© Awesome Incremented 2016")]

[assembly: AssemblyCompany("Awesome Incremented")]
[assembly: AssemblyProduct("OneClickBuild")]
[assembly: AssemblyCopyright("Copyright © Awesome Incremented 2016")]
[assembly: AssemblyTrademark("Awesome Incremented")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]

[assembly: NeutralResourcesLanguage("en", UltimateResourceFallbackLocation.MainAssembly)]
