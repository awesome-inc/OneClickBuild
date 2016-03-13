Welcome to OneClickBuild.

The OneClickBuild package simplifies your build and makes it run everywhere started 
with a simple `build.bat` from the command line

OneClickBuild comprises
  - Versioning: Solution wide assembly versioning (including commit info via GitVersion),
  - Tests: running tests (NUnit & XUnit), 
  - Coverage: Computing Test coverage (with OpenCover) and
  - Deploy: NuGet packaging and pushing

A new member coming to your team does not require any special tooling to compile, run tests, etc.
This makes it also perfectly suitable for continuous integration since your
build jobs reduce to the same one-liner you can use in development.

Steps after 1st time installation:

1) Copy `OneClickBuild\tools` files to your solution root folder. 

    copy .\packages\OneClickBuild.[version]\tools\*.*

2) Rename `before.sln.targets` to `before.[solutionfilename].sln.targets`.

    ren before.sln.targets before.[solutionname].sln.targets

3) Add files as solution items to so you are aware of the files in Visual Studio as well.

4) Complete `SolutionInfo.cs` with e.g. trademark, company & copyright info.

5) Remove duplicate assembly attributes in your `Properties\AssemblyInfo.cs`, i.e.

    [assembly: AssemblyDescription("")]
    [assembly: AssemblyConfiguration("")]
    [assembly: AssemblyCompany("")]
    [assembly: AssemblyProduct("...")]
    [assembly: AssemblyCopyright("Copyright ©  2016")]
    [assembly: AssemblyTrademark("")]
    [assembly: AssemblyCulture("")]
    [assembly: ComVisible(false)]
    [assembly: AssemblyVersion("1.0.0.0")]
    [assembly: AssemblyFileVersion("1.0.0.0")]

Finally, do a test build. In your solution folder type

    build

and you should get a clean `Release` build with version and git commit info attached
to your output assemblies (see `File -> Properties -> Details`).

Optionally you might find it helpful to wrap up on GitVersion:

   - [GitVersion / Usage / Command Line (Docs)](http://gitversion.readthedocs.org/en/latest/usage/#command-line)

That's it. Enjoy.

If you have any issues or feature requests with OneClickBuild please raise them 
on [awesome-inc/OneClickBuild](https://github.com/awesome-inc/OneClickBuild).
