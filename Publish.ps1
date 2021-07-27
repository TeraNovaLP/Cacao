<# This Source Code Form is subject to the terms of the Mozilla Public
   License, v. 2.0. If a copy of the MPL was not distributed with this
   file, You can obtain one at https://mozilla.org/MPL/2.0/. #>

$apiKey = Read-Host "NuGet API-Key";
$version = Read-Host "Version to publish";
$package = Read-Host "Package to publish";

if ($null -ne $apiKey -and $null -ne $version -and $package -like "Cacao*") {
    $packagePath = Join-Path -Path $PSScriptRoot -ChildPath $package -Resolve;

    Set-Location($packagePath);

    Invoke-Expression("dotnet clean");
    Invoke-Expression("dotnet publish -c release");

    Set-Location(Join-Path -Path $packagePath -ChildPath "/bin/Release");
    Invoke-Expression("dotnet nuget push $package.$version.nupkg --api-key $apiKey --source https://api.nuget.org/v3/index.json");
}
