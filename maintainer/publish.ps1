function PublishNbtDevAssistant($projectFile, $releaseTag) {
    $outputDirectory = "C:\PublishedFiles\openAiFileFlow_$releaseTag\"
    $archivePath = "$outputDirectory\openAiFileFlow_$releaseTag.zip"

    # Cleanup Folder
	New-Item -ItemType Directory -Force -Path $outputDirectory
    Remove-Item "$outputDirectory*" -Recurse -Force
    
    # Publish Binaries
    dotnet publish $projectFile --configuration "Release" --framework net8.0 --runtime win-x64 --output $outputDirectory -p:PublishSingleFile=true --self-contained true -p:PublishReadyToRun=true
    
    # Zip Archive
    $files = Get-ChildItem -Path $outputDirectory | ForEach-Object { $_.FullName }
    Compress-Archive -Path $files -DestinationPath $archivePath
    
    # Open Folder
    Invoke-Item $outputDirectory
}