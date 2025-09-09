$RepoPath = "C:\Users\User\Desktop\Bank-Management-System"
$Branch = "master"
Set-Location $RepoPath

while ($true) {
    if (git status --porcelain) {
        git add .
        git commit -m "Project Updated $(Get-Date -Format 'HH:mm:ss')"
        git pull origin $Branch --rebase
        git push origin $Branch
        Write-Host "✅ Project Updated"
    }
    else {
        Write-Host "⏱ No changes detected"
    }

    Start-Sleep -Seconds 2
}
