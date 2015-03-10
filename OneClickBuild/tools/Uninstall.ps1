param($installPath, $toolsPath, $package, $project)

Import-Module (Join-Path $toolsPath "OneClickBuild.psm1")

function Main {
	Remove-Import '.build\build.targets' $project.Name
}

Main
