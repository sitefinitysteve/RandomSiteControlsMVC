param(
	[Parameter(Mandatory=$true)]
	[string]$binariesDirectory
)

$assemblies = Get-ChildItem $binariesDirectory -Filter *.dll
$controllerAssemblies = @()
foreach ($assembly in $assemblies) {
    $loadedAssembly = [System.Reflection.Assembly]::LoadFrom($assembly.FullName)
    if ($loadedAssembly.CustomAttributes | ? { $_.AttributeType.FullName -eq "Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes.ControllerContainerAttribute" -or $_.AttributeType.FullName -eq "Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes.ResourcePackageAttribute"}) {
        $controllerAssemblies += $assembly.Name
    }
}

$controllerAssemblies | ConvertTo-Json -depth 100 | Set-Content "$binariesDirectory\ControllerContainerAsembliesLocation.json" -Force