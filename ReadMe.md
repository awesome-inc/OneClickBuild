[![Build status](https://ci.appveyor.com/api/projects/status/qs1cu14tjvh1j0le?svg=true)](https://ci.appveyor.com/project/awesome-inc-build/oneclickbuild) ![NuGet Version](https://img.shields.io/nuget/v/OneClickBuild.svg?style=flat-square) ![NuGet Version](https://img.shields.io/nuget/dt/OneClickBuild.svg?style=flat-square)

# OneClickBuild

The *OneClickBuild* package includes a simple `build.bat` and msbuild targets bringing you closer to the famous *1-Click-Build*. 

The `build.bat` shortcuts to MSBuild, the targets include 

- solution wide assembly version and assembly info files,
- git versioning,
- running tests (with NUnit), 
- computing code coverage (with OpenCover) and
- NuGet packaging and pushing.

The package aims to reduce dependencies on pre-installed external tools by getting the runners directly from NuGet. The only thing you need is
	
1. .NET for MSBuild (preinstalled since Windows 7) and
2. `NuGet.exe` (in your path)

A new member coming to your team does not require any special tooling to compile, run tests, etc.
This makes it also perfectly suitable for continuous integration since your build jobs reduce to the same one-liner you can use in development.

## Download

The OneClickBuild package is available via package name `OneClickBuild`.
To install OneClickBuild, run the following command in the Package Manager Console

	PM> Install-Package OneClickBuild
	
## Additional targets

OneClickBuild brings the following additional build targets

- **Test**: Runs tests, default runner: [NUnit](http://www.nunit.org/)
- **Coverage**: Computes (test/code) coverage, default tool: [OpenCover](http://opencover.codeplex.com/)
- **CoverageReport**: Generates HTML reports from code coverage output, default tool: [ReportGenerator](https://reportgenerator.codeplex.com/)
- **Package**: Packages the project, default: [nuget pack](http://docs.nuget.org/docs/creating-packages/creating-and-publishing-a-package#Creating_a_Package)
- **Deploy**: Deploys the project package, default: [nuget push](http://docs.nuget.org/docs/creating-packages/creating-and-publishing-a-package)
 
## Usage Examples

### Building your project

The Usage for `build.bat` is

	build <msbuild command line>

where the [MSBuild Command-Line](http://msdn.microsoft.com/en-us/library/ms164311.aspx) defaults to 

1. build the single solution file present in the current directory
2. with target `Build`. The default `Configuration` is `Release`.

So to build your solution, open a command prompt in your solution directory and just type

	build

To clean and build a specific project type

	build Project\Project.csproj /t:Clean;Build

### Versioning

The version number is generated during the build process according to  [Semantic Versioning](http://semver.org/) augmented by 

- the *build number* from [CI](http://en.wikipedia.org/wiki/Continuous_integration) and
- the *revision number* of the [SCM](http://en.wikipedia.org/wiki/Software_configuration_management).

The pattern is

	<major>.<minor>.<patch>.<build>.<revision>

with

- **major**: Manually incremented for major releases with breaking changes such as adding many new features to the solution or a new architectural design.
- **minor**: Manually incremented for minor releases, such as regular feature implementations (backwards-compatible).
- **patch**: Manually incremented for bug fixes or single and simple feature implementations (backwards-compatible).
- **build**: Automatically set by the continuous build server ([Jenkins](https://jenkins-ci.org/) and [Appveyor](http://www.appveyor.com/) supported).
- **revision**: Automatically set by the build process (client and server) to reflect the source code revision the build is based on. Done with [Gitversion](https://github.com/GitTools/GitVersion).

The first three version parts should be set in `GitVersionConfig.yaml`.
See [GitVersion Usage](http://gitversion.readthedocs.org/en/latest/usage/) for more details.

### Including version number to your project

Most solution wide settings for all assemblies are stored in `SolutionInfo.cs` except of the version information, which is integrated with [GitVersion](https://github.com/GitTools/GitVersion).

After installing `OneClickBuild` you need to strip down your original `Properties\AssemblyInfo.cs` down to the following two default attributes:

	[assembly: AssemblyTitle("MyLib")]
	[assembly: Guid("22669957-af00-4154-9ec9-633664d5d29b")]

After this, all of your project assemblies contain the same meta information and version number.

### Running Tests (NUnit)

The target `Test` executes the NUnit console runner. 
Run from the command line using

	build MyLib.Tests\MyLib.Tests.csproj /t:Test

**Note:** When tests fail the build will fail. As the execution of tests should support build verification this is considered good build practice.

The output location for the test results can be set by the properties 
`TestResultsDir` and `TestResults`. They default to

	<PropertyGroup>
		<TestResultsDir>$(SolutionDir)testresults\</TestResultsDir>
		<TestResults>$(TestResultsDir)$(ProjectName).Tests.xml</TestResults>
	</PropertyGroup>

You can explicitly define the assemblies for NUnit to run by 
including `TestAssemblies`-items in your project like this

	<ItemGroup>
		<!-- run tests on "MyLib.Tests.dll" -->
	  <TestAssemblies Include="MyLib.Tests.dll"/>
	</ItemGroup>

This will instruct NUnit to run all tests found in assembly  `MyLib.Tests.dll`.
Alternatively, you can use the `TestsProjectPattern`-property to specify a 
wildcard pattern like this

	<PropertyGroup>
		<!-- run tests on all assemblies in the output 
			 directory matching "*.Tests.dll" -->
		<TestsProjectPattern>*.Tests.dll</TestsProjectPattern>
	</PropertyGroup>

Typically you will go with the default which follows the convention

	<TestsProjectPattern >$(OutDir)$(AssemblyName).dll</TestsProjectPattern>

for libraries and

	<TestsProjectPattern >$(OutDir)$(AssemblyName).exe</TestsProjectPattern>

for applications. This presumes that you may have tests included in your production code as well as your main application code. It won't harm if you don't, however.


**Notes:**

- The `Test`-target will look up the `PlatformTarget`-property to use the correct NUnit runner, i.e.
`nunit-console.exe` for `x64/AnyCPU` or `nunit-console-x86.exe` for `x86` 

- Multiple patterns can be specified like

	<TestsProjectPattern >$(OutDir)$(AssemblyName).exe;$(OutDir)MyCode*.dll;$(OutDir)3rdParty*.dll</TestsProjectPattern>

### Getting Code Coverage (OpenCover)

The target `Coverage` executes [OpenCover](http://opencover.codeplex.com/) targeting [NUnit](http://www.nunit.org/).
Run from the command line using

	build MyLib.Tests\MyLib.Tests.csproj /t:Coverage

The output location for the coverage results can be set by the `OpenCoverOutput`-property 
which defaults to

	<OpenCoverOutput>$(TestResultsDir)$(ProjectName).Coverage.xml</OpenCoverOutput>

The default filter for OpenCover is

	<OpenCoverFilter>+[$(AssemblyName)]* -[*]*Tests -[FluentAssertions]*</OpenCoverFilter>

which includes all code from the current assembly and excludes all classes ending with `Tests` and everything from the `FluentAssertions`-assembly which i use extensively in my tests. More details on OpenCover-filters can be found in the
[OpenCover documentation (pdf)](https://github.com/sawilde/opencover/blob/master/main/OpenCover.Documentation/Usage.pdf?raw=true)
or the [OpenCover Usage Wiki (GitHub)](https://github.com/opencover/opencover/wiki/Usage).

**Notes:**

- OneClickBuild v1.3 supports the standard `[ExcludeFromCodeCoverage]` attribute (see [MSDN](http://msdn.microsoft.com/en-us/library/dd984116(VS.100).aspx)). This is handy for generated code snippets or [MiniMods](https://github.com/minimod/minimods).

### Coverage Report
To generate a HTML report from the coverage xml use the `CoverageReport`-target, i.e.

	build [project] /t:CoverageReport

This will generate a directory containing HTML defaulting to

	<CoverageReportDir>$(TestResultsDir)coverage\$(ProjectName)</CoverageReportDir>

#### Coverage vs. Test

Running coverage usually includes running tests so you can produce results for both by just running coverage. This also reduces build time and is therefore considered as good build practice. The default behavior of the `Coverage` target is therefore to return the exit code of its target application (usually the NUnit test runner). So when a test fails the `Coverage` target will also fail.

This is the default behavior but since there may be cases where you want to get coverage even on failing code you can disable this by setting the project property `CoverageFailOnTargetFail` to `false` like this

	<PropertyGroup>
		<CoverageFailOnTargetFail>false</CoverageFailOnTargetFail>
	</PropertyGroup>

### Continuous Integration (Jenkins)

In Jenkins just use the *Execute Windows Batch command* and use a command line like

	build [project] [/t:targets] [/v:verbosity]

For automatic builds you may set MSBuild's output verbosity to minimal, e.g.

	build [project] [/t:targets] /v:m

To publish test results use the [NUnit plugin](https://wiki.jenkins-ci.org/display/JENKINS/NUnit+Plugin)
and add the Post-Build step *Publish NUnit test result report* with

	testresults/*.Tests.xml

To publish coverage reports use the [HTML Publisher Plugin](https://wiki.jenkins-ci.org/display/JENKINS/HTML+Publisher+Plugin)
and add the Post-Build step *Publish HTML reports* with

	testresults\coverage\[project] | index.htm | Coverage [project]

Note that you need to generate a HTML report from the OpenCover results using

	build [project] /t:CoverageReport

### NuGet Packaging and Pushing

To build a [NuGet](http://en.wikipedia.org/wiki/NuGet) package from your library simply add a `Package.nuspec` file to your project and build the `Package`-target, e.g.

	build MyLibrary\MyLibrary.csproj /t:Package

which will output `MyLibrary.[version].nupkg` to the project directory. You can override the default filename for `Package.nuspec` with the `NuspecFile`-property in `solution.targets` like

	<NuspecFile>MyNuSpecFile.nuspec</NuspecFile>

To push the built package to a NuGet gallery use the `Deploy`-target like

	build MyLibrary\MyLibrary.csproj /t:Package

To control the NuGet server the package is pushed to, override the property
`NugetSourceToDeploy` in `solution.targets`. It defaults to
 
	<PropertyGroup>
		<NugetSourceToDeploy>https://www.nuget.org</NugetSourceToDeploy>
	</PropertyGroup>
 

#### Example Package.nuspec
Here is a simplified version of the `Package.nuspec` that is used by `OneClickBuild` itself:

	<?xml version="1.0"?>
	<package >
		<metadata>
			<id>OneClickBuild</id>
			<version>$Version$</version>
			<title>OneClickBuild</title>
			<authors>mkoertgen,tmentzel</authors>
			<owners>mkoertgen,tmentzel</owners>
			<requireLicenseAcceptance>false</requireLicenseAcceptance>
			<description>Simplify your build, run tests and coverage.</description>
			<summary>...</summary>
			<copyright/>
			<releaseNotes>Revision: $Revision$</releaseNotes>
			<projectUrl>https://github.com/mkoertgen/OneClickBuild</projectUrl>
			<dependencies>
				<dependency id="MSBuildTasks" version="1.4.0.88" />
				<dependency id="NUnit.Runners" version="2.6.4" />
				<dependency id="OpenCover" version="4.5.3723" />
				<dependency id="ReportGenerator" version="2.1.2.0" />
			</dependencies>    
			<references></references>
			<tags>continuous integration</tags>
		</metadata>
		<files>
			<file src="tools\*" target="tools" />
			<file src="..\ReadMe.md" target="content\OneClickBuild.ReadMe.md" />
			<!-- Automatically Displaying a NextSteps During Package Installation, cf.:
				http://docs.nuget.org/docs/creating-packages/creating-and-publishing-a-package#Automatically_Displaying_a_Readme.txt_File_During_Package_Installation -->
			<file src="tools\NextSteps.OneClickBuild.md" target="" />
		</files>
	</package>

### Continuous Inspection (SonarQube)

NUnit test results as well as OpenCover coverage reports can be directly reused in [SonarQube](http://www.sonarqube.org/).
Here is a snippet from a sample *sonar-project.properties*:

	... 
	# NUnit test results (since C# 3.3)
	# cf.: http://stackoverflow.com/questions/27460489/how-to-include-nunit-test-results-in-sonarcube-4-5-1-c-sharp-plugin-3-3-sonar-pr
	sonar.cs.nunit.reportsPaths=testresults/MyApp.Tests.xml
	
	# Code Coverage
	sonar.cs.opencover.reportsPaths=testresults/MyApp.Coverage.xml
	...


## FAQ

### I get a warning from NuGet.exe saying that "restore" is an unknown command.

The command `restore` is supported since NuGet v2.7. You probably need to update your `NuGet.exe` by typing

	nuget.exe update -self

### I use .NET 4.0 and when running coverage i get an error saying that "assembly 'System.Core, Version=2.0.5.0' could not be loaded."

OpenCover uses [AutoFac](https://code.google.com/p/autofac/) which is a great [IoC](http://de.wikipedia.org/wiki/Inversion_of_Control) container.
However, at the time of writing *AutoFac* is deployed as [Portable Class Library](http://msdn.microsoft.com/en-us/library/vstudio/gg597391(v=vs.100).aspx) which 
- for .NET 4.0 - requires the [KB2468871](http://support.microsoft.com/kb/2468871)-Patch  as explained
in [AutoFac Issue 415](https://code.google.com/p/autofac/issues/detail?id=415).

### I get an MSBuild error saying that OutputPath property is not set

You maybe targeting the `x86` platform and have defined `AnyCPU` as default in your project file.
When building from Visual Studio the platform property is set via the solution file. However, when building
the project you did not specify the platform property, thus defaulting to `AnyCPU` resulting in `OutputPath` not set. 
To fix this issue either set the Platform property accordingly or just set the default to `x86` like this

	<?xml version="1.0" encoding="utf-8"?>
	<Project ToolsVersion="4.0" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
	  <PropertyGroup>
		<Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
		<!-- <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform> -->
		<Platform Condition=" '$(Platform)' == '' ">x86</Platform>
	   
The full error message should read something like this

	"<myproject>.csproj" (Build target) (1) -> 
	(_CheckForInvalidConfigurationAndPlatform target) ->
	  C:\Windows\Microsoft.NET\Framework\v4.0.30319\Microsoft.Common.targets(609,5)
	: error : The OutputPath property is not set for project '<myproject>.csproj'.
	Please check to make sure that you have specified a valid combination of 
	Configuration and Platform for this project.  Configuration='Release'  Platform='AnyCPU'.  
	You may be seeing this message because you are trying to build a project without a solution file,
	and have specified a non-default Configuration or Platform that doesn't exist for this project. [<myproject>.csproj]

### I get an MSBuild error saying that a `<project>.metaproj` could not be found

You may have set `ProjectDependencies` in your solution file which you should remove, cf.:
[Building .net 4.0 web sites: .metaproj -files (Social MSDN)](http://social.msdn.microsoft.com/Forums/vstudio/en-US/562ae95f-e042-45c2-9821-62cac49d0152/building-net-40-web-sites-metaproj-files?forum=msbuild)

### 'Test' target fails on Windows 10 with exit code -2146232576.

NUnit runners need .NET 3.5 so you need to turn on this Windows Feature. 