param($installPath, $toolsPath, $package, $project)

Import-Module (Join-Path $toolsPath "OneClickBuild.psm1")

# We need some dummy content to kick off Install.ps1/Uninstall.ps1 (http://stackoverflow.com/questions/6892025/nuget-install-ps1-does-not-get-called)
# As content is automatically added to the project we remove it right-away
function Delete-Temporary-File 
{
	Write-Host "Delete temporary file"

	$project.ProjectItems | Where-Object { $_.Name -eq 'OneClickBuild.ReadMe.md' } | Foreach-Object {
		Remove-Item ( $_.FileNames(0) )
		$_.Remove() 
	}
}

function Copy-File($file, $sourceDir, $destDir, $overwrite = $true) {
	$destFile = Join-Path $destDir $file
	if($overwrite -or !(Test-Path $destFile)) {
		$sourceFile = Join-Path $sourceDir $file
		Copy-Item $sourceFile $destDir -Force | Out-Null
	}
	return $destFile
}

function Get-Solution-Folder($folderName) {
	$solution = Get-Interface $dte.Solution ([EnvDTE80.Solution2])
	$folder = $solution.Projects | Where {$_.ProjectName -eq $folderName}
	if (!$folder) {
		$folder = $solution.AddSolutionFolder($folderName)
	}
	return $folder
}

function Get-Solution-Name
{
	$solution = Get-Interface $dte.Solution ([EnvDTE80.Solution2])
	$itm = Get-Item $solution.FullName
	return $itm.Name
}

function Add-Project-Item($projectItems, $file) {
	$projPath = [IO.Path]::GetFullPath($file)
	$projectItems.AddFromFile($projPath)
}

function Add-OneClickBuild($project) {

	# copy files to .build folder
	$solutionDir = Get-SolutionDir
	$buildDir = Join-Path $solutionDir ".build"
	if(!(Test-Path $buildDir)) {
		mkdir $buildDir | Out-Null
	}
	$buildFile = Copy-File "build.bat" $toolsPath $solutionDir
	$targetsFile = Copy-File "build.targets" $toolsPath $buildDir
	$emptyTargetsFile = Copy-File "empty.targets" $toolsPath $buildDir

	# add as solution items
	$buildFolder = Get-Solution-Folder ".build"
	$buildItems = Get-Interface $buildFolder.ProjectItems ([EnvDTE.ProjectItems])
	Add-Project-Item $buildItems $buildFile
	Add-Project-Item $buildItems $targetsFile
	Add-Project-Item $buildItems $emptyTargetsFile

	# add the solution level packages.config
	$nugetFolder = Get-Solution-Folder ".nuget"
	$nugetItems = Get-Interface $nugetFolder.ProjectItems ([EnvDTE.ProjectItems])
	$packagesFile = Join-Path $solutionDir ".nuget\packages.config"
	if(!(Test-Path $packagesFile)) {
		throw "Could not find solution level packages config"
	}
	Add-Project-Item $nugetItems $packagesFile | Out-Null

	#add solution targets and assembly files
	$solutionFolder = Get-Solution-Folder "Solution Items"
	$solutionItems = Get-Interface $solutionFolder.ProjectItems ([EnvDTE.ProjectItems])
	
	if (!(Test-Path -Path (Join-Path $solutionDir "solution.targets"))) { # make sure solution targets dont get overwritten
		$solutionTargetsFile = Copy-File "solution.targets" $toolsPath $solutionDir
		Add-Project-Item $solutionItems $solutionTargetsFile
	}; 

	if (!(Test-Path -Path (Join-Path $solutionDir "VersionInfo.g.cs"))) { # make sure version info dont get overwritten
		$versionInfoFile = Copy-File "VersionInfo.g.cs" $toolsPath $solutionDir
		Add-Project-Item $solutionItems $versionInfoFile
	}; 

	if (!(Test-Path -Path (Join-Path $solutionDir "SolutionInfo.cs"))) { # make sure solution info dont get overwritten
		$solutionInfoFile = Copy-File "SolutionInfo.cs" $toolsPath $solutionDir
		Add-Project-Item $solutionItems $solutionInfoFile
	}; 

	$tmpBeforeName =(Join-Path $solutionDir ("before." + (Get-Solution-Name) + ".targets")) 
	if (!(Test-Path -Path $tmpBeforeName)) { # make sure solution info dont get overwritten
		$beforeTargetsFile = Copy-File "before.sln.targets" $toolsPath $tmpBeforeName
		Add-Project-Item $solutionItems $beforeTargetsFile
	}; 
	
	# add import for the targets file
	Add-Import '$(SolutionDir)\.build\build.targets' $project.Name
}

function Main {
	Delete-Temporary-File
	Add-OneClickBuild $project
}

Main