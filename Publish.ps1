$apiKey = Read-Host "NuGet API-Key";
$version = Read-Host "Version to publish";
$package = Read-Host "Package to publish";

if ($null -ne $apiKey -and $null -ne $version -and $package -like "Cacao*") {
    Invoke-Expression("dotnet clean");
    Invoke-Expression("dotnet publish -c release");

    Set-Location (Join-Path $PSScriptRoot $package "/bin/Release" -Resolve);
    Invoke-Expression ("dotnet nuget push $package.$version.nupkg --api-key $apiKey --source https://api.nuget.org/v3/index.json");
}
