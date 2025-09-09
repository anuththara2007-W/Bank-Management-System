# AutoGitPush.ps1
param (
    [string]$RepoPath = "C:\Users\User\Bank  Management System",
    [string]$Branch = "master"
)

Set-Location $RepoPath

while ($true) {
    $status = git status --porcelain

    if ($status) {
        git add .
        git commit -m "project updated"
        git push origin $Branch
        Write-Output "[$(Get-Date -Format 'HH:mm:ss')] Changes committed & pushed to $Branch"
    }

    Start-Sleep -Seconds 2
}
