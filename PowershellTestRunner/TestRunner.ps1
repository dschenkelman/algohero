
function printResultsFromFile($filePath)
{
    gc $filePath | foreach{
        if ($_.Contains("Tests run:")){
            if ($_.Contains("Failures: 0")){
                Write-Host $_ -foregroundcolor green
            }
            else
            {
                Write-Host $_ -foregroundcolor red
            }
        }
    }
}


$programPath = pwd
$nunit = "C:\Program Files (x86)\NUnit 2.4.8\bin"
cd..
$path = pwd
cd $nunit
$outputPath = [System.IO.Path]::Combine($programPath, "results.txt")
dir $path -filter *Tests.dll -recurse | foreach{
    if ($_.FullName.contains("bin")){
        $file = [string]::format("Running {0}", $_.Name)
        Write-Host $file -foregroundcolor cyan
        .\nunit-console.exe $_.FullName | Out-File $outputPath
        printResultsFromFile $outputPath
    }
}

del $outputPath
cd $programPath