
**This package provides solution wide items for versioning and assembly tagging.**

##Action Items


1.  Include a *"Add as Link"* to you project for both files: `SolutionInfo.cs` and `VersionInfo.g.cs`
Keep in mind that `VersionInfo.g.cs` is a auto generated file during the build process.

2. Set the version number in `solution.targets`. Do not change the `VersionInfo.g.cs` file manually.

3. Change the `SolutionInfo.cs` file and modify the following attributes:

		[assembly: AssemblyTrademark("<trademark name>")]
		[assembly: AssemblyProduct("<product name>")]

###`before.<solution-file>.targets`##

This is important to enable the OneClickBuild-specific targets on 
	1. the solution meta-project and on
	2. projects in the solution that don´t explicitely use OneClickBuild.
 
	The bottom line is that you can simply do

		build MySolution.sln /t:Coverage

	and projects not knowing the `Coverage` target will silently skip instead of breaking the build because of unknown or unsupported target.
	For more information see [MSBuild: Ignore targets that don't exist (StackOverflow)](http://stackoverflow.com/questions/15869111/msbuild-ignore-targets-that-dont-exist)


## After you performed the `action items` above, you can delete this file