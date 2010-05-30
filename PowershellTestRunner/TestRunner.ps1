$programPath = pwd
$nunit = "C:\Program Files (x86)\NUnit 2.4.8\bin"
cd..
$path = pwd
cd $nunit
dir $path -filter *Tests.dll -recurse | foreach{
    if ($_.FullName.contains("bin")){
        $file = [string]::format("Running {0}", $_.Name)
        Write-Host $file -foregroundcolor red
        .\nunit-console.exe $_.FullName
    }
}
cd $programPath