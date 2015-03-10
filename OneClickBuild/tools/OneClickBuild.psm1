function Resolve-ProjectName {
	param(
		[parameter(ValueFromPipelineByPropertyName = $true)]
		[string[]]$ProjectName
	)
	
	if($ProjectName) {
		$projects = Get-Project $ProjectName
	}
	else {
		# All projects by default
		$projects = Get-Project
	}
	
	$projects
}

function Get-MSBuildProject {
	param(
		[parameter(ValueFromPipelineByPropertyName = $true)]
		[string[]]$ProjectName
	)
	Process {
		(Resolve-ProjectName $ProjectName) | % {
			$path = $_.FullName
			@([Microsoft.Build.Evaluation.ProjectCollection]::GlobalProjectCollection.GetLoadedProjects($path))[0]
		}
	}
}

function Add-Import {
	param(
		[parameter(Position = 0, Mandatory = $true)]
		[string]$Path,
		[parameter(Position = 1, ValueFromPipelineByPropertyName = $true)]
		[string[]]$ProjectName
	)
	Process {
		(Resolve-ProjectName $ProjectName) | %{
			$buildProject = $_ | Get-MSBuildProject
			$buildProject.Xml.AddImport($Path)
			$_.Save()
		}
	}
}

function Remove-Import {
	param(
		[parameter(Position = 0, Mandatory = $true)]
		[string]$Name,
		[parameter(Position = 1, ValueFromPipelineByPropertyName = $true)]
		[string[]]$ProjectName
	)
	Process {
		(Resolve-ProjectName $ProjectName) | %{
			$buildProject = $_ | Get-MSBuildProject
			$importToRemove = $buildProject.Xml.Imports | Where-Object { $_.Project.Endswith($Name) }
			$buildProject.Xml.RemoveChild($importToRemove)
			$_.Save()
		}
	}
}

function Get-SolutionDir {
	if($dte.Solution -and $dte.Solution.IsOpen) {
		return Split-Path $dte.Solution.Properties.Item("Path").Value
	}
	else {
		throw "Solution not avaliable"
	}
}

Export-ModuleMember Get-MSBuildProject, Add-Import, Remove-Import, Get-SolutionDir