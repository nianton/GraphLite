param (
    [string]$applicationId = $(throw "-applicationId is required."), 
    [string]$applicationSecret = $(throw "-applicationSecret is required."),    
    [string]$tenant = $(throw "-tenant is required."),
    [string]$extensionProperty = ""
)

$settings = Get-Content ..\src\GraphLite.Tests\appsettings.stub.json
$settings = $settings.Replace("<APPLICATION_ID>", $applicationId)
$settings = $settings.Replace("<APPLICATION_SECRET>", $applicationSecret)
$settings = $settings.Replace("<TENANT>", $tenant.Replace(".onmicrosoft.com", ""))
$settings = $settings.Replace("<EXTENSION_PROPERTY>", $extensionProperty)

Set-Content -Value $settings -Path ..\src\GraphLite.Tests\appsettings.json